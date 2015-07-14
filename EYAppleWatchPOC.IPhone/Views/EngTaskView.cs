
using System;

using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System;
using ObjCRuntime;
using UIKit;
using EYAppleWatchPOC.Core.ViewModels;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace EYAppleWatchPOC.IPhone.Views
{
	public partial class EngTaskView : MvxViewController
	{
		public EngTaskView()
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}



		public override void ViewDidLoad()
		{
			

			base.ViewDidLoad();
			this.Title = "Tasks";

			EngTaskViewModel viewModel = (EngTaskViewModel)this.ViewModel;

			if (viewModel.EngTasks.Where (s => s.IsNew == true).ToList().Count > 0) {
				var notification = new UILocalNotification();

				// set the fire date (the date time in which it will fire)
				notification.FireDate = NSDate.Now.AddSeconds(3); //DateTime.Now.AddSeconds(10));
				notification.TimeZone = NSTimeZone.DefaultTimeZone;
				// configure the alert stuff
				notification.AlertTitle = "New Task";
				notification.AlertAction = "Alert Action";
				notification.AlertBody = "New Task :" +
				viewModel.EngTasks.Where (s => s.IsNew == true).ToList () [0].Description
					+ " Added to  " + viewModel.Engagement.Description;
				notification.SoundName = UILocalNotification.DefaultSoundName;

				notification.UserInfo = NSDictionary.FromObjectAndKey (new NSString("UserInfo for notification"), 
					new NSString("Notification"));

				UIApplication.SharedApplication.ScheduleLocalNotification(notification);
			}

			var tableView = new UITableView(new RectangleF(0, 0, 1200, 1208), UITableViewStyle.Plain);
			//var tableView = new UITableView(View., UITableViewStyle.Plain);
			Add(tableView);

			tableView.RowHeight = 89;
			tableView.BackgroundColor = UIColor.White;

			var source = new MvxSimpleTableViewSource(tableView, EngTaskViewCell.Key, EngTaskViewCell.Key);
			tableView.Source = source;

			var set = this.CreateBindingSet<EngTaskView, EngTaskViewModel>();
			set.Bind(source).To(vm => vm.EngTasks);
			//set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
			set.Apply();

			tableView.ReloadData();
		}
	}
}