using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IAccountRepository
    {
        int Register(Users request);
        Users GetById(int id);
        Users GetUserByCredentials( string email, string mobile);
        Users GetUserDetailByColumnName(string columnName, string value);
        void AddUserResetToken(int userId, byte[] resetToken, DateTime expiryDate);
        void UpdateUserPassword(int userId, Users request);
        int GetUserIdByToken(byte[] resetToken);
        void RemovePasswordResetToken(byte[] resetToken);
        void UpdateUserDetails(int userId, UserDetailUpdateRequest request);

        string GetUserEmailbyId(int id);
        int GetUserIdbyEmail(string email);

        UserRes GetNameImageById(int id);
    }
}
