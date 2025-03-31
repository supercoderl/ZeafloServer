using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Grpc.Interfaces;
using ZeafloServer.Proto.Messages;
using ZeafloServer.Shared.Messages;

namespace ZeafloServer.Grpc.Contexts
{
    public class MessagesContext : IMessagesContext
    {
        private readonly MessagesApi.MessagesApiClient _client;

        public MessagesContext(MessagesApi.MessagesApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MessageViewModel>> GetByIds(IEnumerable<Guid> ids)
        {
            var request = new GetMessagesByIdsRequest();

            request.Ids.AddRange(ids.Select(id => id.ToString()));

            var result = await _client.GetByIdsAsync(request);

            return result.Messages.Select(message => new MessageViewModel(
                Guid.Parse(message.Id),
                Guid.Parse(message.SenderId),
                Guid.Parse(message.ReceiverId),
                message.Content,
                message.MediaUrl,
                message.IsRead
            ));
        }
    }
}
