using System;
using Foundation;
using UserNotifications;

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

            NSDictionary userInfo = BestAttemptContent.UserInfo;
            if (userInfo == null)
            {
                TaskComplete();
                return;
            }

            NSString urlImage = userInfo.ObjectForKey(new NSString("media-attachment")) as NSString;
            if (urlImage == null)
            {
                TaskComplete();
                return;
            }

            NSString typeImage = FileExtensionForMediaUrl(urlImage);
            if (typeImage == null)
            {
                TaskComplete();
                return;
            }

            loadAttachmentForUrlString(urlImage, typeImage, (attachment) => {
                if (attachment != null)
                {

                    BestAttemptContent.Attachments = new UNNotificationAttachment[] { attachment };
                }

                TaskComplete();
            });

        }

        public override void TimeWillExpire()
        {
            // Called just before the extension will be terminated by the system.
            // Use this as an opportunity to deliver your "best attempt" at modified content, otherwise the original push payload will be used.

            TaskComplete();
        }

        private void loadAttachmentForUrlString(NSString url, NSString type, OnAttachmentDownload block)
        {
            NSUrl attachmentURL = new NSUrl(url);

            NSUrlSession session = NSUrlSession.FromConfiguration(NSUrlSessionConfiguration.DefaultSessionConfiguration);
            var downloadTask = session.CreateDownloadTask(attachmentURL, (location, response, error) =>
            {
                if (error != null)
                {
                    Console.Write("Notification Service error: " + error);
                }
                else
                {
                    NSFileManager fileManager = NSFileManager.DefaultManager;
                    NSUrl localUrl = new NSUrl(String.Concat(location.Path, type));
                    fileManager.Move(location, localUrl, out error);

                    if (error != null)
                    {
                        Console.Write("Notification Service error: " + error);
                    }

                    NSError attachmentError = null;
                    UNNotificationAttachment attachment =
                        UNNotificationAttachment.FromIdentifier("", localUrl, new UNNotificationAttachmentOptions(), out attachmentError);

                    if (attachmentError != null)
                    {
                        Console.Write("Notification Service error: " + attachmentError);
                    }

                    block(attachment);
                }
            });

            downloadTask.Resume();
        }

        private void TaskComplete()
        {
            ContentHandler(BestAttemptContent);
        }

        private NSString FileExtensionForMediaUrl(NSString url)
        {
            NSString ext = null;

            if (url.Contains(new NSString("emma.io")))
            {
                ext = new NSString("png");
            }
            else if (url.Contains(new NSString("jpg")))
            {
                ext = new NSString("jpg");
            }
            else if (url.Contains(new NSString("jpeg")))
            {
                ext = new NSString("jpg");
            }
            else if (url.Contains(new NSString("png")))
            {
                ext = new NSString("png");
            }
            else if (url.Contains(new NSString("gif")))
            {
                ext = new NSString("gif");
            }

            if (ext != null)
            {
                ext = new NSString(String.Concat(".", ext));
            }

            return ext;
        }

        delegate void OnAttachmentDownload(UNNotificationAttachment attachment);
    }
}
