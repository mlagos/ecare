using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace familyecare
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController () : base ("MainViewController", null)
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
			
			//this.Title = "Family eCare";
			NavigationItem.Title = "Family eCare";
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

		public void LoadMensajesView()
		{
			var messagesViewController = new MessagesViewController ();
			// Pass the selected object to the new view controller.
			NavigationController.PushViewController (messagesViewController, true);
		}

		partial void buttonMensajes_Click (MonoTouch.Foundation.NSObject sender)
		{
			var messagesViewController = new MessagesViewController ();
				NavigationController.PushViewController (messagesViewController, true);
		}
		
		partial void buttonAvisos_Click (MonoTouch.Foundation.NSObject sender)
		{
			var eventsViewController = new EventsViewController();
			NavigationController.PushViewController (eventsViewController, true);
		}
		
		partial void buttonSOS_Click (MonoTouch.Foundation.NSObject sender)
		{
			var sosViewController = new SOSViewController();
			NavigationController.PushViewController(sosViewController, true);
		}
		
	}
}

