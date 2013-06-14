// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace familyecare
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton buttonMensajes { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonAvisos { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonSOS { get; set; }

		[Action ("buttonMensajes_ClickUp:")]
		partial void buttonMensajes_Click (MonoTouch.Foundation.NSObject sender);

		[Action ("buttonAvisos_ClickUp:")]
		partial void buttonAvisos_Click (MonoTouch.Foundation.NSObject sender);

		[Action ("buttonSOS_Click:")]
		partial void buttonSOS_Click (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonMensajes != null) {
				buttonMensajes.Dispose ();
				buttonMensajes = null;
			}

			if (buttonAvisos != null) {
				buttonAvisos.Dispose ();
				buttonAvisos = null;
			}

			if (buttonSOS != null) {
				buttonSOS.Dispose ();
				buttonSOS = null;
			}
		}
	}
}
