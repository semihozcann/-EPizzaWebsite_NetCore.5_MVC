using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Data.Concrete.EntityFramework.Mappings
{
    public class PaymentDetailsMap : IEntityTypeConfiguration<PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.Property(p => p.TransactionId).HasMaxLength(400);
            builder.Property(p => p.Tax);
            builder.Property(p => p.Currency).HasMaxLength(500);
            builder.Property(p => p.Total);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(p => p.Status).HasMaxLength(150);
            builder.Property(p => p.CartId).HasMaxLength(150);
            builder.Property(p => p.UserId);
            builder.Property(p => p.GrandTotal);
            builder.ToTable("PaymentDetails");
        }
    }
}
