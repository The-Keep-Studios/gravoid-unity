using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	
	public class RegisterServerListener : EventListener{
		
		override public EventHandler OnEvent {
			get {
				return RegisterServer;
			}
		}

		private void RegisterServer(object requestSource,  EventArgs e){ 
			eventArgs.CreateServerEventArgs csEventArgs = (eventArgs.CreateServerEventArgs) e;
			Debug.Log("Starting Server " + csEventArgs.hostName);
			MasterServer.RegisterHost (ApplicationValues.Name, csEventArgs.hostName);
			Network.maxConnections = csEventArgs.connectionsAllowed;
		}
	}
}