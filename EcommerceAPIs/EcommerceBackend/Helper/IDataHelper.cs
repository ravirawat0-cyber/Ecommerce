namespace EcommerceBackend.Helper
{
    public interface IDataHelper
    {
        void DeleteFromProductCategoryTable(int productId);
        void AddIntoProductCategoryTable(int productId, string categoryIds);
    }
}
