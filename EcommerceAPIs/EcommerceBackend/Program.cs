using EcommerceBackend.CustomMiddleware;
using EcommerceBackend.Helper;
using EcommerceBackend.Repository;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace EcommerceBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey

                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();

            });

         
            builder.Services.AddScoped<DbContext>(); 
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IDataHelper, DataHelper>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartServices, CartServices>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod().
                        AllowAnyHeader().
                        AllowCredentials());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {                                                
                         options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                         {
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                             ValidateIssuer = false,
                             ValidateAudience = false,
                             ValidateLifetime = true,
                             ClockSkew = TimeSpan.Zero
                         };

                         //handle jwt token validation failures with a custom response
                         options.Events = new JwtBearerEvents
                         {
                             OnAuthenticationFailed = context =>
                             {
                                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                 {
                                     context.Response.Headers.Add("Token-Expired", "true");
                                     context.Response.StatusCode = 401;
                                     context.Response.ContentType = "application/json";
                                     return context.Response.WriteAsync(
                                         "{'message': 'Your session has expired. Please log in again'}");
                                 }

                                 return Task.CompletedTask;
                             }
                         };
                     });


            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}