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
    public sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.PlanId);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Description);

            builder.Property(p => p.MonthlyPrice)
                .IsRequired();

            builder.Property(p => p.AnnualPrice)
                .IsRequired();

            builder.Property(p => p.MaxSeats)
                .IsRequired();
        }
    }
}
