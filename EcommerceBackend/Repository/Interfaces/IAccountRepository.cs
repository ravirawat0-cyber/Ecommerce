using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IAccountRepository
    {
        int Register(Users request);
        Users GetById(int id);
        Users GetUserByCredentials(string userName, string email, string mobile);
        Users GetUserDetailByUserName(string userName);
    }
}
