using ePizza.Data.Concrete.EntityFramework.Context;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Implementations;
using ePizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Configuration
{
    public class ConfigureRepositories
    {
        public static void AddService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ePizzaContext>((option) =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequiredLength = 6; //minimum karakter sayısı
                option.Password.RequireDigit = false; //sayısal karakter
                option.Password.RequiredUniqueChars = 0; //birbirinden farklı
                option.Password.RequireNonAlphanumeric = false; //sayısal değer gerekli değil
                option.Password.RequireLowercase = false; //küçük harf
                option.Password.RequireUppercase = false; //büyük harf
                option.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789._-@+";  //Username and email options
                option.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<ePizzaContext>();


            services.AddScoped<DbContext, ePizzaContext>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();


            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<Category>, Repository<Category>>();
            services.AddTransient<IRepository<ProductType>, Repository<ProductType>>();
            services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
            services.AddTransient<IRepository<OrderItem>, Repository<OrderItem>>();
            services.AddTransient<IRepository<PaymentDetails>, Repository<PaymentDetails>>();

            //AddTransient: yükarıdaki nerneleri her kullanımda tekrar cağırma işlemini yapar. tekrar tekrar her istekte nesne oluşur kısacası.
        }
    }
}
