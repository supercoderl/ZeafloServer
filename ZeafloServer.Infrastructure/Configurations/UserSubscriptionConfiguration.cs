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
    public sealed class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.HasKey(us => us.UserSubscriptionId);

            builder.Property(us => us.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(us => us.PlanId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(us => us.StartDate)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.Property(us => us.EndDate)
                .HasColumnType("timestamp");

            builder.Property(us => us.IsTrial)
                .IsRequired()
                .HasColumnType("boolean");

            builder.Property(us => us.PaymentProviderId)
                .HasColumnType("uuid");

            builder.HasOne(u => u.User)
                .WithMany(us => us.UserSubscriptions)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserSubscription_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Plan)
                .WithMany(us => us.UserSubscriptions)
                .HasForeignKey(p => p.PlanId)
                .HasConstraintName("FK_UserSubscription_Plan_PlanId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
