using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Collections;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using System.Net.NetworkInformation;

using Path = System.IO.Path;
using SQLite;

namespace familyecare
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;
		private DeviceListener deviceListener = null;
		private Thread threadGPS;
		private Thread threadGPSBackground;
		Database _db;

		public DeviceListener DeviceListenerShared {
			get {
				if (deviceListener == null) {
					deviceListener = new DeviceListener (this);
				}
				return deviceListener;
			}
		}

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			//var controller = new RootViewController ();
			var controller = new MainViewController ();
			navigationController = new UINavigationController (controller);
			window.RootViewController = navigationController;

			//Inicializamos la base de datos
			//_db = new Database(Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "familyecare.db"));
			//_db.Trace = true;

			//Register for remote notifications
			UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Alert
			                                                                   | UIRemoteNotificationType.Badge
			                                                                   | UIRemoteNotificationType.Sound);
						
			//Iniciamos el control y seguimiento GPS
			//Iniciamos el Timer del GPS en un hilo independiente para que no se bloquee la interfaz de usuario
			threadGPS=new Thread(StartTimerGPS as ThreadStart);
			threadGPS.Start();

			//Procesamos notificaciones ya descargadas
			processNotification(options, true);

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}


		private int ReadBatteryLevel()
		{
			var dev = UIDevice.CurrentDevice;
			dev.BatteryMonitoringEnabled = true;
			int level = 0;
			if (dev.BatteryMonitoringEnabled)
				try
				{
				level = Convert.ToInt32(Math.Round(dev.BatteryLevel * 100));
				}
				finally
				{
					dev.BatteryMonitoringEnabled = false;
				}
			else
			{
				level = -1;
			}            
			return level;
		}

		public void ProcessLocation(CLLocation location, CLLocation oldLocation)
		{
			//Console.WriteLine("Posicion: " + location.Timestamp.ToString() + " / " + location.Coordinate.Latitude.ToString());
			DeviceListenerShared.StopMotionUpdates();
			//DeviceListenerShared.ToggleLockPosition();

			familyecare.com.DeviceServices deviceServices = new global::familyecare.familyecare.com.DeviceServices();

			string id = Device.GetMAC();
			double lat = location.Coordinate.Latitude;
			double lon = location.Coordinate.Longitude;
			DateTime date = location.Timestamp;
			DateTime fecha = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
			DateTime locFecha = TimeZone.CurrentTimeZone.ToLocalTime(fecha);
			double direction = location.Course;
			double mileage = 0.0;
			bool isCellId = !location.Coordinate.IsValid();
			double speed = Math.Max(location.Speed, 0);
			int baterryLevel = ReadBatteryLevel();
			double hdop = Math.Max(location.HorizontalAccuracy, 0);
			int crc = (locFecha.Year + locFecha.Month)*locFecha.Day;
			//Console.WriteLine("MAC: " + id);
			try
			{
				int result = 0;
				//"012658000382847"
				result = deviceServices.AddLocation(id, lat, lon, locFecha, isCellId, baterryLevel, "", direction, mileage, speed, hdop, crc); 
				Console.WriteLine("Enviada localizacion GPS al servicio: " + result.ToString());
			}
			catch (Exception ex) {
				Console.WriteLine("DeviceServices: " + ex.ToString());
			}
			//UIDevice.CurrentDevice.BatteryMonitoringEnabled=false;
		}

		//Timer para control y seguimiento GPS
		NSTimer timerGPS;
		[Export("StartTimerGPS")]
		void StartTimerGPS()
		{
			using (var pool = new NSAutoreleasePool())
			{
				timerGPS = NSTimer.CreateRepeatingScheduledTimer(240, delegate { 
					Console.WriteLine("TimerGPS fire!");
					DeviceListenerShared.StartDevicePositionMonitor();    
					//DeviceListenerShared.ToggleLockPosition();
				});
				NSRunLoop.Current.Run();
			}
		}
		
		void StopTimerGPS()
		{
			timerGPS.Invalidate ();
			timerGPS.Dispose ();
			timerGPS = null;
			Console.WriteLine("StopTimerGPS.");
		}

		NSTimer timerGPSBackground;
		[Export("StartTimerGPSBackground")]
		void StartTimerGPSBackground()
		{
			using (var pool = new NSAutoreleasePool())
			{
				timerGPSBackground = NSTimer.CreateRepeatingScheduledTimer(240, delegate { 
					Console.WriteLine("TimerGPSBackground fire!");
					DeviceListenerShared.StartDevicePositionMonitor();    
					//DeviceListenerShared.ToggleLockPosition();
				});
				NSRunLoop.Current.Run();
			}
		}

		void StopTimerGPSBackground()
		{
			timerGPSBackground.Invalidate ();
			timerGPSBackground.Dispose ();
			timerGPSBackground = null;
			Console.WriteLine("StopTimerGPSBackground.");
		}

		public override void WillEnterForeground(UIApplication application)
		{
			Console.WriteLine("Estoy en primer plano");
			StopTimerGPSBackground();
			Console.WriteLine("Foreground: detenido GPSBackground.");
			//El GPS ya se ejecuta en 2 Threads uno para background y otro para primer plano.
			//Los Threads no hace falta despertarlos, ya arrancan automáticamente al cambiar de primer a segundo plano y viceversa.
			//StartTimerGPS();

			//TODO: leer las notificaciones pendientes y marcarlas en la interfaz


		}

		//our task id
		int ourTask;
		public override void DidEnterBackground (UIApplication application)
		{
			UIApplication.SharedApplication.SetKeepAliveTimeout(601, () => { Console.WriteLine("KeepAlive!"); });
			ourTask = application.BeginBackgroundTask(delegate
			                                          {    //this is the action that will run when the task expires
				if (ourTask != 0) //this check is because we want to avoid ending the same task twice
				{
					application.EndBackgroundTask(ourTask); //end the task
					ourTask = 0; //reset the id
				}
			});
			
			//we start an asynchronous operation
			//so that we make sure that DidEnterBackground
			//executes normally
			new System.Action(delegate
			                  {
				//code to execute
				//...
				Console.WriteLine("Estoy en background...");
				StartTimerGPSBackground();
				Console.WriteLine("Background: arrancado GPSBackground.");
				if (threadGPSBackground == null)
				{
					//StopTimerGPS();
					//StartTimerGPSBackground();
					//Si es la primera vez que entramos en Background tenemos que crear y arrancar el Thread
					//en los siguientes backgrounds ya no es necesario, arrancan y paran automáticamente.
					//Iniciamos el control y seguimiento GPS en Background
					//Iniciamos el Timer del GPS en un hilo independiente para que no se bloquee la interfaz de usuario
					//threadGPSBackground=new Thread(StartTimerGPSBackground as ThreadStart);
					//threadGPSBackground.Start();
					//Console.WriteLine("Background: threadGPSBackground arrancado.");
				}
				//StartTimerGPS();
				//Since we are in an asynchronous method, 
				//we have to make sure that EndBackgroundTask 
				//will run on the application's main thread
				//or we might have unexpected behavior.
				application.BeginInvokeOnMainThread(delegate
				                                    {
					if (ourTask != 0) //same as above
					{
						application.EndBackgroundTask(ourTask);
						ourTask = 0;
					}
				});
			}).BeginInvoke(null, null);
		}//end override void DidEnterBackground

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
			
			//There's probably a better way to do this
			var strFormat = new NSString("%@");
			var dt = new NSString(MonoTouch.ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(new MonoTouch.ObjCRuntime.Class("NSString").Handle, new MonoTouch.ObjCRuntime.Selector("stringWithFormat:").Handle, strFormat.Handle, deviceToken.Handle));
			var newDeviceToken = dt.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");

			Console.WriteLine("Device Token: " + newDeviceToken);

			if (string.IsNullOrEmpty(oldDeviceToken) || !deviceToken.Equals(newDeviceToken))
			{
				//Notificamos al servidor del DeviceToken del iPhone
				familyecare.com.DeviceServices deviceServices = new global::familyecare.familyecare.com.DeviceServices();
				string id = Device.GetMAC();
				if (deviceServices.RegisterDeviceToken(id, newDeviceToken) != 0)
				{
					//Save device token now
					NSUserDefaults.StandardUserDefaults.SetString(newDeviceToken, "PushDeviceToken");
					Console.WriteLine("Error registrando DeviceToken");
				}
				else {
					//Reset device token
					NSUserDefaults.StandardUserDefaults.SetString("", "PushDeviceToken");
					Console.WriteLine("DeviceToken registrado OK.");
				}
			}
		}
		
		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			Console.WriteLine("Failed to register for notifications: " + error.ToString());
		}
		
		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			Console.WriteLine("Received Remote Notification!");

			/*
			var av = new UIAlertView("Notification:", "Received Remote Notification!", null, "OK", null);
			av.Show ();

			(navigationController.VisibleViewController as MainViewController).LoadMensajesView();
			*/
			processNotification(userInfo, false);

		}


		void processNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			try
			{
				//Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
				if (null != options && options.ContainsKey(new NSString("aps")))
				{
					//Get the aps dictionary
					NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;
					string alert = string.Empty;
					string sound = string.Empty;
					int badge = -1;
					//Extract the alert text
					//NOTE: If you're using the simple alert by just specifying "  aps:{alert:"alert msg here"}  "
					//      this will work fine.  But if you're using a complex alert with Localization keys, etc., your "alert" object from the aps dictionary
					//      will be another NSDictionary... Basically the json gets dumped right into a NSDictionary, so keep that in mind
					if (aps.ContainsKey(new NSString("alert")))
						alert = (aps[new NSString("alert")] as NSString).ToString();
					//Extract the sound string
					if (aps.ContainsKey(new NSString("sound")))
						sound = (aps[new NSString("sound")] as NSString).ToString();
					//Extract the badge
					if (aps.ContainsKey(new NSString("badge")))
					{
						string badgeStr = (aps[new NSString("badge")] as NSObject).ToString();
						int.TryParse(badgeStr, out badge);
					}
					//If this came from the ReceivedRemoteNotification while the app was running,
					// we of course need to manually process things like the sound, badge, and alert.
					if (!fromFinishedLaunching)
					{
						//Manually set the badge in case this came from a remote notification sent while the app was open
						if (badge >= 0)
							UIApplication.SharedApplication.ApplicationIconBadgeNumber = -1;
						//Manually play the sound
						/*********************
						if (!string.IsNullOrEmpty(sound))
						{
							//This assumes that in your json payload you sent the sound filename (like sound.caf)
							// and that you've included it in your project directory as a Content Build type.
							var soundObj = MonoTouch.AudioToolbox.SystemSound.FromFile(sound);
							soundObj.PlaySystemSound();
						}
						*/
						//Manually show an alert
						if (!string.IsNullOrEmpty(alert))
						{
							UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
							avAlert.Show();
						}
					}
				}
				//You can also get the custom key/value pairs you may have sent in your aps (outside of the aps payload in the json)
				// This could be something like the ID of a new message that a user has seen, so you'd find the ID here and then skip displaying
				// the usual screen that shows up when the app is started, and go right to viewing the message, or something like that.
				if (options.ContainsKey(new NSString("EVT")))
				{
					string msg = (options[new NSString("EVT")] as NSString).ToString();
					UIAlertView avAlert = new UIAlertView("Evento", msg, null, "OK", null);
					avAlert.Show();
				}
				if (options.ContainsKey(new NSString("SOS")))
				{
					string msg = (options[new NSString("SOS")] as NSString).ToString();
					UIAlertView avAlert = new UIAlertView("SOS", msg, null, "OK", null);
					avAlert.Show();
				}
				if (options.ContainsKey(new NSString("CFG")))
				{
					string msg = (options[new NSString("CFG")] as NSString).ToString();
					UIAlertView avAlert = new UIAlertView("Configuracion", msg, null, "OK", null);
					avAlert.Show();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

	}


}

