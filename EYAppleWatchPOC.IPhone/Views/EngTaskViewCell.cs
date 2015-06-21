using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using EYAppleWatchPOC.Core.Models;

namespace EYAppleWatchPOC.IPhone
{
	public partial class EngTaskViewCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("EngTaskViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EngTaskViewCell");


		public EngTaskViewCell(IntPtr handle)
			: base(handle)
		{
			this.DelayBind(() =>
				{
					var set = this.CreateBindingSet<EngTaskViewCell, EngTask>();
					set.Bind(DescriptionLabel).To(item => item.Description);
					set.Bind(ClientLabel).To(item =>  item.Type);
					//set.Bind (_loader).To (item => item.volumeInfo.imageLinks.thumbnail); //smallThumbnail);
					set.Apply();
				});
		}

		public static EngTaskViewCell Create()
		{
			return (EngTaskViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
