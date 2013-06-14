// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace familyecare
{
	[Register ("MessageReplyViewController")]
	partial class MessageReplyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton buttonEnviar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView textBoxMessage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonEnviar != null) {
				buttonEnviar.Dispose ();
				buttonEnviar = null;
			}

			if (textBoxMessage != null) {
				textBoxMessage.Dispose ();
				textBoxMessage = null;
			}
		}
	}
}
