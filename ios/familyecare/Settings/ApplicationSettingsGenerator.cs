﻿		///</summary> 
		// This class is generated by ApplicationSettingsGenerator.tt template.
		///</summary>
				public static partial class ApplicationSettings
		{    
				public static partial class LocationMonitor
		{    
				public static string ApplicationPurpose = @"SampleApp location monitor";  
				} //End Of Class LocationMonitor  
				public static partial class MotionMonitor
		{    
					public static double GyroUpdateInterval = 0.1;
					public static double AccelerometerUpdateInterval = 0.1;
					public static double DeviceMotionUpdateInterval = 0.1;
				} //End Of Class MotionMonitor  
					///<summary>  
			// Landscape mode
	// http://stackoverflow.com/questions/3594199/iphone-4-camera-specifications-field-of-view-vertical-horizontal-angle 
			///</summary>
					public static partial class FOV
		{    
					///<summary>  
			// For iPhone in radians (float)2.0 * (float)Math.Atan (58.5 / 2.0 / 56.5); 
		// For iPhone 4S in radians (float)2.0 * (float)Math.Atan (4.592 / (2 * 4.28)); 
			///</summary>
						public static double Horizontal = 0.98475913484564;
					///<summary>  
			// For iPhone in radians (float)2.0 *  (float)Math.Atan (21.5 / 2.0 / 56.5);
		// For iPhone 4S in radians (float)2.0 *  (float)Math.Atan (3.450 / (2 * 4.28)); 
			///</summary>
						public static double Vertical = 0.76624413062396;
				} //End Of Class FOV  
				public static partial class Notification
		{    
				public static partial class ApplicationServerUrl
		{    
				public static string Subscribe = @"http://192.168.44.254:50569/api/Subscribe";  
				} //End Of Class ApplicationServerUrl  
				} //End Of Class Notification  
				} //End Of Class Configuration  
		