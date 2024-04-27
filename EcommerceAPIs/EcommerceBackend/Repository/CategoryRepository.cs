using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class CategoryRepository : BaseRepository<Category>,  ICategoryRepository
    {
        private readonly DbContext _dbContext;

        public CategoryRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Category entity)
        {
            string query = @"INSERT INTO Category (
                                Name
                            ) 
                            VALUES (@name)
                            SELECT SCOPE_IDENTITY()";
            var values = new { Name = entity.Name };
            var id = CreateDb(query, values);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE FROM Category
                          WHERE Id = @id";
            var values = new { Id = id };
            DeleteDb(query, values);
        }

        public IEnumerable<Category> GetAll()
        {
            var query = @"SELECT * 
                          FROM Category (NOLOCK)";
            var categoryList = GetAllDb(query);
            return categoryList;
        }

        public Category GetById(int id)
        {
            var query = @"SELECT * FROM Category (NOLOCK)
                       WHERE Id = @id";
            var values = new { Id = id };
            return GetByCredDb(query, values);
        }

        public IEnumerable<DbCategoryResponse> GetCategoryWithSubCategory()
        {
            var query = @"SELECT 
                      c.Id AS CategoryId,
                      c.Name AS Category,
                      sc.Id AS SubCategoryId,
                      sc.Name AS SubCategoryName,
                      sc.ImageUrl AS SubCategoryImageUrl
                      FROM Category AS c 
                      INNER JOIN 
                          SubCategory sc ON c.Id = sc.ParentCategoryId 
                      ";
            using var connection = _dbContext.CreateConnection();
            var categoryInfo = connection.Query<DbCategoryResponse>(query);
            return categoryInfo;
        }

        public int CheckCategoryByName(string name)
        {
            var query = @"SELECT COUNT(*) FROM Category (NOLOCK)
                          WHERE Name = @name";
            var value = new { Name = name };
            return GetCountFromDb(query, value);
        }
        
        public void Update(int id, Category entity)
        {
            var query = @"UPDATE Category
                        SET
                          Name = @name
                        WHERE Id = @id";
            var values = new { Id = id,Name = entity.Name };    
            UpdateDb(query, values);
        }
    }
}
