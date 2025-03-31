using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.ViewModels.Messages;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.FriendShips
{
    public sealed class FriendShipViewModel
    {
        public Guid FriendShipId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public required ContactInfo Info { get; set; }
        public List<MessageViewModel>? Messages { get; set; }

        public static FriendShipViewModel FromFriendShip(
            FriendShip friendShip, 
            Guid userId,
            ContactInfo info,
            List<MessageViewModel>? messages
        )
        {
            return new FriendShipViewModel
            {
                UserId = userId,
                FriendShipId = friendShip.FriendShipId,
                CreatedAt = DateTime.Now,
                Info = info,
                Messages = messages
            };
        }
    }

    public sealed class ContactInfo
    {
        public Guid FriendId { get; set; }
        public required string Username { get; set; }
        public required string Fullname { get; set; }
        public required string AvatarUrl { get; set; }
        public FriendShipStatus Status { get; set; }
    }
}
