namespace EcommerceBackend.Helper
{
    public interface IDataHelper
    {
        void AddEmailUUID(string email, string uuid);

        string GetUUIDbyEmail(string email);
        void DeleteEmailUUID(string email);
    }
}
