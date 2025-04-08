using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Places;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Application.ViewModels.Trip
{
    public class TripHintResult
    {
        public Schedule Schedule { get; set; }
        public List<DailyItinerary> DailyItineraries { get; set; }
    }

    public class DailyItinerary
    {
        public int Day { get; set; }
        public List<ScheduledPlace> ScheduledPlaces { get; set; }
        public string Description { get; set; }
    }

    public class PlaceInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class ScheduledPlace
    {
        public PlaceInfo Place { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Formatted time for display
        public string FormattedTimeSlot => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }
}
