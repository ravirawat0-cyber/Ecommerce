using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
    {
        private readonly DbContext _dbContext;

        public SubCategoryRepository(DbContext dbContext) : base(dbContext)
        {
             _dbContext = dbContext;
        }

        public int Create(SubCategory entity)
        {
            string query = @"INSERT INTO SubCategory (
                         Name,
                         ParentCategoryId,
                         ImageUrl
                     )
                     VALUES (
                       @Name, 
                       @ParentCategoryId,
                       @ImageUrl
                     );
                   SELECT SCOPE_IDENTITY()";
            var values = new
            {
                Name = entity.Name,
                ParentCategoryId = entity.ParentCategoryId,
                ImageUrl = entity.ImageUrl
            };
            var id = CreateDb(query, values);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE FROM SubCategory
                          WHERE Id = @id";
            var values = new { Id = id };
            DeleteDb(query, values);
        }

        public int IsDataExists(string name)
        {
            var query = @"SELECT COUNT(*)
                          FROM SubCategory
                          WHERE Name = @name";
            var values = new { Name = name };
            var count = GetCountFromDb(query, values);
            return count;
        }

        public IEnumerable<SubCategory> GetAll()
        {
            var query = @"SELECT * 
                          FROM SubCategory (NOLOCK)";
            var subCategoryList = GetAllDb(query);
            return subCategoryList;
        }
    }
}
