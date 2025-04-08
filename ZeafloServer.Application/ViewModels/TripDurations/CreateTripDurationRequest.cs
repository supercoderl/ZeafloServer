using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Application.ViewModels.TripDurations
{
    public sealed record CreateTripDurationRequest
    (
        string Label,
        int Days,
        int Nights
    );
}
