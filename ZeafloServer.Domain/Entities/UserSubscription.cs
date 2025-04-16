using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class UserSubscription : Entity<Guid>
    {
        [Column("user_subcription_id")]
        public Guid UserSubscriptionId { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("plan_id")]
        public Guid PlanId { get; private set; }

        [Column("start_date")]
        public DateTime StartDate { get; private set; }

        [Column("end_date")]
        public DateTime? EndDate { get; private set; }

        [NotMapped]
        public bool IsActive => !EndDate.HasValue || EndDate.Value > TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

        [Column("is_trial")]
        public bool IsTrial { get; private set; }

        [Column("payment_provider_id")]
        public Guid? PaymentProviderId { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserSubscriptions")]
        public virtual User? User { get; set; }

        [ForeignKey("PlanId")]
        [InverseProperty("UserSubscriptions")]
        public virtual Plan? Plan { get; set; }

        public UserSubscription(
            Guid userSubscriptionId,
            Guid userId,
            Guid planId,
            DateTime startDate,
            DateTime? endDate,
            bool isTrial,
            Guid? paymentProviderId
        ) : base(userSubscriptionId)
        {
            UserSubscriptionId = userSubscriptionId;
            UserId = userId;
            PlanId = planId;
            StartDate = startDate;
            EndDate = endDate;
            IsTrial = isTrial;
            PaymentProviderId = paymentProviderId;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetPlanId( Guid planId ) { PlanId = planId; }
        public void SetStartDate( DateTime startDate ) { StartDate = startDate; }
        public void SetEndDate( DateTime? endDate ) { EndDate = endDate; }
        public void SetIsTrial( bool isTrial ) { IsTrial = isTrial; }
        public void SetPaymentProviderId( Guid? paymentProviderId ) { PaymentProviderId = paymentProviderId; }
    }
}
