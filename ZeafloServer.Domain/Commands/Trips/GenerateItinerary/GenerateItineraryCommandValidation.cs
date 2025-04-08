using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands.Trips.GenerateItinerary
{
    public sealed class GenerateItineraryCommandValidation : AbstractValidator<GenerateItineraryCommand>
    {
        public GenerateItineraryCommandValidation()
        {
            
        }
    }
}
