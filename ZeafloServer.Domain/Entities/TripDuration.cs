using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public class TripDuration : Entity<Guid>
    {
        [Column("trip_duration_id")]
        public Guid TripDurationId { get; private set; }

        [Column("label")]
        public string Label { get; private set; }

        [Column("days")]
        public int Days { get; private set; }

        [Column("nights")]
        public int Nights { get; private set; }

        [InverseProperty("TripDuration")]
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        public TripDuration(
            Guid tripDurationId,
            string label,
            int days,
            int nights
        ) : base(tripDurationId)
        {
            TripDurationId = tripDurationId;
            Label = label;
            Days = days;
            Nights = nights;
        }

        public void SetLabel(string label) { Label = label; }
        public void SetDays(int days) { Days = days; }
        public void SetNights(int nights) { Nights = nights; }
    }
}
