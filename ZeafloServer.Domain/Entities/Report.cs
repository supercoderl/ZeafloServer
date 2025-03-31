using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class Report : Entity<Guid>
    {
        [Column("report_id")]
        public Guid ReportId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("post_id")]
        public Guid PostId { get; private set; }

        [Column("reason")]
        public string Reason { get; private set; }

        [Column("status")]
        public ReportStatus Status { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Reports")]
        public virtual User? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Reports")]
        public virtual Post? Post { get; set; }

        public Report(
            Guid reportId,
            Guid userId,
            Guid postId,
            string reason,
            ReportStatus status,
            DateTime createdAt
        ) : base(reportId)
        {
            ReportId = reportId;
            UserId = userId;
            PostId = postId;
            Reason = reason;
            Status = status;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetPostId( Guid postId ) { PostId = postId; }
        public void SetReason( string reason ) { Reason = reason; }
        public void SetStatus( ReportStatus status ) { Status = status; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
