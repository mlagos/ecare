using System;
using MonoTouch.CoreLocation;

namespace familyecare
{
	public class LocationManagerDelegate:CLLocationManagerDelegate
	{
		AppDelegate app;
		
		public LocationManagerDelegate(AppDelegate appDelegate):base()
		{
			this.app = appDelegate;
		}
		
		public override void UpdatedLocation (CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation)
		{
			/*
			this.app.DeviceListenerShared.Altitude = newLocation.Altitude;
			this.app.DeviceListenerShared.Longitude = newLocation.Coordinate.Longitude;
			this.app.DeviceListenerShared.Latitude = newLocation.Coordinate.Latitude;
			this.app.DeviceListenerShared.HorizontalAccuracy = newLocation.HorizontalAccuracy;
			this.app.DeviceListenerShared.VerticalAccuracy = newLocation.VerticalAccuracy;
			*/
			app.ProcessLocation(newLocation, oldLocation);

			//app.DeviceListenerShared.StopMotionUpdates();
		}
		
		public override void UpdatedHeading (CLLocationManager manager, CLHeading newHeading)
		{
			/*
			this.app.DeviceListenerShared.TrueHeading = newHeading.TrueHeading;
			this.app.DeviceListenerShared.HeadingAccuracy = newHeading.HeadingAccuracy;
			*/
			Console.WriteLine("UpdateHeading...");
		}
	}
}

