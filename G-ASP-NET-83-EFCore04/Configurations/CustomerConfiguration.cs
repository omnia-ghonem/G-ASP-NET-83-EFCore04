using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Configurations
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> C)
        {
            C.HasKey(c => c.Id);
            C.Property(c => c.Id)
             .UseIdentityColumn(1, 1);

            C.Property(c => c.Address)
             .IsRequired()
             .HasMaxLength(250);

            C.Property(c => c.DateOfBirth)
             .IsRequired()
             .HasColumnType("date");

            C.Property(c => c.FullName)
             .IsRequired()
             .HasMaxLength(150);

            C.Property(c => c.Email)
             .IsRequired();

            C.Property(c => c.PhoneNumber)
             .IsRequired()
             .HasMaxLength(11);

            C.Property(c => c.CustomerType)
             .IsRequired()
             .HasConversion<string>();

            C.Property(c => c.NationalId)
             .IsRequired()
             .HasMaxLength(14);

            C.HasIndex(c => c.NationalId).IsUnique();
            C.HasIndex(c => c.Email).IsUnique();

        }
    }
}
