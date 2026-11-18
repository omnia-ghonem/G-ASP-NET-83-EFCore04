using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace G_ASP_NET_83_EFCore04.Configurations
{
    public class CustomerAccountConfiguration: IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> CA)
        {
            CA.HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            CA.Property(ca => ca.OwnershipStartDate)
              .HasColumnType("date")
              .IsRequired();

            CA.Property(ca => ca.OwnershipRole)
              .IsRequired()
              .HasConversion<string>();
            CA.Property(ca => ca.AccountStatus)
              .IsRequired()
              .HasDefaultValue(AccountStatus.Active)
              .HasConversion<string>();

            CA.HasOne(ca => ca.Customers)
              .WithMany(c => c.customerAccounts)
              .HasForeignKey(ca => ca.CustomerId)
              .OnDelete(DeleteBehavior.Cascade);

            CA.HasOne(ca => ca.Accounts)
              .WithMany(a => a.customerAccounts)
              .HasForeignKey(ca => ca.AccountNumber)
              .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
