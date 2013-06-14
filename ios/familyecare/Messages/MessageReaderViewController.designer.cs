// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace familyecare
{
	[Register ("MessageReaderViewController")]
	partial class MessageReaderViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel labelDe { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel labelFecha { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView labelMensaje { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonEnviar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (labelDe != null) {
				labelDe.Dispose ();
				labelDe = null;
			}

			if (labelFecha != null) {
				labelFecha.Dispose ();
				labelFecha = null;
			}

			if (labelMensaje != null) {
				labelMensaje.Dispose ();
				labelMensaje = null;
			}

			if (buttonEnviar != null) {
				buttonEnviar.Dispose ();
				buttonEnviar = null;
			}
		}
	}
}
