using Dapper;

namespace EcommerceBackend.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext dbContext)
        {
            _context = dbContext;
        }    

        public int CreateDb(string query, object value )
        {
            using var connection = _context.CreateConnection();
            return connection.ExecuteScalar<int>( query, value );   
        }

        public void DeleteDb(string query, object value)
        {
            using var connection = _context.CreateConnection();
            connection.Execute(query, value);
        }

        public IEnumerable<T> GetAllDb(string query)
        {
            using var connection = _context.CreateConnection();
            return connection.Query<T>(query);
        }

        public T GetByCredDb(string query, object value)
        {
            using var connection = _context.CreateConnection();
            return connection.QueryFirstOrDefault<T>(query, value);
        }


        public IEnumerable<T> GetById(string query, object value)
        {
            using var connection = _context.CreateConnection();
            return connection.Query<T>(query, value);
        }


        public void UpdateDb(string query, object values)
        {
            using var connection = _context.CreateConnection();
            connection.Execute(query, values);
        }

        public int GetCountFromDb(string query,object values)
        {
            using var connection = _context.CreateConnection();
            return connection.ExecuteScalar<int>(query, values);
        }

        

    }
}
