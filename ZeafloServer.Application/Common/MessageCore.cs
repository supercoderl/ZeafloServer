using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Domain.Commands.Messages.CreateMessage;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Interfaces.IRepositories;

namespace ZeafloServer.Application.Common
{
    public class MessageCore
    {
        public static async Task<Message?> SendFriendMessage(
            Guid userId, 
            IUserRepository userRepository, 
            IFriendShipRepository friendShipRepository,
            CreateMessageRequest request,
            IMediatorHandler bus
        )
        {
            try
            {
                var user = await userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception("You are not eligible to send messages");
                }

                if (request.MediaUrl != null)
                {


                }

                var userRelation = await friendShipRepository.GetByUserAndFriendAsync(userId, request.ReceiverId);

                if (userRelation == null)
                {
                    throw new Exception("The other party is not your friend");
                }

                return await bus.SendCommandAsync(new CreateMessageCommand(
                    Guid.NewGuid(),
                    userId, 
                    request.ReceiverId, 
                    request.Content, 
                    request.MediaUrl
                ));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*public static void SendGroupMessage(AppDbContext _dbcontext, IMapper _mapper, string userId, GroupMessageInput input)
        {
            var user = _dbcontext.ChatUsers.FirstOrDefault(t => t.Id == userId);
            if (user == null)
            {
                throw new Exception("你没资格发消息");
            }

            var userGroupMap = _dbcontext.ChatGroupMaps.FirstOrDefault(t => t.UserId == input.UserId && t.GroupId == input.GroupId);
            if (userGroupMap == null)
            {
                throw new Exception("群消息发送错误");
            }
            if (input.MessageType == MessageTypeEnum.图片)
            {

            }
            var groupMessage = _mapper.Map<GroupMessageInput, ChatGroupMessage>(input);
            groupMessage.CreateTime = DateTime.Now.ToTimestamp();
            _dbcontext.Add(groupMessage);
            _dbcontext.SaveChanges();

        }*/
    }
}
