using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	
	public class InitalizeServerListener : EventListener{
		
		override public EventHandler OnEvent {
			get {
				return InitializeServer;
			}
		}
		
		
		private void InitializeServer(object requestSource,  EventArgs e){ 
			Debug.Log("Initializing Server Single Player" );
			//Need below line to happen when player clicks "New Game"
			Network.InitializeServer ( 0, -1, true);

		}
	}
}
