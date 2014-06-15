using System;
using UnityEngine;

namespace TheKeepStudios.events.eventArgs{
	public class ServerJoinRequestEventArgs : System.EventArgs{
		public readonly HostData hostData;
		public ServerJoinRequestEventArgs (HostData hostData){
			this.hostData = hostData;
		}
	}
}