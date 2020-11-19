using Foundation;
using UIKit;
using System;
using EMMASDK;
using ObjCRuntime;
using UserNotifications;

namespace iOSExample
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public readonly PushNotificationDelegate notificationDelegate = new PushNotificationDelegate();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            EMMA.SetDebuggerOutput(true);
            EMMA.StartSession("emmaxamarinMA2E6IUjG");

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                //Disable or enable push alert
                EMMA.SetPushSystemOptions(EMMAPushSystemOptions.PushSystemDisableAlert);

                EMMA.SetPushNotificationsDelegate(notificationDelegate);
            }

            if (Runtime.Arch != Arch.SIMULATOR)
            {
                EMMA.StartPushSystem();
            }


            if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
            {
                EMMA.RequestTrackingWithIdfa();
            }


            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        /// PUSH DELEGATES
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            EMMA.RegisterToken(deviceToken);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            EMMA.HandlePush(userInfo);
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            // Handle the error
        }

        public class PushNotificationDelegate : UNUserNotificationCenterDelegate
        {

            public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
            {
                EMMA.HandlePush(notification.Request.Content.UserInfo);
                completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Badge);
            }

            public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
            {
                EMMA.HandlePush(response.Notification.Request.Content.UserInfo);
                completionHandler();
            }
        }

    }
}



