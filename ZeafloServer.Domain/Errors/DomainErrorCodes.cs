using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Errors
{
    public static class DomainErrorCodes
    {
        public static class User
        {
            //User Validation
            public const string EmptyId = "USER_EMPTY_ID";
            public const string EmptyUsername = "USER_EMPTY_USER_NAME";
            public const string EmptyEmail = "USER_EMPTY_EMAIL";
            public const string EmptyPhoneNumber = "USER_EMPTY_PHONE_NUMBER";
            public const string EmptyQrUrl = "USER_EMPTY_QR_URL";
            public const string EmptyAvatarBase64String = "USER_EMPTY_BASE_64_STRING";

            //User Password Validation
            public const string EmptyPassword = "USER_PASSWORD_MAY_NOT_BE_EMPTY";
            public const string ShortPassword = "";
            public const string LongPassword = "";
            public const string UppercaseLetterPassword = "";
            public const string LowercaseLetterPassword = "";
            public const string NumberPassword = "";
            public const string SpecialCharPassword = "";

            //General
            public const string AlreadyExists = "USER_ALREADY_EXISTS";
            public const string PasswordIncorrect = "USER_PASSWORD_INCORRECT";
            public const string AccountBlocked = "USER_HAS_BEEN_BLOCKED";
            public const string TokenBuiltError = "USER_TOKEN_BUILT_ERROR";

            //User Change Password Validation
            public const string EmptyOldPassword = "OLD_PASSWORD_MAY_NOT_BE_EMPTY";
            public const string EmptyNewPassword = "NEW_PASSWORD_MAY_NOT_BE_EMPTY";
        }

        public static class Token
        {
            //Token Validation
            public const string EmptyId = "TOKEN_EMPTY_ID";
            public const string EmptyAccessToken = "TOKEN_EMPTY_ACCESS_TOKEN";
            public const string EmptyRefreshToken = "TOKEN_EMPTY_REFRESH_TOKEN";
            public const string TokenInvalid = "TOKEN_IS_INVALID";
        }

        public static class File
        {
            //File Validation
            public const string EmptyPublicId = "FILE_EMPTY_PUBLIC_ID";
            public const string EmptyBase64String = "FILE_EMPTY_BASE_64_STRING";
            public const string EmptyFileName = "FILE_EMPTY_FILE_NAME";
            public const string EmptyFolderName = "FILE_EMPTY_FOLDER_NAME";
        }

        public static class Post
        {
            //Post Validation
            public const string EmptyPostId = "POST_EMPTY_ID";
            public const string EmptyTitle = "POST_EMPTY_TITLE";
            public const string EmptyContent = "POST_EMPTY_CONTENT";
        }

        public static class PostMedia
        {
            //Post Media Validation
            public const string EmptyPostMediaId = "POST_MEDIA_EMPTY_ID";
            public const string EmptyMediaUrl = "POST_MEDIA_EMPTY_MEDIA_URL";
        }

        public static class MapTheme
        {
            //Map Theme Validation
            public const string EmptyMapThemeId = "MAP_THEME_EMPTY_ID";
            public const string EmptyName = "MAP_THEME_EMPTY_NAME";
            public const string EmptyMapStyle = "MAP_THEME_EMPTY_MAP_STYLE";
            public const string EmptyPreviewUrl = "MAP_THEME_EMPTY_PREVIEW_URL";
        }

        public static class FriendShip
        {
            //Friend Ship Validation
            public const string EmptyFriendShipId = "FRIEND_SHIP_EMPTY_ID";
        }

        public static class Message
        {
            //Message Validation
            public const string EmptyMessageId = "MESSAGE_EMPTY_ID";
            public const string EmptyContent = "MESSAGE_EMPTY_CONTENT";
            public const string EmptySenderId = "MESSAGE_EMPTY_SENDER_ID";
            public const string EmptyReceiverId = "MESSAGE_EMPTY_RECEIVER_ID";
        }

        public static class SavePost
        {
            //Save Post Validation
            public const string EmptyUserId = "SAVE_POST_EMPTY_USER_ID";
            public const string EmptyPostId = "SAVE_POST_EMPTY_POST_ID";
        }

        public static class Process
        {
            //Process Validation
            public const string EmptyProcessingId = "PROCESS_EMPTY_PROCESSING_ID";
            public const string EmptyUserId = "PROCESS_EMPTY_USER_ID";
            public const string EmptyType = "PROCESS_EMPTY_TYPE";
        }

        public static class Place
        {
            //Place Validation
            public const string EmptyPlaceId = "PLACE_EMPTY_PLACE_ID";
            public const string EmptyName = "PLACE_EMPTY_NAME";
        }

        public static class City
        {
            //City Validation
            public const string EmptyCityId = "CITY_EMPTY_CITY_ID";
            public const string EmptyName = "CITY_EMPTY_NAME";
            public const string EmptyPostalCode = "CITY_EMPTY_POSTAL_CODE";
        }

        public static class PlaceImage
        {
            //Place Image Validation
            public const string EmptyPlaceImageId = "PLACE_IMAGE_EMPTY_PLACE_IMAGE_ID";
            public const string EmptyImageUrl = "PLACE_IMAGE_EMPTY_IMAGE_URL";
        }

        public static class TripDuration
        {
            //Trip Duration Validation
            public const string EmptyTripDurationId = "TRIP_DURATION_EMPTY_TRIP_DURATION_ID";
            public const string EmptyLabel = "TRIP_DURATION_EMPTY_LABEL";
        }

        public static class Plan
        {
            //Plan Validation
            public const string EmptyPlanId = "PLAN_EMPTY_PLAN_ID";
            public const string EmptyName = "PLAN_EMPTY_NAME";
        }
    }
}
