using System;
using UnityEngine;

namespace TheKeepStudios.events.eventArgs
{
	public class CreateServerEventArgs : System.EventArgs
	{
		public readonly string hostName;
		public readonly object source;
		public readonly int connectionsAllowed;
		public readonly int portNumber;

		public CreateServerEventArgs (object source, string hostName, int connectionsAllowed = 5, int portNumber = 26789)
		{
			this.hostName = hostName;
			this.source = source;
			this.connectionsAllowed = connectionsAllowed;
			this.portNumber = portNumber;
		}
	}
}

