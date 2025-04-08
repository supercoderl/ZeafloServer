using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands.Trips.GenerateItinerary
{
    public sealed class GenerateItineraryCommandHandler : CommandHandlerBase<bool>, IRequestHandler<GenerateItineraryCommand, bool>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ITripDurationRepository _tripDurationRepository;
        private readonly IPlaceRepository _placeRepository;

        public GenerateItineraryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICityRepository cityRepository,
            ITripDurationRepository tripDurationRepository,
            IPlaceRepository placeRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _cityRepository = cityRepository;
            _tripDurationRepository = tripDurationRepository;
            _placeRepository = placeRepository;
        }

        public Task<bool> Handle(GenerateItineraryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /*public async Task<TripHintResponse> GenerateTripHintAsync(Guid cityId, Guid tripDurationId, string messageType)
        {
            try
            {
                // Get city and trip duration info
                var city = await _cityRepository.GetByIdAsync(cityId);

                var tripDuration = await _tripDurationRepository.GetByIdAsync(tripDurationId);

                if (city == null || tripDuration == null)
                {
                    await NotifyAsync(new DomainNotification(
                        messageType,
                        $"City or trip duration not found for ID: {cityId}, {tripDurationId}.",
                        ErrorCodes.ObjectNotFound
                    ));
                }

                // Get places in this city
                var places = await _placeRepository
                    .QueryByCity(cityId)
                    .OrderByDescending(p => p.Rating)
                    .Take(20) // Limit to top 20 places by rating
                    .ToListAsync();

                // Build AI prompt with context about the destination
                var prompt = BuildPromptForTripGeneration(city!, tripDuration!, places);

                // Call AI service to generate the trip hint
                var aiResponseData = await CallAIServiceAsync(prompt);

                // Process AI response into our TripHintResponse object
                return ProcessAIResponse(aiResponseData, city!, tripDuration!, places);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating trip hint for city {CityId}, duration {TripDurationId}", cityId, tripDurationId);
                throw;
            }
        }

        private string BuildPromptForTripGeneration(City city, TripDuration tripDuration, List<Place> places)
        {
            var placeInfoJson = JsonSerializer.Serialize(places.Select(p => new
            {
                id = p.PlaceId,
                name = p.Name,
                type = p.Type.ToString(),
                rating = p.Rating,
                reviewCount = p.ReviewCount,
                isOpen = p.IsOpen
            }));

            var prompt = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are a travel planning assistant that creates detailed trip itineraries in JSON format."
                    },
                    new
                    {
                        role = "user",
                        content = $"Create a detailed trip to {city.Name} for {tripDuration.Days} days and {tripDuration.Nights} nights. " +
                                  $"The trip should include accommodations, meals, attractions, and activities spaced throughout the days. " +
                                  $"Include practical tips about weather, what to pack, and local cuisine. " +
                                  $"Here are some available places in {city.Name} that you can include: {placeInfoJson}. " +
                                  $"Format your response as a complete JSON object that follows this structure: " +
                                  "{ \"cityName\": string, \"duration\": string, \"description\": string, \"weatherTip\": string, " +
                                  "\"packingTips\": string[], \"localCuisine\": string[], \"schedule\": [{ \"day\": number, \"activities\": " +
                                  "[{ \"time\": string, \"activity\": string, \"place\": { \"id\": string, \"name\": string, \"type\": string, " +
                                  "\"description\": string, \"rating\": number }, \"description\": string }] }] }"
                    }
                },
                max_tokens = 4000,
                temperature = 0.7
            };

            return JsonSerializer.Serialize(prompt);
        }

        private async Task<string> CallAIServiceAsync(string prompt)
        {
            // Set up the HTTP request to the AI service
            var request = new HttpRequestMessage(HttpMethod.Post, _apiEndpoint)
            {
                Content = new StringContent(prompt, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            // Send the request to the AI service
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        private TripHintResponse ProcessAIResponse(string aiResponseData, City city, TripDuration tripDuration, List<Place> places)
        {
            try
            {
                // Extract the JSON content from the AI response
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(aiResponseData);

                // The structure will depend on your AI service response format
                // This assumes the response has a "choices" array with a "message" property containing "content"
                var contentJson = responseObject["choices"][0].GetProperty("message").GetProperty("content").GetString();

                // Parse the content JSON into our TripHintResponse object
                var tripHint = JsonSerializer.Deserialize<TripHintResponse>(contentJson);

                // Ensure we have the correct city and duration
                tripHint.CityName = city.Name;
                tripHint.Duration = $"{tripDuration.Days} day {tripDuration.Nights} night";

                // Try to match places from our database
                foreach (var day in tripHint.Schedule)
                {
                    foreach (var activity in day.Activities)
                    {
                        if (activity.Place != null)
                        {
                            // Try to find a matching place in our database
                            var matchingPlace = places.FirstOrDefault(p =>
                                p.Name.Equals(activity.Place.Name, StringComparison.OrdinalIgnoreCase));

                            if (matchingPlace != null)
                            {
                                activity.Place.Id = matchingPlace.PlaceId;
                                activity.Place.Rating = matchingPlace.Rating;
                            }
                        }
                    }
                }

                return tripHint;
            }
            catch (Exception ex)
            {
                // Fallback to a basic trip hint if parsing fails
                return CreateFallbackTripHint(city, tripDuration);
            }
        }

        private TripHintResponse CreateFallbackTripHint(City city, TripDuration tripDuration)
        {
            // Create a basic trip hint if AI processing fails
            return new TripHintResponse
            {
                CityName = city.Name,
                Duration = $"{tripDuration.Days} day {tripDuration.Nights} night",
                Description = $"A wonderful trip to {city.Name} for {tripDuration.Days} days and {tripDuration.Nights} nights.",
                WeatherTip = "Check local weather before your trip.",
                PackingTips = new List<string> { "Comfortable shoes", "Camera", "Weather-appropriate clothing" },
                LocalCuisine = new List<string> { "Try local specialties" },
                Schedule = new List<DaySchedule>
                {
                    new DaySchedule
                    {
                        Day = 1,
                        Activities = new List<ActivityItem>
                        {
                            new ActivityItem
                            {
                                Time = "09:00 - 12:00",
                                Activity = "Explore local attractions",
                                Description = "Visit the main sights in the area."
                            }
                        }
                    }
                }
            };
        }*/
    }
}
