using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using WatchKit;
using Foundation;
using System.IO;
namespace EYAppleWatchPOC.IPhoneWatchKitExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		

		public override void HandleLocalNotificationAction (string identifier, UIKit.UILocalNotification localNotification)
		{
			base.HandleLocalNotificationAction (identifier, localNotification);

			Console.WriteLine ("HandleLocalNotificationAction alertbody:" + localNotification.AlertBody);
		}

		public override void HandleRemoteNotificationAction (string identifier, NSDictionary remoteNotification)
		{
			base.HandleRemoteNotificationAction (identifier, remoteNotification);

			Console.WriteLine ("HandleRemoteNotificationAction count:"  + remoteNotification.Count);
		}

		List<Dictionary<string,string>> engList;
		public InterfaceController (IntPtr handle) : base (handle)
		{
			var appData = NSBundle.MainBundle.PathForResource ("AppData", "json");
			engList = JsonConvert.DeserializeObject<List<Dictionary<string,string>>> (File.ReadAllText (appData));
		}

		void LoadTableRows()
		{
			interfaceTable.SetNumberOfRows ((nint)engList.Count, "default");
			for (var i = 0; i < engList.Count; i++) {
				var	taskRow = (ElementRowController)interfaceTable.GetRowController (i);
				var rowData = engList [i];
				taskRow.ElementLabel.SetText (rowData ["label"]);
				SetSeparator (taskRow.separatorTask,rowData ["color"]);



			}
		}

		void SetSeparator(WKInterfaceSeparator separator,string color)
		{
			string[] rgb = color.Split('~');
			separator.SetColor (UIKit.UIColor.FromRGB(Convert.ToInt32(rgb[0]),Convert.ToInt32(rgb[1]),Convert.ToInt32(rgb[2])));
			//separator.SetColor (UIKit.UIColor.FromRGB(180,213,76));
			//separator.SetHeight (20);
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);
			LoadTableRows ();
			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}

		/*partial void WKInterfaceButton11_Activated (WKInterfaceButton sender)
		{
			var suggest = new string[] {"Get groceries", "Buy gas", "Post letter"};

			PresentTextInputController (suggest, WatchKit.WKTextInputMode.AllowEmoji, (result) => {
				// action when the "text input" is complete
				if (result != null && result.Count > 0) {
					// this only works if result is a text response (Plain or AllowEmoji)
					 string enteredText = result.GetItem<NSObject>(0).ToString();
					Console.WriteLine (enteredText);

					WKInterfaceController.OpenParentApplication(
						new NSDictionary (
							"todoTask", enteredText
						), 
						(replyInfo, error) => {
						if(error != null) {
							Console.WriteLine (error);
							return;
						}
						Console.WriteLine ("parent app responded");
						// do something with replyInfo[] dictionary
					});
					// do something, such as myLabel.SetText(enteredText);
				}
			});


		}*/
	}
}

