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
    public class BranchConfiguration: IEntityTypeConfiguration<Branch>
    {

        public void Configure(EntityTypeBuilder<Branch> B)
        {
            B.HasKey(b => b.Code);

            B.Property(b => b.Name)
             .IsRequired()
             .HasMaxLength(150);

            B.Property(b => b.Address)
             .IsRequired()
             .HasMaxLength(250);

            B.Property(b => b.PhoneNumber)
             .IsRequired()
             .HasMaxLength(11);




        }
    }
}
