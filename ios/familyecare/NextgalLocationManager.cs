using System;

using MonoTouch.Foundation;
using MonoTouch.CoreLocation;

namespace familyecare
{
	//Liberia portada de: https://github.com/chrisdoble/low-power-location/blob/master/src/CDLocationManager.m
	public class NextgalLocationManager
	{
		/**
		 * @brief The maximum "age" of a location update (in seconds).
		 *
		 * Location updates that are older than this value will be rejected. This
		 * prevents "cached" updates from being passed through to the delegate.
		 *
		 * You shouldn't need to change this value.
		 */
		public const int LOCATION_MANAGER_LIFETIME = 1;

		/**
		 * @brief The minimum acceptable accuracy for a location update to be considered
		 *     accurate (in metres). Based on the <tt>horizontalAccuracy</tt> property.
		 *
		 * Accuracy often increases in successive location updates; this value
		 * determines when we should accept an update as being "accurate enough".
		 *
		 * Decreasing this value means that fewer, more accurate updates will be sent to
		 * the delegate, but it is likely increase the occurrence of timeouts and make
		 * the manager less robust. If you encounter these problems, just use the GPS.
		 */
		public const int LOCATION_MANAGER_MINIMUM_ACCURACY = 65;

		/**
		 * @brief The longest we will wait for an accurate location update (in seconds).
		 *
		 * Sometimes the GPS can't get an accurate fix on the device's location. This
		 * value determines when we should just give up and wait for another update.
		 *
		 * Decreasing this value will reduce power consumption as the GPS won't be on
		 * for as long. You need to strike a balance between this value an the minimum
		 * accuracy; if this is too low and it's too high, you'll continually timeout.
		 */
		public const int LOCATION_MANAGER_TIMEOUT = 60;

		//DELEGATE --------------------------------------------------------------------
		public delegate void LocationUpdatedHandler(CLLocation location);
		public event LocationUpdatedHandler LocationUpdatedEvent;

		//PROPERTIES --------------------------------------------------------------------
		// Whether the next update will be accurate (rather than significant).
		private bool _accurateUpdate;
		public bool accurateUpdate {
			get { return _accurateUpdate; }
		}

		// When we turned the GPS on to generate an accurate update.
		private DateTime _accurateUpdateStarted;
		public DateTime accurateUpdateStarted {
			get {return accurateUpdateStarted;}
			set {_accurateUpdateStarted = value;}
		}

		// Whether the next update will be the first.
		private bool _firstUpdate;
		public bool firstUpdate  {
			get {return _firstUpdate; }
		}

		// Our source of raw location updates.
		private CLLocationManager _locationManager;
		public CLLocationManager locationManager {
			get {return _locationManager; }
			set {_locationManager = value; }
		}

		// Whether the location manager is monitoring the device's location.
		private bool _running;
		public bool running {
			get {return _running; }
		}

		public NextgalLocationManager ()
		{
			_locationManager = new CLLocationManager();
			//_locationManager.UpdatedLocation += HandleUpdatedLocation;
			_locationManager.UpdatedLocation += new EventHandler<CLLocationUpdatedEventArgs>(HandleUpdatedLocation);
			//_locationManager.DesiredAccuracy = 100;
			//_locationManager.DistanceFilter = 100;
			Console.WriteLine("CLLocationManager inicializado.");
		}

		void HandleUpdatedLocation (object sender, CLLocationUpdatedEventArgs e)
		{
			Console.WriteLine("evento!");
			this.UpdatedLocation(_locationManager, e.NewLocation, e.OldLocation);
		}

		public void StartUpdatingLocation()
		{
			if (_running) {
				return;
			}
			_running = true;
			_firstUpdate = true;
			_locationManager.StartMonitoringSignificantLocationChanges();
			_locationManager.StartUpdatingLocation();
			Console.WriteLine("StartUpdatingLocation...OK");
		}

		public void StopUpdatingLocation()
		{
			if (!_running)
			{
				return;
			}
			_running = false;
			_accurateUpdate = false;
			_accurateUpdateStarted = DateTime.MinValue;
			_locationManager.StopUpdatingLocation();
			_locationManager.StopMonitoringSignificantLocationChanges();
			Console.WriteLine("StopUpdatingLocation...OK");
		}

		~NextgalLocationManager()
		{
			_locationManager = null;
		}


		private void UpdatedHeading(CLLocationManager manager, CLHeading newHeading)
		{
		}

		private void UpdatedLocation(CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation)
		{
			Console.WriteLine("UpdatedLocation...");

			if (Math.Abs(newLocation.Timestamp.SecondsSinceReferenceDate) > LOCATION_MANAGER_LIFETIME && !_firstUpdate) {
				return;
			}

			_firstUpdate = false;
			if (_accurateUpdate)
			{
				// Have we got an accurate location update or timed out?
				bool finished = false;
				bool timeOut = (DateTime.Now.Subtract(this._accurateUpdateStarted).TotalSeconds >= LOCATION_MANAGER_TIMEOUT);

				if (newLocation.HorizontalAccuracy <= LOCATION_MANAGER_MINIMUM_ACCURACY)
				{
					finished = true;
					LocationUpdatedEvent(newLocation);
				}

				if (finished || timeOut)
				{
					_accurateUpdate = false;
					_accurateUpdateStarted = DateTime.MinValue;
					_locationManager.StopUpdatingLocation();
				}
			}
			else
			{
				// We got a significant location update, so fire up the GPS.
				_accurateUpdate = true;
				_accurateUpdateStarted = DateTime.Now;
				_locationManager.StartUpdatingLocation();
			}
		}


	}
}

