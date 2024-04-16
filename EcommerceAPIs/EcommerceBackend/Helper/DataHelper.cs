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
        public void AddIntoProductSubCategoryTable(int productId, string subCategoryIds)
        {
            string query = @"INSERT INTO Product_Subcategories 
                            (ProductID, SubCategoryId)
                            VALUES (@ProductId, @SubCategoryId)";
            string[] subcategoryArray = subCategoryIds.Split(',');
            foreach (string subcategoryIdString in subcategoryArray)
            {
                if (int.TryParse(subcategoryIdString, out int subcategoryId))
                {
                    var values = new { ProductId = productId, SubCategoryId = subcategoryId };
                    using var connection = _dbContext.CreateConnection();
                    connection.Execute(query, values);
                }
            }
        }
        public void DeleteFromProductSubCategoryTable(int productId)
        {
            string query = @"DELETE FROM Product_Subcategories 
                     WHERE ProductID = @ProductId";
            var values = new { ProductId = productId };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);
        }

    }
}
