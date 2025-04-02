using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class Processing : Entity<Guid>
    {
        [Column("processing_id")]
        public Guid ProcessingId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("type")]
        public string Type { get; private set; }

        [Column("status")]
        public ProcessStatus Status { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Processing")]
        public virtual User? User { get; set; }

        public Processing(
            Guid processingId,
            Guid userId,
            string type,
            ProcessStatus status,
            DateTime createdAt,
            DateTime? updatedAt
        ) : base(processingId)
        {
            ProcessingId = processingId;
            UserId = userId;
            Type = type;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void SetUserId(Guid userId) { UserId = userId; }
        public void SetType(string type ) { Type = type; }  
        public void SetStatus(ProcessStatus status) { Status = status; }
        public void SetUpdatedAt(DateTime? updatedAt) { UpdatedAt = updatedAt; }
    }
}
