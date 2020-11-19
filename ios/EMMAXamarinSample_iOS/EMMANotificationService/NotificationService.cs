using System;
using Foundation;
using UserNotifications;
using EMMASDK;

namespace EMMANotificationService
{
    [Register("NotificationService")]
    public class NotificationService : UNNotificationServiceExtension
    {
        Action<UNNotificationContent> ContentHandler { get; set; }
        UNMutableNotificationContent BestAttemptContent { get; set; }

        protected NotificationService(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidReceiveNotificationRequest(UNNotificationRequest request, Action<UNNotificationContent> contentHandler)
        {
            ContentHandler = contentHandler;
            BestAttemptContent = (UNMutableNotificationContent)request.Content.MutableCopy();

            EMMA.DidReceiveNotificationRequest(request, BestAttemptContent, (UNNotificationContent) =>
            {
                contentHandler(BestAttemptContent);
            });
        }

        public override void TimeWillExpire()
        {
            // Called just before the extension will be terminated by the system.
            // Use this as an opportunity to deliver your "best attempt" at modified content, otherwise the original push payload will be used.

            TaskComplete();
        }

        private void TaskComplete()
        {
            ContentHandler(BestAttemptContent);
        }
    }
}