using ePizza.Services.Implementations;
using ePizza.Services.Interfaces;
using ePizza.UI.Helpers;
using ePizza.UI.Helpers.Interfaces;
using ePizza.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ePizza.UI.Configuration
{
    public static class ConfigureDependencies
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationManager>();
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IPaymentService, PaymentManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<IProductTypeService, ProductTypeManager>();
            services.AddTransient<IOrderService, OrderManager>();
            services.AddTransient<ICartService, CartManager>();
            services.AddTransient<IFileHelper, FileHelper>();


        }
    }
}
