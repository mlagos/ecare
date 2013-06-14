using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using familyecare.familyecare.com;

namespace familyecare
{
	public partial class MessageReaderViewController : UIViewController
	{
		public CustomMessageDto message;

		public MessageReaderViewController () : base ("MessageReaderViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			if (message != null)
			{
				labelDe.Text = message.SendBy;
				labelFecha.Text = message.Date.ToString();
				labelMensaje.Text = message.Text;
			}
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		[Action ("buttonEnviar_Click:")]
		private void buttonEnviar_Click (MonoTouch.Foundation.NSObject sender)
		{
			var messageReplyViewController = new MessageReplyViewController ();
			messageReplyViewController.message = message;
			// Pass the selected object to the new view controller.
			NavigationController.PushViewController (
				messageReplyViewController,
				true
				);
		}
	}
}

