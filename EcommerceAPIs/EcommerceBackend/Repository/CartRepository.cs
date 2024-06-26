﻿using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class CartRepository : BaseRepository<Carts> , ICartRepository
    {
        private readonly DbContext _dbcontext;

        public CartRepository(DbContext dbContext) :base(dbContext)
        {
                _dbcontext = dbContext;
        }

        public IEnumerable<Carts> GetItemsByUserId(int userId)
        {
            var query = @"
            SELECT p.Id AS ProductId, p.Name AS ProductName, p.Price AS ProductPrice,p.CoverImage as ProductImage, c.Quantity
            FROM Carts c
            JOIN Products p ON c.ProductId = p.Id
            WHERE c.UserId = @UserId";

            var value = new { UserId = userId };
            using var connection = _dbcontext.CreateConnection();
            var dbresponse = connection.Query<Carts>(query, value);
            return dbresponse;
        }

        
        public int AddItemsToCart(CartRequest cart, int userId)
        {
            var query = @"INSERT INTO Carts
                           (UserId, ProductId, Quantity )
                         VALUES (@UserId, @ProductId, @Quantity)
                         SELECT SCOPE_IDENTITY()";
            var value = new
            {
                UserId = userId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
            };
            var id = CreateDb(query, value);
            return id;
        }

        public void UpdateCart(CartRequest cart, int userId)
        {
            var query = @"
                        UPDATE Carts
                        SET Quantity = @Quantity
                        WHERE UserId = @UserId AND ProductId = @ProductId";
            var values = new
            {
                UserId = userId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
            };
            UpdateDb(query, values);
        }

        public void DeleteCartItem(int productId, int userId)
        {
            var query = @"
                        DELETE FROM Carts
                        WHERE ProductId = @ProductId
                              AND UserId = @UserId 
                         ";
            var values = new { ProductId = productId, UserId = userId };
            DeleteDb(query, values);
        }


        public void DeleteCart(int userId)
        {
            var query = @"
                        DELETE FROM Carts
                        WHERE UserId = @userId";
            var values = new { UserId = userId };
            DeleteDb(query, values);
        }

        public bool CheckProductWithUserExist(int productId, int userId)
        {
            var query = @"SELECT COUNT(*)
                          FROM Carts
                          WHERE UserId = @UserId AND ProductId = @ProductId";
            var values = new
            {
                UserId = userId,
                ProductId = productId,
            };
            var response = GetCountFromDb(query, values);
            return response > 0;
        }
    }
}
