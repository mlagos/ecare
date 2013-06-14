using System;
using System.Net.NetworkInformation;

namespace familyecare
{
	public class Device
	{
		public Device ()
		{
		}

		public static string GetMAC()
		{
			foreach (var i in NetworkInterface.GetAllNetworkInterfaces ()) {
				//Console.WriteLine ("{0} {1}", i.Id, i.GetPhysicalAddress ());
				if (i.Id.Equals("en0"))
					return i.GetPhysicalAddress().ToString();
			}
			return "";
		}

	}
}

