using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Stripe.Checkout;
using Stripe;
using static System.Net.WebRequestMethods;
namespace EcommerceBackend.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly ICartServices _cartServices;
        private readonly IOrderServices _orderServices;
        const string endpointSecret = "whsec_f78dc11d4f2170c6d5259edfebbfd5baf54a8fa0c3de6d525894ee9eb1e1410a";

        public AccountController(IAccountServices accountServices,
                                 ICartServices cartServices,
                                  IOrderServices orderServices)
        {
            _accountServices = accountServices;
            _cartServices = cartServices;
            _orderServices = orderServices;
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


        [HttpGet("purchase")]
     [Authorize]
        public IActionResult PurchaseCart()
        {
            StripeConfiguration.ApiKey =
                "sk_test_51PErUmSItTq9yJF65Pe7CStTmwqDj2W9aPEIZR71k7BwcyrfBx75kFavzG63aFxIsUJ5qPNJPjlKtnYtTreazVXZ0099XF23Am";

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;

            var items = _cartServices.GetItemsByUserId(int.Parse(userIdClaim));
            var email = _accountServices.GetEmailByUserId(userIdClaim);

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
                LineItems = lineItems,
                CustomerEmail = email,
                SuccessUrl = "http://localhost:4200",
                CancelUrl = "http://localhost:4200",
                Mode = "payment",
            };

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);
            return Ok(session.Url);
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> webhook()
        {

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);



            if (stripeEvent != null)
            {
                JObject eventData = JObject.Parse(json);
                string type = (string)eventData["type"];

                switch (type)
                {
                    case "charge.succeeded":
                    {

                        var orderReq = new Order
                        {
                            UserEmail = (string)eventData["data"]["object"]["billing_details"]["email"],
                            TransactionId = (string)eventData["data"]["object"]["payment_method_details"]["card"]["three_d_secure"]["transaction_id"],
                            TotalPrice = (long)eventData["data"]["object"]["amount"],
                            ReceiptURL = (string)eventData["data"]["object"]["receipt_url"]
                        };

                        var id = _orderServices.Create(orderReq);

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
