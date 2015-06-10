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
    public class EngagementView : MvxTableViewController
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

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;

            var source = new MvxStandardTableViewSource(TableView, "TitleText Description;Client ClientName");
            TableView.Source = source;

            var set = this.CreateBindingSet<EngagementView, EngagementViewModel>();
            set.Bind(source).To(vm => vm.Engagements);
            set.Apply();

            TableView.ReloadData();
        }
    }
}