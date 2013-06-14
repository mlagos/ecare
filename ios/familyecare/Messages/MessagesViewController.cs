using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using familyecare.familyecare.com;

namespace familyecare
{
	public partial class MessagesViewController : UIViewController
	{
		public RefreshTableHeaderView _refreshHeaderView;
		public List<CustomMessageDto> _mensajes;

		public MessagesViewController () : base ("MessagesViewController", null)
		{
			Title = "Mensajes";
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		void LoadData()
		{
			var app = UIApplication.SharedApplication;
			app.NetworkActivityIndicatorVisible = true;
			//activityIndicator.StartAnimating();
			_mensajes = new List<CustomMessageDto> ();
			try {
				DeviceServices deviceServices = new DeviceServices();
				CustomMessageDto[] mensajes = deviceServices.GetMessages(Device.GetMAC());
				foreach (CustomMessageDto m in mensajes) {
					_mensajes.Add (m);
				}
			}
			catch (Exception ex) {
				//activityIndicator.StopAnimating();
				using (UIAlertView alert = new UIAlertView ("Atención", "No ha sido posible obtener los datos del servidor. Por favor, inténtelo de nuevo en unos minutos. " + ex.ToString(), null, "OK", null)) {
					alert.Show ();
				}
			}
			//activityIndicator.StopAnimating();
			app.NetworkActivityIndicatorVisible = false;
		}

		public void ShowMessage(CustomMessageDto message)
		{
			var messageReaderViewController = new MessageReaderViewController ();
			messageReaderViewController.message = message;
			// Pass the selected object to the new view controller.
			NavigationController.PushViewController (
				messageReaderViewController,
				true
				);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			UIApplication.SharedApplication.ApplicationIconBadgeNumber = -1;

			LoadData();
			
			_refreshHeaderView = new RefreshTableHeaderView ();
			_refreshHeaderView.BackgroundColor = new UIColor (226f, 231f, 237f, 1f);
			messagesTableView.Source = new TableViewSource (this);
			messagesTableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			messagesTableView.AddSubview (_refreshHeaderView);
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			messagesTableView.Dispose();
			messagesTableView = null;

			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

	}

	class TableViewSource : UITableViewSource
	{
		static NSString kCellIdentifier = new NSString ("MyIdentifier");
		
		List<CustomMessageDto> _list;
		bool _checkForRefresh;
		bool _reloading;
		NSTimer _reloadTimer;
		RefreshTableHeaderView _refreshHeaderView;
		UITableView _table;
		MessagesViewController _rtvc;
		
		public TableViewSource (MessagesViewController rtvc)
		{
			_list = rtvc._mensajes;
			_checkForRefresh = false;
			_reloading = false;
			_refreshHeaderView = rtvc._refreshHeaderView;
			_table = rtvc.messagesTableView;
			_rtvc = rtvc;
		}

		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
			_rtvc.ShowMessage(_list[indexPath.Row]);
			/*
			using (UIAlertView alert = new UIAlertView(_list[indexPath.Row].SendBy, _list[indexPath.Row].Text, null, "OK", null))
			{
				alert.Show ();
			}
			*/
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//TODO: mostrar el mensaje y marcarlo como leido y actualizarlo en el servidor.
		    /*
			using (UIAlertView alert = new UIAlertView(_list[indexPath.Row].SendBy, _list[indexPath.Row].Text, null, "OK", null))
			{
				alert.Show ();
			*/
			if (_list[indexPath.Row].DateReceived == null)
			{
				DeviceServices ds = new DeviceServices();
				MessageDto messageDto = new MessageDto();
				messageDto.DateReceipt = DateTime.Now;
				messageDto.DateSent = _list[indexPath.Row].Date;
				messageDto.IdActive = _list[indexPath.Row].IdActive;
				messageDto.IdMessage = _list[indexPath.Row].IdMessage;
				messageDto.IdUser = _list[indexPath.Row].IdUser;
				messageDto.MsgText = _list[indexPath.Row].Text;
				ds.UpdateMessage(messageDto);
				//ReloadData();
			}
			_rtvc.ShowMessage(_list[indexPath.Row]);
			//}
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _list.Count;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (_list[indexPath.Row].DateReceived == null)
			{
				MessageCell cell =
					(MessageCell)tableView.DequeueReusableCell (
						kCellIdentifier);

				cell = new MessageCell (
					UITableViewCellStyle.Default,
					kCellIdentifier);

				cell.Title = _list[indexPath.Row].Date.ToString() + " " + _list[indexPath.Row].SendBy;
				cell.Details = _list[indexPath.Row].Text;
			
				return cell;
			}
			else {
				MessageReadedCell cell =
					(MessageReadedCell)tableView.DequeueReusableCell (
						kCellIdentifier);
				cell = new MessageReadedCell(
					UITableViewCellStyle.Default,
					kCellIdentifier);
				
				cell.Title = _list[indexPath.Row].Date.ToString() + " " + _list[indexPath.Row].SendBy;
				cell.Details = _list[indexPath.Row].Text;

				return cell;
			}
			/*
			if (cell == null)
			{
				cell = new MessageCell (
					UITableViewCellStyle.Default,
					kCellIdentifier);
			}*/
		}
		
		#region UIScrollViewDelegate
		[Export("scrollViewDidScroll:")]
		public void Scrolled (UIScrollView scrollView)
		{
			if (_checkForRefresh) {
				if (_refreshHeaderView.isFlipped && (_table.ContentOffset.Y > -65f) && (_table.ContentOffset.Y < 0f) && !_reloading) {
					_refreshHeaderView.FlipImageAnimated (true);
					_refreshHeaderView.SetStatus (RefreshTableHeaderView.RefreshStatus.PullToReloadStatus);
				} else if ((!_refreshHeaderView.isFlipped) && (_table.ContentOffset.Y < -65f)) {
					_refreshHeaderView.FlipImageAnimated (true);
					_refreshHeaderView.SetStatus (RefreshTableHeaderView.RefreshStatus.ReleaseToReloadStatus);
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
				CustomMessageDto[] mensajes = deviceServices.GetMessages(Device.GetMAC());
				_list.Clear();
				foreach (CustomMessageDto m in mensajes) {
					_list.Add (m);
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
					//TODO: refrescar mensajes
					ReloadData();

					_reloadTimer = null;
					_reloading = false;
					_refreshHeaderView.FlipImageAnimated (false);
					_refreshHeaderView.ToggleActivityView ();
					UIView.BeginAnimations ("DoneReloadingData");
					UIView.SetAnimationDuration (0.3);
					_table.ContentInset = new UIEdgeInsets (0f, 0f, 0f, 0f);
					_refreshHeaderView.SetStatus (RefreshTableHeaderView.RefreshStatus.PullToReloadStatus);
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


