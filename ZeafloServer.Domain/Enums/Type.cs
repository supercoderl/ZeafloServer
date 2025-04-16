using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Enums
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
        Member,
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

    public enum ReactionType
    {
        Like,
        Dislike,
        Love,
        Wow,
        Sad,
        Angry
    }

    public enum ResponseType
    {
        Success = 0,
        Error = 1,
        ServerError = 2,
        LoginExpiration = 302,
        ParametersLack = 303,
        TokenExpiration,
        Other
    }

    public enum AnnotationType
    {
        Text,
        Time,
        Location,
        Weather
    }
}
