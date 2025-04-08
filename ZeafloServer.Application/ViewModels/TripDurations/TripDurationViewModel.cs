using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.TripDurations
{
    public class TripDurationViewModel
    {
        public Guid TripDurationId { get; set; }    
        public string Label { get; set; } = string.Empty;
        public int Days { get; set; }
        public int Nights { get; set; }

        public static TripDurationViewModel FromTripDuration(TripDuration tripDuration)
        {
            return new TripDurationViewModel
            {
                TripDurationId = tripDuration.TripDurationId,
                Label = tripDuration.Label,
                Days = tripDuration.Days,
                Nights = tripDuration.Nights
            };
        }
    }
}
