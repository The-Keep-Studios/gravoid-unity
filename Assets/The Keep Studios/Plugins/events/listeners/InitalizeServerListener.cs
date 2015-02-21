using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {

	//[RequireComponent(typeof(TheKeepStudios.network.ServerInitializor))]
	public class InitalizeServerListener : EventListener{

		public int allowedConnections;

		public int listeningPort;

		public bool natStuffThingy;
		
		public void InitializeServer(object requestSource,  EventArgs e){ 
			this.gameObject.GetComponent<ServerInitializor>().InitializeServer(allowedConnections, listeningPort, natStuffThingy);
		}
		
		override public EventHandler OnEvent {
			get {
				return InitializeServer;
			}
		}
	}
}
