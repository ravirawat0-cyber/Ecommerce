using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using EcommerceBackend.Helper;

namespace EcommerceBackend.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly ICartServices _cartServices;
        private readonly IOrderServices _orderServices;
        private readonly IDataHelper _dataHelper;
        private const string endpointSecret = "your stripe key";
     

        public AccountController(IAccountServices accountServices,
                                 ICartServices cartServices,
                                  IOrderServices orderServices,
                                  IDataHelper dataHelper)
        {
            _accountServices = accountServices;
            _cartServices = cartServices;
            _orderServices = orderServices;
            _dataHelper = dataHelper;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequest request)
        {
           var userDetails = _accountServices.Register(request);
            return Ok(userDetails);
        }


        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var userDetails = _accountServices.UserLogin(request);
            return Ok(userDetails);
        }

        [HttpGet("")]
        [Authorize]
        public IActionResult getUserDetial()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _accountServices.GetByUserId(userIdClaim);
            return Ok(response);
        }


        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUserDetails(UserDetailUpdateRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _accountServices.UpdateUserDetails(userIdClaim, request);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequest request)
        {
            var message = _accountServices.ForgotUserPassword(request);
            return Ok(message);
        }


        [HttpGet("purchase/{uuid}")]
        [Authorize]
        public IActionResult PurchaseCart(string uuid)
        {
            StripeConfiguration.ApiKey =
                "your stripe key";

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;

            var items = _cartServices.GetItemsByUserId(int.Parse(userIdClaim));
            var email = _accountServices.GetEmailByUserId(int.Parse(userIdClaim));

            var lineItems = items.Items.Select(item => new Stripe.Checkout.SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    
                    UnitAmount = (long)(item.ProductPrice * 100),
                    Currency = "inr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                        Images = new List<string> { item.ProductImage }
                    }
                },
                Quantity = item.Quantity
            }).ToList();


            var options = new Stripe.Checkout.SessionCreateOptions
            {
                Metadata = new Dictionary<string, string>{{"UserUUID", uuid}},
                LineItems = lineItems,
                CustomerEmail = email,
                SuccessUrl = $"http://localhost:4200/recipt/{uuid}",
                CancelUrl = "http://localhost:4200/cart",
                Mode = "payment",
            };

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);

            var response = new
            {
                Url = session.Url
            };

            return Ok(response);
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);
            
            var x = stripeEvent.Data.Object as Stripe.Checkout.Session;

            if (stripeEvent != null)
            {
                JObject eventData = JObject.Parse(json);
                string type = (string)eventData["type"];

                switch (type)
                {
                    case "checkout.session.completed":
                        {
                            var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                            if (session != null)
                            {
                                var  userTransaction = session.Metadata.GetValueOrDefault("UserUUID");
                                var userEmail = session.CustomerEmail;
                                _dataHelper.AddEmailUUID(userEmail, userTransaction);
                            }
                        }
                        break;
                    case "charge.succeeded":
                    {
                        var userEmail = (string)eventData["data"]["object"]["billing_details"]["email"];
                            var totalPrice = (long)eventData["data"]["object"]["amount"];
                            var receiptUrl = (string)eventData["data"]["object"]["receipt_url"];
                            var uuid = _dataHelper.GetUUIDbyEmail(userEmail);
                            _dataHelper.DeleteEmailUUID(userEmail);
                            var userId = _accountServices.GetUserIdByEmail(userEmail);

                             var orderReq = new Order
                            {   
                                UserId = userId,
                                TransactionId = uuid,
                                UserEmail = userEmail,
                                TotalPrice = totalPrice,
                                ReceiptURL = receiptUrl
                            };

                            var id = _orderServices.Create(orderReq);
                            var items = _accountServices.GetByUserId(userId.ToString());

                            foreach (var item in items.Data.Cart.Items)
                            {
                                var orderItemReq = new OrderItem
                                {
                                    OrderId = id,
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.ProductPrice,
                                    ProductImage = item.ProductImage,
                                    ProductName = item.ProductName,
                                };
                                _orderServices.CreateOrderItem(orderItemReq);
                            }

                            _cartServices.DeleteCart(userId.ToString());

                    }
                        break;
                    default:
                        break;
                }
            }

            return Ok();
        }


        [HttpPut("update-password")]
        public IActionResult UpdatePassword(UpdatePasswordRequest request)
        {
            var message = _accountServices.UpdatePassword(request);
            return Ok(message);
        }

    }
}
