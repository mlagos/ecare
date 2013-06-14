using System;
using MonoTouch.CoreMotion;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;

namespace familyecare
{
	#region Delegates
	public delegate void HeadingChangedHandler(double heading);
	public delegate void RollChangedHandler(double roll);
	public delegate void YawChangedHandler(double yaw);
	public delegate void AccuracyChangedHandler(double accurH, double accurV);
	#endregion
	
	public class DeviceListener
	{	
		#region Events
		public event HeadingChangedHandler HeadingChanged;
		public event RollChangedHandler RollChanged;
		public event YawChangedHandler YawChanged;
		public event AccuracyChangedHandler AccuracyChanged;
		#endregion
		
		#region Fields
		private CLLocationManager locationMngr = null;
		private LocationManagerDelegate lmDelegate = null;
		private CMMotionManager motionMngr = null;
		private bool lockedPosition = false;
		#endregion
		
		#region Properties
		public double Altitude
		{
			get;
			set;
		}
		
		public double Longitude
		{
			get;
			set;
		}
		
		public double Latitude
		{
			get;
			set;
		}
		
		public double HorizontalAccuracy
		{
			get;
			set;
		}
		
		public double VerticalAccuracy
		{
			get;
			set;
		}
		
		private double _heading;
		
		public double TrueHeading
		{
			get { return _heading; }
			set
			{
				if (_heading != value)
				{
					_heading = value;
					OnHeadingChanged(_heading);
				}
			}
		}
		
		private double _roll;
		
		public double Roll
		{
			get { return _roll; }
			set
			{
				if (_roll != value)
				{
					_roll = value;
					OnRollChanged(_roll);
				}
			}
		}
		
		private double _horizontalAccuracy;
		private double _verticalAccuracy;
		
		public double HeadingAccuracy
		{
			get;
			set;
		}
		
		private double _yaw;
		
		public double Yaw
		{
			get { return _yaw; }
			set
			{
				if (_yaw != value)
				{
					_yaw = value;
					OnYawChanged(_yaw);
				}
			}
		}
		
		public CLLocationManager LocationManager
		{
			get
			{
				return locationMngr;
			}
		}
		
		public CMMotionManager MotionManager
		{
			get
			{
				return motionMngr;
			}
		}
		#endregion
		
		#region Constructor
		public DeviceListener(AppDelegate app)
		{
			locationMngr = new CLLocationManager();
			locationMngr.Delegate = new LocationManagerDelegate(app);
			motionMngr = new CMMotionManager();
			motionMngr.ShowsDeviceMovementDisplay = true;
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Starts the device position monitor.
		/// </summary>
		public void StartDevicePositionMonitor()
		{
			InitMonitors();
			
			//Start monitoring updates
			if (locationMngr != null)
			{
				locationMngr.StartUpdatingLocation();
			}
			if (motionMngr != null)
			{
				//Attach handles
				RestartAllMotionUpdates();
			}
		}
		
		public void RestartAllMotionUpdates()
		{
			if (motionMngr != null)
			{
				RestartMotionUpdates();
				
				motionMngr.StopAccelerometerUpdates();
				motionMngr.StartAccelerometerUpdates();
				
				motionMngr.StopGyroUpdates();
				motionMngr.StartGyroUpdates();
				
				motionMngr.StopMagnetometerUpdates();
				motionMngr.StartMagnetometerUpdates();
			}
		}
		
		public void RestartMotionUpdates()
		{
			if (motionMngr != null)
			{
				motionMngr.StopDeviceMotionUpdates();
				motionMngr.StartDeviceMotionUpdates(CMAttitudeReferenceFrame.XArbitraryCorrectedZVertical);
				motionMngr.StartDeviceMotionUpdates(NSOperationQueue.MainQueue, MotionData_Received);
			}
		}
		
		public void StopMotionUpdates()
		{
			locationMngr.StopUpdatingHeading();
			locationMngr.StopUpdatingLocation();
			motionMngr.StopAccelerometerUpdates();
			motionMngr.StopDeviceMotionUpdates();
			motionMngr.StopGyroUpdates();
			motionMngr.StopMagnetometerUpdates();
		}
		
		private void InitMonitors()
		{
			//Init LocationManager
			if (locationMngr == null)
			{
				throw new NullReferenceException("Cannot start monitor. LocationManager is null!");
			}
			locationMngr.Purpose = ApplicationSettings.LocationMonitor.ApplicationPurpose;
			locationMngr.DesiredAccuracy = CLLocation.AccuracyBest;
			locationMngr.HeadingFilter = -1;
			locationMngr.DistanceFilter = 20;
			locationMngr.PausesLocationUpdatesAutomatically = false;

			
			//Init MotionManager
			/*
			if (motionMngr == null)
			{
				throw new NullReferenceException("Cannot start monitor. MotionManager is null!");
			}
			motionMngr.ShowsDeviceMovementDisplay = true;
			motionMngr.AccelerometerUpdateInterval = 1.0 / 10.0;
			motionMngr.GyroUpdateInterval = ApplicationSettings.MotionMonitor.GyroUpdateInterval;
			motionMngr.DeviceMotionUpdateInterval = 1.0 / 10.0;
			*/
		}
		
		/// <summary>
		/// Gets the current location.
		/// </summary>
		/// <returns>
		/// The CL location.
		/// </returns>
		public CLLocation GetCLLocation()
		{
			return new CLLocation(new CLLocationCoordinate2D(this.Latitude, this.Longitude), this.Altitude, this.HorizontalAccuracy, this.VerticalAccuracy, NSDate.Now);
		}
		
		/// <summary>
		/// Toggles the lock position.
		/// </summary>
		/// <returns>
		/// New lock position value.
		/// </returns>
		public bool ToggleLockPosition()
		{
			lockedPosition = !lockedPosition;
			if (lockedPosition)
			{
				locationMngr.StopUpdatingLocation();
			}
			else
			{
				locationMngr.StartUpdatingLocation();
			}
			return lockedPosition;
		}
		#endregion
		
		#region Event Methods
		protected void OnHeadingChanged(double heading)
		{
			if (HeadingChanged != null)
				HeadingChanged(heading);
		}
		
		protected void OnRollChanged(double roll)
		{
			if (RollChanged != null)
			{
				RollChanged(roll);
			}
		}
		
		protected void OnYawChanged(double yaw)
		{
			if (YawChanged != null)
			{
				YawChanged(yaw);
			}
		}
		
		protected void OnAccuracyChanged(double accurH, double accurV)
		{
			if (AccuracyChanged != null)
			{
				AccuracyChanged(accurH, accurV);
			}
		}
		#endregion
		
		#region Event handlers
		private void MotionData_Received(CMDeviceMotion motionData, NSError error)
		{
			this.Roll = motionData.Attitude.Roll;
			this.Yaw = motionData.Attitude.Yaw;
		}
		#endregion
		
	}
}

