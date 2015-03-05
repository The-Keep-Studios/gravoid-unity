using System;
using UnityEngine;

namespace TheKeepStudios.events.eventArgs{

	public class CreateServerEventArgs : System.EventArgs{
		public readonly string hostName;

		public readonly object source;

		public readonly int connectionsAllowed;

		public readonly int portNumber;

		private const int defaultMaxConnections = 5;

		private const int defaultPort = 26789;

		public CreateServerEventArgs(object source, string hostName){
			this.hostName = hostName;
			this.source = source;
			this.connectionsAllowed = defaultMaxConnections;
			this.portNumber = defaultPort;
		}
		
		public CreateServerEventArgs(object source, string hostName, int connectionsAllowed){
			this.hostName = hostName;
			this.source = source;
			this.connectionsAllowed = connectionsAllowed;
			this.portNumber = defaultPort;
		}

		public CreateServerEventArgs(object source, string hostName, int connectionsAllowed, int portNumber){
			this.hostName = hostName;
			this.source = source;
			this.connectionsAllowed = connectionsAllowed;
			this.portNumber = portNumber;
		}
	}
}

