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

        public void AddEmailUUID(string email, string uuid)
        {
            var query = @"INSERT INTO EmailUUID
                         (Email , UUID)
                         VALUES (@Email, @UUID)";
            var values = new { Email = email, UUID = uuid };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);
        }

        public string GetUUIDbyEmail(string email)
        {
            var query = @"SELECT UUID
                         FROM EmailUUID
                          WHERE Email = @Email";
            var values = new { Email = email };
            using var connection = _dbContext.CreateConnection();
            var response = connection.QueryFirstOrDefault<string>(query, values);
            return response;
        }

        public void DeleteEmailUUID(string email)
        {
            var query = @"DELETE FROM EmailUUID
                         WHERE Email = @Email";
            var values = new { Email = email };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);

        }
    }
}
