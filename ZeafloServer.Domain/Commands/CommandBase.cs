using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Commands
{
    public abstract class CommandBase<TResponse> : IRequest<TResponse>
    {
        public Guid AggregateId { get; }
        public string MessageType { get; }
        public DateTime Timestamp { get; }
        public ValidationResult? ValidationResult { get; protected set; }

        protected CommandBase(Guid aggregateId)
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
        }

        public abstract bool IsValid();
    }
}
