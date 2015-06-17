using System;
using System.Drawing;
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
   

    [Register("EngTaskView")]
    public class EngTaskView : MvxTableViewController
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

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
                EdgesForExtendedLayout = UIRectEdge.None;

            var source = new MvxStandardTableViewSource(TableView, "TitleText Description;Engagement EngDesc");
            TableView.Source = source;

            var set = this.CreateBindingSet<EngTaskView, EngTaskViewModel>();
            set.Bind(source).To(vm => vm.EngTasks);
            set.Apply();

            TableView.ReloadData();
        }
    }
}