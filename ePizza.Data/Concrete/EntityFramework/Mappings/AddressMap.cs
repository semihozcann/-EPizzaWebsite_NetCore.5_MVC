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
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id); //primary key
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); //bir bir arttır
            builder.Property(a => a.Street).IsRequired(); //zorunlu
            builder.Property(a => a.Street).HasMaxLength(250); //max uzunluk 
            builder.Property(a => a.Locality).IsRequired();
            builder.Property(a => a.Locality).HasMaxLength(100);
            builder.Property(a => a.ZipCode).IsRequired();
            builder.Property(a => a.ZipCode).HasMaxLength(10);
            builder.Property(a => a.PhoneNumber).IsRequired();
            builder.Property(a => a.PhoneNumber).HasMaxLength(13); 
            builder.ToTable("Addresses"); //tablo ismi belirtme
        }
    }
}
