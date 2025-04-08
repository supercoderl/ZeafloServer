using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Shared.Enums
{
    public enum MediaType
    {
        Image,
        Video,
        Gif,
        None
    }

    public enum NotificationType
    {
        Like,
        Comment,
        FriendRequest,
        Message
    }

    public enum PlaceType
    {
        Restaurant,
        Coffee,
        Hotel,
        HomeStay,
        Resort,
        Market,
        Church,
        Museum,
        Tunnel,
        Zoo,
        Park
    }

    public enum LevelType
    {
        Silver,
        Gold,
        Diamond,
    }

    public enum ActionType
    {
        Send,
        View,
        Receive
    }

    public enum AnnotationType
    {
        Text,
        Time,
        Location,
        Weather
    }
}
