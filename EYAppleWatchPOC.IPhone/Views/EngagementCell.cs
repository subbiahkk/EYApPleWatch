
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using EYAppleWatchPOC.Core.Models;

namespace EYAppleWatchPOC.IPhone
{
    public partial class EngagementCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("EngagementCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EngagementCell");
       

        public EngagementCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<EngagementCell, Engagement>();
                set.Bind(DescriptionLabel).To(item => item.Description);
                set.Bind(ClientLabel).To(item => item.ClientName);
                //set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
                set.Apply();
            });
        }

        public static EngagementCell Create()
        {
            return (EngagementCell)Nib.Instantiate(null, null)[0];
        }
	}
}

