namespace EcommerceBackend.Helper
{
    public interface IDataHelper
    {
        void DeleteFromProductSubCategoryTable(int productId);
        void AddIntoProductSubCategoryTable(int productId, string subCategoryIds);
    }
}
