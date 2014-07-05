using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	
	public class RegisterServerListener : EventListener{
		
		override public EventHandler OnEvent {
			get {
				return StartServer;
			}
		}
		
		private void StartServer(object requestSource,  EventArgs e){ 
			eventArgs.CreateServerEventArgs csEventArgs = (eventArgs.CreateServerEventArgs) e;
			Debug.Log("Starting Server " + csEventArgs.hostName);
			Network.InitializeServer ( csEventArgs.connectionsAllowed,  csEventArgs.portNumber, !Network.HavePublicAddress ());
			MasterServer.RegisterHost (ApplicationValues.Name, csEventArgs.hostName);
		}
	}
}