using AutoMapper.Configuration.Conventions;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;


namespace EcommerceBackend.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountServices _accountServices;


        public OrderServices(IOrderRepository orderRepository , IAccountServices accountServices)
        {
            _orderRepository = orderRepository;
            _accountServices = accountServices;
        }


        public int Create(Order request)
        {
            if (string.IsNullOrEmpty(request.UserEmail))
            {
                throw new ArgumentException("Email cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(request.TransactionId))
            {
                throw new ArgumentException("Transaction ID cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(request.ReceiptURL))
            {
                throw new ArgumentException("Receipt URL cannot be null or empty.");
            }

            if (request.TotalPrice <= 0)
            {
                throw new ArgumentException("Total price must be greater than zero.");
            }
            request.OrderDate = DateTime.UtcNow;

            var response = _orderRepository.Create(request);
            return response;
        }

        public int CreateOrderItem(OrderItem item)
        {
            var response = _orderRepository.CreateOrderItems(item);
            return response;
        }

        public int hasPurchasedQuery(int userId, int productId)
        {
            return _orderRepository.hasPurchasedQuery(userId, productId);
        }
        


        public OrderResponseModel.OrderResponse GetAllByUserId(int userId)
        {
            var email = _accountServices.GetEmailByUserId(userId);
            var orderDb = _orderRepository.GetAllByUserEmail(email);
            if (orderDb == null)
                throw new KeyNotFoundException("Orders not found.");

            var orderDetailsList = new List<OrderResponseModel.OrderDetails>();

            foreach (var dbOrder in orderDb)
            {
                var itemDetails = _orderRepository.GetOrderItemsByOrderId(dbOrder.Id);

                var orderDetails = new OrderResponseModel.OrderDetails
                {
                    TotalPrice = dbOrder.TotalPrice,
                    ItemDetails = itemDetails,
                    OrderDate = dbOrder.OrderDate,
                    TransactionId = dbOrder.TransactionId,
                    ReceiptURL = dbOrder.ReceiptURL
                };

                orderDetailsList.Add(orderDetails);
            }

            var response = new OrderResponseModel.OrderResponse
            {
                Data = new OrderResponseModel.OrderData
                {
                    OrderDetails = orderDetailsList
                },
                StatusMessage = "Success."
            };

            return response;
        }


        public OrderResponseModel.TransactionOrderResponse GetByTransactionId(string transactionId)
        {
            var orderDb = _orderRepository.GetByTransactionId(transactionId);
            if (orderDb == null)
                throw new KeyNotFoundException("Order not found.");

            var response = new OrderResponseModel.TransactionOrderResponse
            {
                Data = orderDb,
                StatusMessage = "Success."

            };
            return response;

        }
    }
}
