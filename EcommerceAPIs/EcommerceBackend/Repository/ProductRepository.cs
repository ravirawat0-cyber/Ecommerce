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
                    Price,
                    CompanyName,
                    Sold,
                    IsCustomized,
                    KeyFeature,
                    IsActive,
                    ImageUrls
                )
                VALUES (
                    @Name,
                    @Description,
                    @Price,
                    @CompanyName,
                    @Sold,
                    @IsCustomized,
                    @KeyFeature,
                    @IsActive,
                    @ImageUrls
                );
                SELECT SCOPE_IDENTITY()
            ";
            var values = new
            {
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CompanyName = entity.CompanyName,
                Sold = entity.Sold,
                IsCustomized = entity.IsCustomized,
                KeyFeature = entity.KeyFeature,
                IsActive = entity.IsActive,
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
                p.Id AS ProductID,
                p.Name AS ProductName,
                p.Description,
                p.Price,
                p.CompanyName,
                p.Sold,
                p.IsCustomized,
                p.KeyFeature,
                p.IsActive,
                p.ImageUrls,
                c.Name AS CategoryName,
                sc.Id AS SubCategoryID,
                sc.Name AS SubCategoryName
            FROM 
                Products p
            INNER JOIN 
                Product_SubCategories psc ON p.Id = psc.ProductId
            INNER JOIN 
                SubCategory sc ON psc.SubCategoryId = sc.Id
            INNER JOIN 
                Category c ON sc.ParentCategoryId = c.Id";

            using var connection = _dbContext.CreateConnection();
            var ProductInfo = connection.Query<DbProductResponse>(query);
            return ProductInfo;
           
        }

        public void Update(int id, Products entity)
        {
            var query = @"UPDATE Products
                  SET 
                   Name  = @name,
                   Description = @description,
                   Price = @price,
                   CompanyName = @companyName,
        
                   Sold = @sold,
                   IsCustomized = @isCustomized,
                   KeyFeature = @keyFeature,
                   IsActive @isActive,
                   ImageUrls = @imageUrls
                 WHERE Id = @id";
                   
            var values = new
            {
                Id = id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                CompanyName = entity.CompanyName,
                Sold = entity.Sold,
                IsCustomized = entity.IsCustomized,
                KeyFeature = entity.KeyFeature,
                IsActive = entity.IsActive,
                ImageUrls = entity.ImageUrls
            };
            UpdateDb(query, values);      
        }
    }
}
