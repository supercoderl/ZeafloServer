using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Errors
{
    public abstract class ErrorCodes
    {
        public const string CommitFailed = "COMMIT_FAILED";
        public const string ObjectNotFound = "OBJECT_NOT_FOUNT";
        public const string InsufficientPermissions = "UNAUTHORIZED";
        public const string TokenExpired = "TOKEN_EXPIRED";
        public const string SendMailFailed = "SEND_MAIL_FAILED";
        public const string ErrorInCreating = "ERROR_CREATING";
        public const string ErrorInDeleting = "ERROR_DELETING";
        public const string ErrorInPredicting = "ERROR_PREDICTING";
        public const string UploadFailed = "UPLOAD_FAILED";
    }
}
