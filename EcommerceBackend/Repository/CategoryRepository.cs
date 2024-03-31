using EcommerceBackend.Models.DBModels;
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
