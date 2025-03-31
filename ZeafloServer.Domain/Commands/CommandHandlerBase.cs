using MediatR;
using ZeafloServer.Domain.Errors;
using ZeafloServer.Domain.Interfaces;
using ZeafloServer.Domain.Notifications;

namespace ZeafloServer.Domain.Commands
{
    public abstract class CommandHandlerBase<TResponse>
    {
        protected readonly IMediatorHandler Bus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandlerBase(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications
        )
        {
            Bus = bus;
            _unitOfWork = unitOfWork;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected async Task<bool> CommitAsync()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }

            if (await _unitOfWork.CommitAsync())
            {
                return true;
            }

            await Bus.RaiseEventAsync(
                new DomainNotification(
                    "Commit",
                    "Problem occured while saving the data. Please try again.",
                    ErrorCodes.CommitFailed));

            return false;
        }

        protected async Task NotifyAsync(string key, string message, string code)
        {
            await Bus.RaiseEventAsync(new DomainNotification(key, message, code));
        }

        protected async Task NotifyAsync(DomainNotification notification)
        {
            await Bus.RaiseEventAsync(notification);
        }

        protected async ValueTask<bool> TestValidityAsync(CommandBase<TResponse> command)
        {
            if (command.IsValid())
            {
                return true;
            }

            if (command.ValidationResult is null)
            {
                throw new InvalidOperationException("Command is invalid and should therefore have a validation result");
            }

            foreach (var error in command.ValidationResult!.Errors)
            {
                await NotifyAsync(
                    new DomainNotification(
                        command.MessageType,
                        error.ErrorMessage,
                        error.ErrorCode,
                        error.FormattedMessagePlaceholderValues));
            }

            return false;
        }
    }
}
