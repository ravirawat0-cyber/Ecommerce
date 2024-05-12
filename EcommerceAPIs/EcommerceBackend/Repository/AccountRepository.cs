using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Repository.Interfaces;
using System.Reflection;


namespace EcommerceBackend.Repository
{

    public class AccountRepository : BaseRepository<Users>, IAccountRepository
    {
        private readonly DbContext _dbContext;

        public AccountRepository(DbContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;
        }

        public Users GetUserByCredentials( string email, string mobile)
        {
            var query = @"SELECT  Email, Mobile 
                      FROM UsersDetails 
                      WHERE Email = @Email OR Mobile = @Mobile";
            var values = new { Email = email, Mobile = mobile };

            var users = GetByCredDb(query, values);
            return users;
        }

        public Users GetUserDetailByColumnName(string columnName, string value)
        {
            var query = $@"SELECT *
                      FROM UsersDetails 
                      WHERE {columnName} = @value";
            var values = new { Value = value };

            var users = GetByCredDb(query, values);
            return users;
        }

        public int Register(Users request)
        {
            var query = @"INSERT INTO UsersDetails 
                             (Name, Mobile, Email, Address, JoinedDate, PasswordHash, PasswordSalt) 
                            VALUES (@Name, @Mobile, @Email,@Address, @JoinedDate, @PasswordHash, @PasswordSalt);
                            SELECT SCOPE_IDENTITY()";
            var values = new
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Mobile = request.Mobile,
                JoinedDate = request.JoinedDate,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt,
            };
            var id = CreateDb(query, values);
            return id;

        }

        public Users GetById(int id)
        {
            var query = @"SELECT * FROM UsersDetails (NOLOCK)
                       WHERE Id = @id";
            var values = new { Id = id };
            return GetByCredDb(query, values);
        }


        public string GetUserEmailbyId(int id)
        {
            var query = @"SELECT Email FROM UsersDetails (NOLOCK)
                          WHERE Id = @id";
            var values = new { Id = id };
            using var connection = _dbContext.CreateConnection();
            var response = connection.QueryFirstOrDefault<string>(query, values);
            return response;
        }

        public void AddUserResetToken(int userId, byte[] resetToken, DateTime expiryDate)
        {
            var query = @"INSERT INTO UsersRestToken 
                             (UserId, ResetToken, ExpiryDate) 
                            VALUES (@userId, @resetToken, @expiryDate)";
            var values = new
            {
                UserId = userId,
                ResetToken = resetToken,
                ExpiryDate = expiryDate
            };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);
        }


        public int GetUserIdByToken(byte[] resetToken)
        {
            var query = @"SELECT UserId 
                  FROM UsersRestToken 
                  WHERE ResetToken = @resetToken 
                  AND ExpiryDate > GETDATE()";
            var values = new { ResetToken = resetToken };

            using var connection = _dbContext.CreateConnection();
            var userId = connection.QueryFirstOrDefault<int>(query, values);

            return userId;
        }

        public void RemovePasswordResetToken(byte[] resetToken)
        {
            var query = @"DELETE FROM UsersRestToken
                          WHERE ResetToken = @resetToken";
            var value = new { ResetToken = resetToken };
            DeleteDb(query, value);
        }

        public void UpdateUserPassword(int userId, Users request)
        {
            var query = @"
                        UPDATE UsersDetails
                        SET Name = @Name, 
                            Mobile = @Mobile, 
                            Email = @Email, 
                             Address = @Address,
                            PasswordHash = @PasswordHash, 
                            PasswordSalt = @PasswordSalt
                        WHERE Id = @UserId
                    ";

            var values = new
            {
                UserId = userId,
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Mobile = request.Mobile,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt,
            };
            UpdateDb(query, values);
        }

        public void UpdateUserDetails(int userId, UserDetailUpdateRequest request)
        {
            var query = @"
                        UPDATE UsersDetails
                        SET Name = @Name, 
                            Mobile = @Mobile, 
                            Address = @Address
                        WHERE Id = @UserId
                    ";

            var values = new
            {
                UserId = userId,
                Name = request.Name,
                Address = request.Address,
                Mobile = request.Mobile,
            };
            UpdateDb(query, values);
        }
    }
}
