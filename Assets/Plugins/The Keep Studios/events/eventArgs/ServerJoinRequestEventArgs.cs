using System;
using UnityEngine;

namespace TheKeepStudios.events.eventArgs
{
	public class ServerJoinRequestEventArgs : System.EventArgs
	{
		public readonly HostData hostData;
		public readonly object source;

		public ServerJoinRequestEventArgs (object source, HostData hostData)
		{
			this.hostData = hostData;
			this.source = source;
		}
	}
}