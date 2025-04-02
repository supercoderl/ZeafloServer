using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Commands.Processes.CreateProcess;
using ZeafloServer.Domain.Commands.Processes.UpdateProcess;
using ZeafloServer.Domain.Enums;
using ZeafloServer.Domain.Interfaces;

namespace ZeafloServer.Domain.Helpers
{
    public sealed class ProcessingHelpers
    {
        public async Task<Entities.Processing?> StartProcess(IMediatorHandler bus, Guid userId, string type)
        {
            return await bus.SendCommandAsync(new CreateProcessCommand(
                        Guid.NewGuid(),
                        userId,
                        type,
                        Enums.ProcessStatus.InProgress
            ));
        }

        public async Task<Entities.Processing?> UpdateProcess(IMediatorHandler bus, Entities.Processing processing, ProcessStatus status)
        {
            return await bus.SendCommandAsync(new UpdateProcessCommand(
                       processing.ProcessingId,
                       processing.UserId,
                       processing.Type,
                       status
            ));
        }
    }
}
