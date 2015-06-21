// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace EYAppleWatchPOC.IPhone.Views
{
	[Register ("EngTaskView")]
	partial class EngTaskView
	{
		[Outlet]
		UIKit.UIButton btnAddTask { get; set; }

		[Action ("Button_Click:")]
		partial void Button_Click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddTask != null) {
				btnAddTask.Dispose ();
				btnAddTask = null;
			}
		}
	}
}
