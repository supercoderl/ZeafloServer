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
    public sealed class PostReactionConfiguration : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> builder)
        {
            builder.HasKey(p => p.PostReactionId);

            builder.Property(p => p.PostId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(p => p.ReactionType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CreatedAt)
                .HasColumnType("timestamp");

            builder.ToTable(tb => tb.HasCheckConstraint("CK_PostReaction_ReactionType", "reaction_type IN ('Like', 'Dislike', 'Love', 'Wow', 'Sad', 'Angry')"));

            builder.HasOne(p => p.Post)
                .WithMany(pr => pr.PostReactions)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_PostReaction_Post_PostId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                .WithMany(pr => pr.PostReactions)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_PostReaction_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
