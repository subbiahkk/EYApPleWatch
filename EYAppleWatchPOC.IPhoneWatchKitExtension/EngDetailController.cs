using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EYAppleWatchPOC.IPhoneWatchKitExtension
{
	
	partial class EngDetailController : WatchKit.WKInterfaceController
	{
		public EngDetailController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);
		}

		public override void WillActivate ()
		{
			// This method is called when the controller is about to be visible to the wearer.
			Console.WriteLine (string.Format ("{0} will activate", this));
		}

		public override void DidDeactivate ()
		{
			// This method is called when the controller is no longer visible.
			Console.WriteLine (string.Format ("{0} did deactivate", this));
		}
	}
}
