

namespace ZeafloServer.Shared.Events
{
    public class UploadMessageEvent
    {
        public Guid UserId { get; }
        public string QrBase64 { get; }

        public UploadMessageEvent(
            Guid userId,
            string qrBase64
        )
        {
            UserId = userId;
            QrBase64 = qrBase64;
        }
    }
}
