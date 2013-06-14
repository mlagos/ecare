// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace familyecare
{
	[Register ("SOSViewController")]
	partial class SOSViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel labelStatus { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonCancel { get; set; }

		[Action ("buttonCancel_Click:")]
		partial void buttonCancel_Click (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (labelStatus != null) {
				labelStatus.Dispose ();
				labelStatus = null;
			}

			if (buttonCancel != null) {
				buttonCancel.Dispose ();
				buttonCancel = null;
			}
		}
	}
}
