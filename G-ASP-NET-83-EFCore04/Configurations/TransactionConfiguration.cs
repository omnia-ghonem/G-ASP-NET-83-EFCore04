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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> T)
        {

            T.HasKey(t => t.TransactionNumber);
            T.Property(a => a.TransactionNumber)
             .UseIdentityColumn(1, 1);

            T.Property(t => t.TransactionDate)
             .IsRequired()
             .HasColumnType("datetime2");

            T.Property(t => t.Amount)
             .IsRequired()
             .HasPrecision(18, 2);

            T.Property(t => t.TransactionType)
             .IsRequired()
             .HasConversion<string>();

            T.Property(t => t.Note)
             .IsRequired(false)
             .HasMaxLength(500);


            T.HasOne(t => t.Account)
             .WithMany(a => a.Transactions)
             .HasForeignKey(t => t.AccountId)
             .OnDelete(DeleteBehavior.NoAction);




        }

    }
}
