// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace EYAppleWatchPOC.IPhone
{
	[Register ("EngagementCell")]
	partial class EngagementCell
	{
        [Outlet]
        UIKit.UILabel ClientLabel { get; set; }

        [Outlet]
        UIKit.UILabel DescriptionLabel { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (ClientLabel != null)
            {
                ClientLabel.Dispose();
                ClientLabel = null;
            }

            if (DescriptionLabel != null)
            {
                DescriptionLabel.Dispose();
                DescriptionLabel = null;
            }
        }
	}
}
