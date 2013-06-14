using System;
using System.Drawing;
using System.Threading;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreTelephony;

using familyecare.familyecare.com;

namespace familyecare
{
	public partial class SOSViewController : UIViewController
	{
		private Thread threadCall;
		CTTelephonyNetworkInfo networkInfo;
		CTCallCenter callCenter;
		string carrierName;
		CTCall[] calls = new CTCall [0];
		bool callChecked = false;

		public SOSViewController () : base ("SOSViewController", null)
		{
			Title = "LLamada de emergencia";
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

			//Inicializamos componentes de control de la llamada
			networkInfo = new CTTelephonyNetworkInfo ();
			callCenter = new CTCallCenter ();
			callCenter.CallEventHandler += CallEvent;
			carrierName = networkInfo.SubscriberCellularProvider == null ? null : networkInfo.SubscriberCellularProvider.CarrierName;
			networkInfo.CellularProviderUpdatedEventHandler += ProviderUpdatedEvent;

			//Iniciamos la secuencia de la llamada de emergencia
			//la iniciamos en un thread para no interferir con la interfaz de usuario.
			threadCall = new Thread(DoSOSCall as ThreadStart);
			threadCall.Start();
			//DoSOSCall();
		}

		private void ProviderUpdatedEvent (CTCarrier carrier)
		{
			MonoTouch.CoreFoundation.DispatchQueue.MainQueue.DispatchSync (() =>
			                                                               {
				carrierName = carrier == null ? null : carrier.CarrierName;
				//¿nos interesa hacer algo con esto?
			});
		}
		
		private void CallEvent (CTCall inCTCall)
		{
			MonoTouch.CoreFoundation.DispatchQueue.MainQueue.DispatchSync (() =>
			                                                               {
				NSSet calls = callCenter.CurrentCalls;
				calls = callCenter.CurrentCalls;
				if (calls == null) {
					this.calls = new CTCall [0];
				} else {
					this.calls = calls.ToArray<CTCall> ();
				}
				Array.Sort (this.calls, (CTCall a, CTCall b) =>
				            {
					return string.Compare (a.CallID, b.CallID);
				});
				//Aqui sabemos si la llamada se ha podido realizar, etc.

			});
		}

		private void UpdateLabelFromThread(string text)
		{
			InvokeOnMainThread (delegate { 
				labelStatus.Text = text; 
			});
		}
		[Export("DoSOSCall")]
		private void DoSOSCall()
		{
			//labelStatus.Text = "Iniciando llamada de emergencia y enviando posición GPS...";
			UpdateLabelFromThread("Iniciando llamada de emergencia y enviando posición GPS...");

			var app = UIApplication.SharedApplication;
			
			try {
				app.NetworkActivityIndicatorVisible = true;
				DeviceServices ds = new DeviceServices();
				string emergencyNumbers = ds.GetEmergencyNumbers(Device.GetMAC());
				app.NetworkActivityIndicatorVisible = false;

				if (emergencyNumbers.Length>0)
				{
					Console.WriteLine(emergencyNumbers);
					string[] separador = new string[1];
					separador[0] = "##";
					string[] emergency = emergencyNumbers.Split(separador, StringSplitOptions.RemoveEmptyEntries);
					//el formato del mensaje es: 'Nombre##Telefono##NumIntentos'
					//como máximo puede haber 3 números SOS
					//Console.WriteLine("Emergency.Length = " + emergency.Length);
					switch (emergency.Length) {
						case 3:
							//hay 1 número SOS
							if (!DoPhoneCall(emergency[0], emergency[1], Int32.Parse(emergency[2])))
						    {
								//labelStatus.Text = "No se ha podido realizar la llamada de emergencia";
								UpdateLabelFromThread("No se ha podido realizar la llamada de emergencia");
							}
							break;
						case 6:
							//hay 2 números SOS
						if (!DoPhoneCall(emergency[0], emergency[1], Int32.Parse(emergency[2])))
							{
							if (!DoPhoneCall(emergency[3], emergency[4], Int32.Parse(emergency[5])))
								{
									//labelStatus.Text = "No se ha podido realizar la llamada de emergencia";
									UpdateLabelFromThread("No se ha podido realizar la llamada de emergencia");
								}
							}
							break;
						case 9:
							//hay 3 números SOS
							if (!DoPhoneCall(emergency[0], emergency[1], Int32.Parse(emergency[2])))
							{
								if (!DoPhoneCall(emergency[3], emergency[4], Int32.Parse(emergency[5])))
								{
									if (!DoPhoneCall(emergency[6], emergency[7], Int32.Parse(emergency[8])))
									{
										//labelStatus.Text = "No se ha podido realizar la llamada de emergencia";
										UpdateLabelFromThread("No se ha podido realizar la llamada de emergencia");
									}
								}
							}
							break;
					}
				}
				else {
					/*
					using (UIAlertView alert = new UIAlertView ("Atención", "No está configurada la llamada de emergencia en la plataforma.", null, "OK", null)) {
						InvokeOnMainThread (delegate { 
						alert.Show ();
						});
					}
					*/
					UpdateLabelFromThread("ATENCIÓN: no está configurada la llamada de emergencia en la plataforma.");
				}
			}
			catch (Exception ex)
			{
				/*
				using (UIAlertView alert = new UIAlertView ("Atención", "No se ha podido realizar la llamada de emergencia." + ex.ToString(), null, "OK", null)) {
					InvokeOnMainThread (delegate { 
					alert.Show ();
					});
				}
				*/
				Console.WriteLine("No se ha podido realizar la llamada de emergencia: " + ex.ToString());
				UpdateLabelFromThread("No se ha podido realizar la llamada de emergencia");
			}
			finally {
				app.NetworkActivityIndicatorVisible = false;
			}
		}

		private bool DoPhoneCall(string name, string number, int retry)
		{
			callChecked = false;
			int retryCount = 0;
			retryCount++;
			//labelStatus.Text = "Llamando a " + name + " al número " + number +". Llamada " + retryCount + " de " + retry;  
			UpdateLabelFromThread("Llamando a " + name + " al número " + number +". Llamada " + retryCount + " de " + retry);
			NSUrl url = new NSUrl("tel:" + number);

			InvokeOnMainThread(delegate {
				UIApplication.SharedApplication.OpenUrl(url);
			});

			//UIApplication.SharedApplication.OpenUrl(url);
			/*
			if (!UIApplication.SharedApplication.OpenUrl(url))
			{
				var av = new UIAlertView("Atención", "Este dispositivo no permite realizar llamadas.", null, "Aceptar", null);
				av.Show();
				return false;
			}
			*/

			while (!callChecked)
			{
				//labelStatus.Text += ".";
				//UpdateLabelFromThread(labelStatus.Text += ".");
				Thread.Sleep(150);
				if (calls.Length>0)
				{
					switch (calls[0].CallState) {
						case "CTCallStateConnected":
							callChecked = true;
							//labelStatus.Text += " -> Llamada establecida.";
							UpdateLabelFromThread(" -> Llamada establecida.");
							return true;
							break;
						case "CTCallStateDisconnected": 
							//labelStatus.Text += " -> No conesta.";
							UpdateLabelFromThread(" -> No contesta.");
							if (retryCount < retry)
							{
								retryCount++;
								//labelStatus.Text = "Llamando a " + name + " al número " + number +". Llamada " + retryCount + " de " + retry;
								UpdateLabelFromThread("Llamando a " + name + " al número " + number +". Llamada " + retryCount + " de " + retry);
								InvokeOnMainThread(delegate {
									UIApplication.SharedApplication.OpenUrl(url);
								});
								//UIApplication.SharedApplication.OpenUrl(url);
								callChecked = false;
							}
						    else {
								//labelStatus.Text += " -> No contesta.";
								UpdateLabelFromThread(" -> No contesta.");
								callChecked = true;
								return false;
							}
							break;
					}
				}
			}
			return false;
		}

		public string GetCallState (string callState)
		{
			switch (callState) {
			case "CTCallStateDialing": 
				return "Llamando";
			case "CTCallStateIncoming": 
				return "Llamada entrante";
			case "CTCallStateConnected":
				return "Llamada establecida";
			case "CTCallStateDisconnected": 
				return "Llamada finalizada";
			default:
				return callState;
			}
		}	
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			networkInfo.CellularProviderUpdatedEventHandler -= ProviderUpdatedEvent;
			callCenter.CallEventHandler -= CallEvent;

			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		partial void buttonCancel_Click (MonoTouch.Foundation.NSObject sender)
		{
			callChecked = true;
			labelStatus.Text = "Cancelando llamada de emergencia";
			threadCall.Abort();
			threadCall = null;
			NavigationController.PopViewControllerAnimated(true);
		}
	}
}

