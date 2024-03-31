using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Repository.Interfaces;


namespace EcommerceBackend.Repository
{

    public class AccountRepository : BaseRepository<Users> , IAccountRepository
    {
        private readonly DbContext _dbContext;

        public AccountRepository(DbContext dbContext) : base(dbContext)
        {
           
            _dbContext = dbContext;
        }

        public Users GetUserByCredentials(string userName , string email, string mobile)
        {
            var query = @"SELECT UserName, Email, Mobile 
                      FROM UsersDetails 
                      WHERE UserName = @UserName OR Email = @Email OR Mobile = @Mobile";
            var values = new { UserName = userName, Email = email, Mobile = mobile };

            var users = GetByCredDb(query, values);
            return users;
        }

        public Users GetUserDetailByUserName(string userName)
        {
            var query = @"SELECT *
                      FROM UsersDetails 
                      WHERE UserName = @UserName";
            var values = new { UserName = userName};

            var users = GetByCredDb(query, values);
            return users;
        }

        public int Register(Users request)
        {
            var query = @"INSERT INTO UsersDetails 
                             (Name, UserName, Mobile, Email, PasswordHash, PasswordSalt) 
                            VALUES (@Name, @UserName, @Mobile, @Email, @PasswordHash, @PasswordSalt);
                            SELECT SCOPE_IDENTITY()";
            var values = new
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.UserName,
                Mobile = request.Mobile,
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
    }
}
