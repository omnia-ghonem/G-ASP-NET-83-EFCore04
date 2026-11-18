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
    public class ManagerConfiguration: IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> M)
        {
            M.HasKey(m => m.Id);
            M.Property(m => m.Id)
             .UseIdentityColumn(1, 1);

            M.Property(m => m.FullName)
             .IsRequired()
             .HasMaxLength(150);

            M.Property(m => m.Email)
             .IsRequired()
             .HasMaxLength(200);

            M.Property(m => m.PhoneNumber)
             .IsRequired()
             .HasMaxLength(11);

            M.Property(m => m.HireDate)
             .IsRequired()
             .HasColumnType("date")
             .HasDefaultValueSql("CAST(GETDATE() AS date)");

            M.HasIndex(m => m.Email).IsUnique();

            M.HasOne(m => m.Branch)
             .WithOne(b => b.Manager)
             .HasForeignKey<Manager>(m => m.BranchCode)
             .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
