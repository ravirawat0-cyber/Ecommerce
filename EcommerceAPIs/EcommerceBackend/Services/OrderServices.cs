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

        public OrderServices(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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


        public OrderResponse GetAllByUserId(string userId)
        {
            var orderDb = _orderRepository.GetAllByUserId(int.Parse(userId));
            if (orderDb == null)
                throw new KeyNotFoundException("Orders not found.");

            var response = new OrderResponse
            {
                Data = orderDb,
                StatusMessage = "Success."
            };

            return response;

        }


        public OrderResponse GetByTransactionId(string transactionId)
        {
            var orderDb = _orderRepository.GetByTransactionId(transactionId);
            if (orderDb == null)
                throw new KeyNotFoundException("Order not found.");

            var response = new OrderResponse
            {
                Data = new Order[] { orderDb },
                StatusMessage = "Success."

            };
            return response;

        }
    }
}
