using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System;
using ObjCRuntime;
using UIKit;
using EYAppleWatchPOC.Core.ViewModels;
using Foundation;

namespace EYAppleWatchPOC.IPhone.Views
{
    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(RectangleF bounds)
            : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.Red;
        }
    }

    [Register("EngagementView")]
    public class EngagementView : MvxViewController
    {
        public EngagementView()
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
            this.Title = "Engagements";
			View.BackgroundColor = UIColor.White;
            

			//var buttonRect = new UIButton(new RectangleF(0, 75, 85, 30));
			var buttonRect = UIButton.FromType(UIButtonType.System);
			buttonRect.SetTitle ("Refresh!", UIControlState.Normal);
			buttonRect.Frame = new RectangleF(0, 75, 85, 30);

			View.AddSubview (buttonRect);
            var tableView = new UITableView(new RectangleF(0, 105, 1200, 1208), UITableViewStyle.Plain);
            //var tableView = new UITableView(View., UITableViewStyle.Plain);
            Add(tableView);

            tableView.RowHeight = 89;
            tableView.BackgroundColor = UIColor.White;

            var source = new MvxSimpleTableViewSource(tableView, EngagementCell.Key, EngagementCell.Key);
            tableView.Source = source;

            var set = this.CreateBindingSet<EngagementView, EngagementViewModel>();
            set.Bind(source).To(vm => vm.Engagements);
            set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
			set.Bind (buttonRect).To (vm => vm.RefreshCommand);
            set.Apply();

            tableView.ReloadData();
        }


    }
}