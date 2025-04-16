using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class Plan : Entity<Guid>
    {
        [Column("plan_id")]
        public Guid PlanId { get; private set; }

        [Column("name")]
        public string Name { get; private set; }

        [Column("description")]
        public string? Description { get; private set; }

        [Column("monthly_price")]
        public double MonthlyPrice { get; private set; }

        [Column("annual_price")]
        public double AnnualPrice { get; private set; }

        [Column("max_seats")]
        public int MaxSeats { get; private set; }

        [InverseProperty("Plan")]
        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

        public Plan(
            Guid planId,
            string name,
            string? description,
            double monthlyPrice,
            double annualPrice,
            int maxSeats
        ) : base( planId )
        {
            PlanId = planId;
            Name = name;
            Description = description;
            MonthlyPrice = monthlyPrice;
            AnnualPrice = annualPrice;
            MaxSeats = maxSeats;
        }

        public void SetName( string name ) { Name = name; }
        public void SetDescription( string? description ) { Description = description; }
        public void SetMonthlyPrice( double monthlyPrice ) { MonthlyPrice = monthlyPrice; }
        public void SetAnnualPrice( double annualPrice ) { AnnualPrice = annualPrice; }
        public void SetMaxSeats( int maxSeats ) { MaxSeats = maxSeats; }
    }
}
