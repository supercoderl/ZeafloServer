using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Domain.Entities
{
    public class StoryActivity : Entity<Guid>
    {
        [Column("story_activity_id")]
        public Guid StoryActivityId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("action_type")]
        public ActionType ActionType { get; private set; }

        [Column("point_earned")]
        public int PointEarned { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("StoryActivities")]
        public virtual User? User { get; set; }

        public StoryActivity(
            Guid storyActivityId,
            Guid userId,
            ActionType actionType,
            int pointEarned,
            DateTime createdAt
        ) : base(storyActivityId)
        {
            StoryActivityId = storyActivityId;
            UserId = userId;
            ActionType = actionType;
            PointEarned = pointEarned;
            CreatedAt = createdAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetActionType( ActionType actionType ) { ActionType = actionType; }
        public void SetPointEarned(int pointEarned) { PointEarned = pointEarned; }
        public void SetCreatedAt( DateTime createdAt ) { CreatedAt = createdAt; }
    }
}
