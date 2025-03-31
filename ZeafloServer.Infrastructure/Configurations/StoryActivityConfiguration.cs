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
    public sealed class StoryActivityConfiguration : IEntityTypeConfiguration<StoryActivity>
    {
        public void Configure(EntityTypeBuilder<StoryActivity> builder)
        {
            builder.HasKey(s => s.StoryActivityId);

            builder.Property(s => s.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(s => s.ActionType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(s => s.PointEarned)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_StoryActivity_ActionType", "action_type IN ('Send', 'View', 'Receive')"));

            builder.HasOne(u => u.User)
                .WithMany(s => s.StoryActivities)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_StoryActivity_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
