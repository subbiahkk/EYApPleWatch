using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using System;
using EYAppleWatchPOC.Core.ViewModels;

namespace EYAppleWatchPOC.IPhone
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        UIWindow window;

        UIWindow _window;

		// class-level declarations
		public override void HandleWatchKitExtensionRequest
		(UIApplication application, NSDictionary userInfo, Action<NSDictionary> reply)
		{
			var status = 2;
			// do something in the background, and respond
			reply (new NSDictionary (
				"count", NSNumber.FromInt32 ((int)status),
				"value2", new NSString("some-info")
			));

			EngTaskViewModel taskModel = new EngTaskViewModel ();
			NSObject value = userInfo[NSObject.FromObject("todoTask")];
			taskModel.AddTask (value.ToString(),1);

		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			// show an alert
			new UIAlertView(notification.AlertAction, "!" + notification.AlertBody, null, "OK", null).Show();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		// iOS 8
		public override void HandleAction (UIApplication application, string actionIdentifier, UILocalNotification localNotification, Action completionHandler)
		{
			// show an alert
			new UIAlertView(localNotification.AlertAction, "?" + localNotification.AlertBody, null, "OK", null).Show();

			// reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}


		public override bool FinishedLaunching(UIApplication app, NSDictionary launchOptions)
        {
			if (launchOptions != null)
			{
				// check for a local notification
				if (launchOptions.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					var localNotification = launchOptions[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null)
					{
						new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
						// reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

			var settings = UIUserNotificationSettings.GetSettingsForTypes(
				UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound
				, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
			UIApplication.SharedApplication.RegisterForRemoteNotifications ();

            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, _window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            _window.MakeKeyAndVisible();

            return true;
        }
    }
}