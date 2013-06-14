using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace familyecare
{
	[Register]
	public class EventCell : UITableViewCell
	{
		public string Title { 
			get { return this.TextLabel.Text; }
			set { this.TextLabel.Text = value; }
		}
		
		public string Details { 
			get { return this.CellDetails.Text; }
			set { this.CellDetails.Text = value; }
		}
		
		private UILabel CellDetails;
		
		public EventCell (UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier) 
		{    
			//this.ContentView.Bounds.Height = 80f;
			//this.ContentMode = UIViewContentMode.ScaleAspectFill;
			this.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			
			// cell's title label
			this.TextLabel.BackgroundColor = this.BackgroundColor;
			this.TextLabel.Opaque = false;
			//this.TextLabel.TextColor = UIColor.Black;
			//this.TextLabel.HighlightedTextColor = UIColor.White;
			this.TextLabel.Font = UIFont.BoldSystemFontOfSize(16f);
			
			// cell's check button
			
			CellDetails = new UILabel () {
				Font = UIFont.SystemFontOfSize(14f)
			};
			
			ContentView.AddSubview (this.CellDetails);
		}
		
		public override void LayoutSubviews () 
		{
			base.LayoutSubviews ();
			
			this.TextLabel.Frame = new RectangleF (
				this.ContentView.Bounds.Left + 10f, 
				5f, 
				this.ContentView.Bounds.Width, 
				20f);
			
			
			this.CellDetails.Frame = new RectangleF (
				this.ContentView.Bounds.Left + 10f, 
				27f, 
				this.ContentView.Bounds.Width, 
				15f);
			
			this.ContentView.Bounds.Height = 100f;
			
		}
		
	}
}
