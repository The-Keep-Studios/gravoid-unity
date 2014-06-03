using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {

	public class QuitApplication : SelfRegisteringEventListener{
	
		override public EventHandler OnEvent {
			get {
				return HardQuit;
			}
		}
		
		private void HardQuit(object source, EventArgs e){
			Application.Quit();
		}
	}
}