using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Trip;

namespace ZeafloServer.Application.Interfaces
{
    public interface ITripService
    {
        public Task<TripHintResult> GenerateTripHintAsync(Guid cityId, Guid tripDurationId);
    }
}
