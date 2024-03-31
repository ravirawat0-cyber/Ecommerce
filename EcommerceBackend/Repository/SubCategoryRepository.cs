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
                         ParentCategoryId
                     )
                     VALUES (
                       @Name, 
                       @ParentCategoryId
                     );
                   SELECT SCOPE_IDENTITY()";
            var values = new
            {
                Name = entity.Name,
                ParentCategoryId = entity.ParentCategoryId,
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

        public IEnumerable<SubCategory> GetAll()
        {
            var query = @"SELECT * 
                          FROM SubCategory (NOLOCK)";
            var subCategoryList = GetAllDb(query);
            return subCategoryList;
        }

        public SubCategory GetById(int id)
        {
            var query = @"SELECT * FROM SubCategory (NOLOCK)
                       WHERE Id = @id";
            var values = new { Id = id };
            return GetByCredDb(query, values);
        }

        public void Update(int id, SubCategory entity)
        {
            var query = @"UPDATE SubCategory
                        SET
                            Name = @name,
                            ParentCategoryId = @parentCategoryId
                        WHERE Id = @id";
            var values = new
            {
                Id = id,
                Name = entity.Name,
                ParentCategoryId = entity.ParentCategoryId
     
            };
            UpdateDb(query, values);
        }
    }
}
