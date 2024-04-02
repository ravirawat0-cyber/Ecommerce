using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IAccountRepository
    {
        int Register(Users request);
        Users GetById(int id);
        Users GetUserByCredentials(string userName, string email, string mobile);
        Users GetUserDetailByColumnName(string columnName, string value);
        void AddUserResetToken(int userId, byte[] resetToken, DateTime expiryDate);
        void UpdateUserDetails(int userId, Users request);
        int GetUserIdByToken(byte[] resetToken);
    }
}
