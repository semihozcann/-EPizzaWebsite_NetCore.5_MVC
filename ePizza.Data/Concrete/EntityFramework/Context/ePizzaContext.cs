
using ePizza.Data.Concrete.EntityFramework.Mappings;
using ePizza.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntityFramework.Context
{
    public class ePizzaContext : IdentityDbContext<User, Role, int, UserClaim,UserRole,UserLogin,RoleClaim, UserToken>
    {

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public ePizzaContext()
        {

        }


        //configuration ayarları olacak.
        public ePizzaContext(DbContextOptions<ePizzaContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5R6CJJ3\SQLEXPRESS;Database=ePizzaDb;Trusted_Connection=true");
        }

        //Db kurallarını burada db oluşurken veriyoruz.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaymentDetailsMap());
            builder.ApplyConfiguration(new CartMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new ProductTypeMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderItemMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserTokenMap());
            builder.ApplyConfiguration(new UserRoleMap());
            builder.ApplyConfiguration(new UserLoginMap());
            builder.ApplyConfiguration(new RoleClaimMap());
            builder.ApplyConfiguration(new UserClaimMap());

        }
    }
}
