using Dapper;
using EcommerceBackend.Models.DBModels;
using System.Transactions;

namespace EcommerceBackend.Helper
{
    public class DataHelper : IDataHelper
    {
        private readonly DbContext _dbContext;

        public DataHelper(DbContext context)
        {
            _dbContext = context;
        }
        public void AddIntoProductCategoryTable(int productId, string categoryIds)
        {
            string query = @"INSERT INTO Product_Categories 
                            (ProductID, CategoryId)
                            VALUES (@ProductId, @CategoryId)";
            string[] categoryArray = categoryIds.Split(',');
            foreach (string categoryIdString in categoryArray)
            {
                if (int.TryParse(categoryIdString, out int categoryId))
                {
                    var values = new { ProductId = productId, categoryId = categoryId };
                    using var connection = _dbContext.CreateConnection();
                    connection.Execute(query, values);
                }
            }
        }
        public void DeleteFromProductCategoryTable(int productId)
        {
            string query = @"DELETE FROM Product_Categories  
                     WHERE ProductID = @ProductId";
            var values = new { ProductId = productId };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);
        }

    }
}
