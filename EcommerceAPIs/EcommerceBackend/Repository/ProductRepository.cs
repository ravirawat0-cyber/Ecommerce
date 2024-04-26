using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EcommerceBackend.Repository
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository 
    {
        private readonly DbContext _dbContext;

        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Products entity)
        {
            string query = @"
                INSERT INTO Products (
                    Name,
                    Description,
                    SubcategoryId,
                    Price,
                    CompanyName,
                    Sold,
                    KeyFeature,
                    CoverImage,
                    ImageUrls
                )
                VALUES (
                    @Name,
                    @Description,
                    @SubcategoryId,
                    @Price,
                    @CompanyName,
                    @Sold,
                    @KeyFeature,
                    @CoverImage,
                    @ImageUrls
                );
                SELECT SCOPE_IDENTITY()
            ";
            var values = new
            {
                Name = entity.Name,
                Description = entity.Description,
                SubcategoryId = entity.SubcategoryId,
                Price = entity.Price,
                CompanyName = entity.CompanyName,
                Sold = entity.Sold,
                KeyFeature = entity.KeyFeature,
                CoverImage = entity.CoverImage,
                ImageUrls = entity.ImageUrls
            };

            var id = CreateDb(query, values);
            return id;


        }

        public void Delete(int id)
        {
            var query = @"
                        DELETE 
                        FROM Products
                        WHERE Id = @id";
            var value = new {Id = id};
            DeleteDb(query, value);
        }


        public IEnumerable<DbProductResponse> GetProductInfo()
        {
            string query = @"
                      SELECT 
                          p.Id AS ProductId,
                          p.Name AS ProductName,
                          p.Description,
                          p.Price,
                          p.CompanyName,
                          p.Sold,
                          p.KeyFeature,
                          p.ImageUrls,
                          p.CoverImage,
                          sc.Id AS SubCategoryId,
                          sc.Name AS SubCategoryName,
                          c.Id AS CategoryId,
                          c.Name AS CategoryName
                      FROM 
                          Products p
                      INNER JOIN 
                          SubCategory sc ON p.SubcategoryId = sc.Id
                      INNER JOIN 
                          Category c ON sc.ParentCategoryId = c.Id";
            using var connection = _dbContext.CreateConnection();
            var ProductInfo = connection.Query<DbProductResponse>(query);
            return ProductInfo;
           
        }
    }
}
