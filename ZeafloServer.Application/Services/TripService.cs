using Microsoft.EntityFrameworkCore;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels.Trip;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Services
{
    public class TripService : ITripService
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly ITripDurationRepository _tripDurationRepository;
        private readonly ICityRepository _cityRepository;
        private readonly Random _random;

        public TripService(
            IPlaceRepository placeRepository,
            ITripDurationRepository tripDurationRepository,
            ICityRepository cityRepository
        )
        {
            _placeRepository = placeRepository;
            _tripDurationRepository = tripDurationRepository;
            _cityRepository = cityRepository;
            _random = new Random();
        }

        public async Task<TripHintResult> GenerateTripHintAsync(Guid cityId, Guid tripDurationId)
        {
            // Get city and duration info
            var city = await _cityRepository.GetByIdAsync(cityId);
            var tripDuration = await _tripDurationRepository.GetByIdAsync(tripDurationId);

            if (city == null || tripDuration == null)
                throw new ArgumentException("City or trip duration not found");

            // Calculate how many places to visit based on duration
            int totalPlaces = CalculatePlaceCount(tripDuration.Days, tripDuration.Nights);

            // Get places in the city sorted by rating
            var placesInCity = await _placeRepository.QueryByCity(cityId)
                .Include(x => x.PlaceImages)
                .OrderByDescending(p => p.Rating)
                .ToListAsync();

            // Ensure we have different types of places (diversity)
            var selectedPlaces = SelectDiversePlaces(placesInCity, totalPlaces);

            // Create a schedule
            var schedule = new Schedule
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                cityId,
                $"Trip to {city.Name} - {tripDuration.Label}",
                tripDurationId,
                DateTime.UtcNow,
                 $"AI generated trip hint for {tripDuration.Days} day(s) and {tripDuration.Nights} night(s)"
            );

            // Create daily itineraries
            var dailyItineraries = CreateDailyItineraries(selectedPlaces, tripDuration.Days);

            return new TripHintResult
            {
                Schedule = schedule,
                DailyItineraries = dailyItineraries
            };
        }

        private int CalculatePlaceCount(int days, int nights)
        {
            // Logic to determine places based on duration
            if (days == 1 && nights == 1) return 5; // 5-6 places for 1 day 1 night
            if (days == 2 && nights == 1) return 12; // 12-15 places for 2 days 1 night
            if (days == 3 && nights == 2) return 21; // 21 places for 3 days 2 nights

            // Default formula: approximately 5-7 places per day
            return days * 6;
        }

        private List<Place> SelectDiversePlaces(List<Place> allPlaces, int count)
        {
            var result = new List<Place>();
            var desiredTypes = new[] {
                PlaceType.Coffee, PlaceType.Restaurant, PlaceType.Market,
                PlaceType.Church, PlaceType.Museum, PlaceType.Tunnel,
                PlaceType.Zoo, PlaceType.Park
            };

            // First, try to include at least one of each desired type
            foreach (var type in desiredTypes)
            {
                var place = allPlaces.FirstOrDefault(p => p.Type == type && !result.Contains(p));
                if (place != null)
                    result.Add(place);

                if (result.Count >= count)
                    return result;
            }

            // If we still need more places, add highest rated remaining places
            foreach (var place in allPlaces)
            {
                if (!result.Contains(place))
                    result.Add(place);

                if (result.Count >= count)
                    break;
            }

            return result;
        }

        private List<DailyItinerary> CreateDailyItineraries(List<Place> places, int days)
        {
            var result = new List<DailyItinerary>();
            int remainingPlaces = places.Count;
            int currentPlaceIndex = 0;

            for (int day = 0; day < days; day++)
            {
                bool isFinalDay = (day == days - 1);

                // First day gets 7 places, other days get 6 places
                int placesForThisDay = (day == 0) ? 7 : 6;
                int dailyPlaceCount = Math.Min(placesForThisDay, remainingPlaces);

                // Get places for today
                var dailyPlaces = places
                    .Skip(currentPlaceIndex)
                    .Take(dailyPlaceCount)
                    .ToList();

                // Order places by a logical visiting sequence with time slots
                var scheduledPlaces = OrderPlacesByVisitingSequence(dailyPlaces, day + 1);

                // Generate description
                string description;
                if (isFinalDay)
                {
                    description = GenerateFinalDayDescription(day + 1, scheduledPlaces);
                }
                else
                {
                    description = GenerateRegularDayDescription(day + 1, scheduledPlaces);
                }

                result.Add(new DailyItinerary
                {
                    Day = day + 1,
                    ScheduledPlaces = scheduledPlaces,
                    Description = description
                });

                // Update counters for next day
                currentPlaceIndex += dailyPlaceCount;
                remainingPlaces -= dailyPlaceCount;
            }

            return result;
        }

        private string GenerateRegularDayDescription(int day, List<ScheduledPlace> scheduledPlaces)
        {
            // Generate a human-readable description of all places for this day
            string dayName = day == 1 ? "first" : day == 2 ? "second" : "third";

            var description = $"On your {dayName} day, start at {scheduledPlaces[0].FormattedTimeSlot} with {scheduledPlaces[0].Place.Name}. ";

            if (scheduledPlaces.Count > 2)
            {
                var midActivities = scheduledPlaces.Skip(1).Take(scheduledPlaces.Count - 2).ToList();
                var midActivitiesText = string.Join(", then ", midActivities.Select(sp =>
                    $"{sp.Place.Name} ({sp.FormattedTimeSlot})"));
                description += $"Then explore {midActivitiesText} ";
            }

            if (scheduledPlaces.Count > 1)
            {
                var last = scheduledPlaces.Last();
                description += $"and end your activities at {last.Place.Name} ({last.FormattedTimeSlot}). ";
            }

            description += "Return to your accommodation for rest.";

            return description;
        }

        private string GenerateFinalDayDescription(int day, List<ScheduledPlace> scheduledPlaces)
        {
            // Generate final day description with all places
            string dayName = day == 1 ? "first" : day == 2 ? "second" : "third";

            var description = $"On your {dayName} day, start at {scheduledPlaces[0].FormattedTimeSlot} with {scheduledPlaces[0].Place.Name}. ";

            if (scheduledPlaces.Count > 2)
            {
                var midActivities = scheduledPlaces.Skip(1).Take(scheduledPlaces.Count - 2).ToList();
                var midActivitiesText = string.Join(", then ", midActivities.Select(sp =>
                    $"{sp.Place.Name} ({sp.FormattedTimeSlot})"));
                description += $"Then explore {midActivitiesText} ";
            }

            if (scheduledPlaces.Count > 1)
            {
                var last = scheduledPlaces.Last();
                description += $"and finish your trip at {last.Place.Name} ({last.FormattedTimeSlot}). ";
            }

            description += "Head back home to conclude your journey.";

            return description;
        }

        private List<ScheduledPlace> OrderPlacesByVisitingSequence(List<Place> places, int dayNumber)
        {
            var result = new List<ScheduledPlace>();
            TimeSpan currentTime;

            // Start earlier on the first day (assuming traveler is already in the city)
            // Start a bit later on subsequent days
            if (dayNumber == 1)
                currentTime = new TimeSpan(6, 30, 0); // 6:30 AM
            else
                currentTime = new TimeSpan(7, 0, 0);  // 7:00 AM

            // Copy the places list so we can modify it
            var remainingPlaces = new List<Place>(places);

            // Early morning - Coffee
            var coffee = remainingPlaces.FirstOrDefault(p => p.Type == PlaceType.Coffee);
            if (coffee != null)
            {
                var endTime = currentTime.Add(new TimeSpan(0, 30, 0)); // 30 minutes for coffee
                result.Add(new ScheduledPlace
                {
                    Place = new PlaceInfo
                    {
                        PlaceId = coffee.PlaceId,
                        Name = coffee.Name,
                        Address = coffee.Address,
                        ImageUrl = coffee.PlaceImages.FirstOrDefault()?.ImageUrl
                    },
                    StartTime = currentTime,
                    EndTime = endTime
                });
                remainingPlaces.Remove(coffee);
                currentTime = endTime.Add(new TimeSpan(0, 15, 0)); // Add 15 min travel time
            }

            // Process all remaining places
            while (remainingPlaces.Any())
            {
                // Priority for specific place types at appropriate times
                Place? place = null;

                // Around lunch time, prioritize restaurant
                if (currentTime >= new TimeSpan(11, 30, 0) && currentTime <= new TimeSpan(13, 30, 0))
                {
                    place = remainingPlaces.FirstOrDefault(p => p.Type == PlaceType.Restaurant);
                }
                // Late afternoon/evening, prioritize restaurants again for dinner
                else if (currentTime >= new TimeSpan(17, 30, 0))
                {
                    place = remainingPlaces.FirstOrDefault(p => p.Type == PlaceType.Restaurant);
                }

                // If no specific type was prioritized or found, just take the next place
                if (place == null)
                {
                    place = remainingPlaces.FirstOrDefault();
                }

                if (place != null)
                {
                    var duration = GetEstimatedDuration(place.Type);
                    var endTime = currentTime.Add(duration);

                    result.Add(new ScheduledPlace
                    {
                        Place = new PlaceInfo
                        {
                            PlaceId = place.PlaceId,
                            Name = place.Name,
                            Address = place.Address,
                            ImageUrl = place.PlaceImages.FirstOrDefault()?.ImageUrl
                        },
                        StartTime = currentTime,
                        EndTime = endTime
                    });

                    remainingPlaces.Remove(place);
                    currentTime = endTime.Add(new TimeSpan(0, 20, 0)); // Add 20 min travel time
                }
            }

            return result;
        }

        private TimeSpan GetEstimatedDuration(PlaceType type)
        {
            // Duration in hours and minutes
            switch (type)
            {
                case PlaceType.Coffee:
                    return new TimeSpan(0, 30, 0); // 30 minutes
                case PlaceType.Restaurant:
                    return new TimeSpan(1, 30, 0); // 1.5 hours
                case PlaceType.Market:
                    return new TimeSpan(1, 0, 0);  // 1 hour
                case PlaceType.Church:
                    return new TimeSpan(0, 45, 0); // 45 minutes
                case PlaceType.Museum:
                    return new TimeSpan(1, 30, 0); // 1.5 hours
                case PlaceType.Tunnel:
                    return new TimeSpan(1, 0, 0);  // 1 hour
                case PlaceType.Zoo:
                    return new TimeSpan(2, 0, 0);  // 2 hours
                case PlaceType.Park:
                    return new TimeSpan(1, 30, 0); // 1.5 hours
                default:
                    return new TimeSpan(1, 0, 0);  // Default 1 hour
            }
        }
    }
}
