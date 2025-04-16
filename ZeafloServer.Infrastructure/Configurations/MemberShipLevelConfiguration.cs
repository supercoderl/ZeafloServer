using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Infrastructure.Configurations
{
    public sealed class MemberShipLevelConfiguration : IEntityTypeConfiguration<MemberShipLevel>
    {
        public void Configure(EntityTypeBuilder<MemberShipLevel> builder)
        {
            builder.HasKey(m => m.MemberShipLevelId);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(m => m.MinPoint)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_MemberShipLevel_Type", "type IN ('Member', 'Silver', 'Gold', 'Diamond')"));
        }
    }
}
