
using System;

using Foundation;
using UIKit;

namespace EYAppleWatchPOC.IPhone
{
	public partial class EngagementCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("EngagementCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EngagementCell");

		public EngagementCell (IntPtr handle) : base (handle)
		{
		}

		public static EngagementCell Create ()
		{
			return (EngagementCell)Nib.Instantiate (null, null) [0];
		}
	}
}

