using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using System;
using EYAppleWatchPOC.Core.ViewModels;
using WindowsAzure.Messaging;

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

		private SBNotificationHub Hub{ get; set; }

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
			NSObject todoTask = userInfo[NSObject.FromObject("todoTask")];
			NSObject engId = userInfo[NSObject.FromObject("engId")];
			taskModel.AddTask (todoTask.ToString(),engId.ToString());

		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			Hub = new SBNotificationHub("Endpoint=sb://eyapplewatch.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=WJiTcjnhstX9AdYc/uSq0NlY3fPAqgB3BYvhVBtnmqc=","eypochub");

			Hub.UnregisterAllAsync (deviceToken, (error) => {
				if (error != null)
				{
					Console.WriteLine("Error calling Unregister: {0}", error.ToString());
					return;
				}

				NSSet tags = null; // create tags if you want
				Hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) => {
					if (errorCallback != null)
						Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
				});
			});
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

		void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			// Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey(new NSString("aps")))
			{
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

				string alert = string.Empty;


				if (aps.ContainsKey(new NSString("alert")))
					alert = (aps [new NSString("alert")] as NSString).ToString();

				//If this came from the ReceivedRemoteNotification while the app was running,
				// we of course need to manually process things like the sound, badge, and alert.
				if (!fromFinishedLaunching)
				{
					//Manually show an alert
					if (!string.IsNullOrEmpty(alert))
					{
						UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
						avAlert.Show();
					}
				}
			}
		}


		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary options)
		{

			if (null != options && options.ContainsKey (new NSString ("aps"))) {
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey (new NSString ("aps")) as NSDictionary;

				string alert = string.Empty;


				if (aps.ContainsKey (new NSString ("alert")))
					alert = (aps [new NSString ("alert")] as NSString).ToString ();
			

			var notification = new UILocalNotification();

			// set the fire date (the date time in which it will fire)
			notification.FireDate = NSDate.Now.AddSeconds(3); //DateTime.Now.AddSeconds(10));
			notification.TimeZone = NSTimeZone.DefaultTimeZone;
			// configure the alert stuff
			notification.AlertTitle = "New Task";
			notification.AlertAction = "Alert Action";
				notification.AlertBody = "New Task :" + alert;
				
			notification.SoundName = UILocalNotification.DefaultSoundName;

			notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString("UserInfo for notification"), 
				new NSString("Notification"));

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
			}

			//new UIAlertView("Alert", "!" + "Message", null, "OK", null).Show();
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
				else if(launchOptions.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey)) {
					var remoteNotification =
						launchOptions[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;

					//if(remoteNotification != null)
						//ReceivedRemoteNotification(remoteNotification);
				}
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet ());

				UIApplication.SharedApplication.RegisterUserNotificationSettings (pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
			} else {
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | 
					UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (notificationTypes);
			}


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