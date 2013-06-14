using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using familyecare.familyecare.com;

namespace familyecare
{
	public partial class EventsViewController : UIViewController
	{
		//private Thread thread;
		public RefreshTableEventsHeaderView _refreshHeaderView;
		public List<EventDto> _events;

		public EventsViewController () : base ("EventsViewController", null)
		{
			Title = "Eventos";
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
			LoadData();

			//Cargamos los datos en un thread para no afectar al rendimiento de la interfaz de usuario
			//thread = new Thread(LoadData as ThreadStart);
			//thread.Start();

			_refreshHeaderView = new RefreshTableEventsHeaderView ();
			_refreshHeaderView.BackgroundColor = new UIColor (226f, 231f, 237f, 1f);
			tableView.Source = new TableViewSource (this);
			tableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			tableView.AddSubview (_refreshHeaderView);
		}

		//[Export("LoadData")]
		private void LoadData()
		{
			var app = UIApplication.SharedApplication;
			app.NetworkActivityIndicatorVisible = true;

			try {
				DeviceServices ds = new DeviceServices();
				EventDto[] events = ds.GetEventsByActive(Device.GetMAC());
				//InvokeOnMainThread (delegate { 
					_events = new List<EventDto>(); 
				//});
				foreach(EventDto evento in events) {
					//InvokeOnMainThread (delegate { 
						_events.Add(evento); 
					//});
				}
			}
			catch (Exception ex)
			{
				using (UIAlertView alert = new UIAlertView ("Atención", "No ha sido posible obtener los datos del servidor. Por favor, inténtelo de nuevo en unos minutos. " + ex.ToString(), null, "OK", null)) {
					alert.Show ();
				}
			}
			finally {
				app.NetworkActivityIndicatorVisible = false;
			}
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;

			tableView.Dispose();
			tableView = null;
			_events.Clear();
			_events = null;

			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		class TableViewSource : UITableViewSource
		{
			static NSString kCellIdentifier = new NSString ("MyIdentifier");
			
			List<EventDto> _list;
			bool _checkForRefresh;
			bool _reloading;
			NSTimer _reloadTimer;
			RefreshTableEventsHeaderView _refreshHeaderView;
			UITableView _table;
			EventsViewController _rtvc;
			
			public TableViewSource (EventsViewController rtvc)
			{
				_list = rtvc._events;
				_checkForRefresh = false;
				_reloading = false;
				_refreshHeaderView = rtvc._refreshHeaderView;
				_table = rtvc.tableView;
				_rtvc = rtvc;
			}
			
			public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
			{
				//_rtvc.ShowMessage(_list[indexPath.Row]);
				using (UIAlertView alert = new UIAlertView(_list[indexPath.Row].Date.ToString(), _list[indexPath.Row].Text, null, "OK", null))
				{
					alert.Show ();
				}
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				//TODO: mostrar el mensaje y marcarlo como leido y actualizarlo en el servidor.
				using (UIAlertView alert = new UIAlertView(_list[indexPath.Row].Date.ToString(), _list[indexPath.Row].Text, null, "OK", null))
				{
					alert.Show ();
				}
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return _list.Count;
			}
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{

					EventCell cell =
						(EventCell)tableView.DequeueReusableCell (
							kCellIdentifier);
					
					cell = new EventCell (
						UITableViewCellStyle.Default,
						kCellIdentifier);
					
					cell.Title = _list[indexPath.Row].Date.ToString();
					cell.Details = _list[indexPath.Row].Text;

				return cell;
			}
			
			#region UIScrollViewDelegate
			[Export("scrollViewDidScroll:")]
			public void Scrolled (UIScrollView scrollView)
			{
				if (_checkForRefresh) {
					if (_refreshHeaderView.isFlipped && (_table.ContentOffset.Y > -65f) && (_table.ContentOffset.Y < 0f) && !_reloading) {
						_refreshHeaderView.FlipImageAnimated (true);
						_refreshHeaderView.SetStatus (RefreshTableEventsHeaderView.RefreshStatus.PullToReloadStatus);
					} else if ((!_refreshHeaderView.isFlipped) && (_table.ContentOffset.Y < -65f)) {
						_refreshHeaderView.FlipImageAnimated (true);
						_refreshHeaderView.SetStatus (RefreshTableEventsHeaderView.RefreshStatus.ReleaseToReloadStatus);
					}
				}
			}
			
			[Export("scrollViewWillBeginDragging:")]
			public void DraggingStarted (UIScrollView scrollView)
			{
				_checkForRefresh = true;
			}
			
			private void ReloadData()
			{
				var app = UIApplication.SharedApplication;
				try {
					app.NetworkActivityIndicatorVisible = true;
					DeviceServices deviceServices = new DeviceServices();
					EventDto[] events = deviceServices.GetEventsByActive(Device.GetMAC());
					_list.Clear();
					foreach (EventDto e in events) {
						_list.Add (e);
					}
				} catch {}
				finally {
					app.NetworkActivityIndicatorVisible = false;
				}
			}
			
			[Export("scrollViewDidEndDragging:willDecelerate:")]
			public void DraggingEnded (UIScrollView scrollView, bool willDecelerate)
			{
				if (_table.ContentOffset.Y <= -60f) {
					
					//ReloadTimer = NSTimer.CreateRepeatingScheduledTimer (new TimeSpan (0, 0, 0, 10, 0), () => dataSourceDidFinishLoadingNewData ());
					_reloadTimer = NSTimer.CreateScheduledTimer (TimeSpan.FromSeconds (2f), delegate {
						
						//Recargamos datos del servicio web
						ReloadData();
						
						_reloadTimer = null;
						_reloading = false;
						_refreshHeaderView.FlipImageAnimated (false);
						_refreshHeaderView.ToggleActivityView ();
						UIView.BeginAnimations ("DoneReloadingData");
						UIView.SetAnimationDuration (0.3);
						_table.ContentInset = new UIEdgeInsets (0f, 0f, 0f, 0f);
						_refreshHeaderView.SetStatus (RefreshTableEventsHeaderView.RefreshStatus.PullToReloadStatus);
						UIView.CommitAnimations ();
						_refreshHeaderView.SetCurrentDate ();
					});
					
					_reloading = true;
					_table.ReloadData ();
					_refreshHeaderView.ToggleActivityView ();
					UIView.BeginAnimations ("ReloadingData");
					UIView.SetAnimationDuration (0.2);
					_table.ContentInset = new UIEdgeInsets (60f, 0f, 0f, 0f);
					UIView.CommitAnimations ();
				}
				
				_checkForRefresh = false;
			}
			
#endregion
		}
		
	}

}