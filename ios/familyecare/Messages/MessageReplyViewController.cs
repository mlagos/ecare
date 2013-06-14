using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using familyecare.familyecare.com;

namespace familyecare
{
	public partial class MessageReplyViewController : UIViewController
	{
		public CustomMessageDto message;

		public MessageReplyViewController () : base ("MessageReplyViewController", null)
		{
			Title = "Respuesta";
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

		}

		[Action ("buttonEnviar_Click:")]
		private void buttonEnviar_Click (MonoTouch.Foundation.NSObject sender)
		{
			if (message != null)
			{
				if (textBoxMessage.Text.Trim().Length>0)
				{
					try
					{
						DeviceServices ds = new DeviceServices();
						ReplyDto reply = new ReplyDto();
						reply.IdMessage = message.IdMessage;
						reply.Date = DateTime.Now;
						reply.IdReply = -1;
						reply.Text = textBoxMessage.Text.Trim();

						ds.ReplyMessage(reply);
						//this.ParentViewController.LoadView();
						NavigationController.PopViewControllerAnimated(true);
					}
					catch
					{
						using (UIAlertView alert = new UIAlertView("Error", "No se ha podido enviar el mensaje, por favor, inténtelo más tarde.", null, "OK", null))
						{
							alert.Show();
						}
					}
				}
				else{
					using (UIAlertView alert = new UIAlertView("Error", "El mensaje está vacío.", null, "OK", null))
					{
						alert.Show();
					}
				}
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
	}
}

