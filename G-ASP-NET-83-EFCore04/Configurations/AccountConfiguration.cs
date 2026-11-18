using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Configurations
{
    public class AccountConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> A)
        {
            A.HasKey(a => a.AccountNumber);
            A.Property(a => a.AccountNumber)
             .UseIdentityColumn(1, 1);
              
            A.Property(a => a.CurrentBalance)
             .IsRequired()
             .HasColumnType("decimal(18,2)");

            A.Property(a => a.AccountType)
             .IsRequired()
             .HasConversion<string>();

            A.Property(a => a.OpeningDate)
             .IsRequired()
             .HasColumnType("date");

            A.HasOne(a => a.Branch)
             .WithMany(b => b.Accounts)
             .HasForeignKey(a => a.BranchCode)
             .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
