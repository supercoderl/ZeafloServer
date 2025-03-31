using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task RaiseEventAsync<T>(T @event) where T : DomainEvent;

        Task<TResponse> SendCommandAsync<TResponse>(CommandBase<TResponse> command);

        Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query);
    }
}
