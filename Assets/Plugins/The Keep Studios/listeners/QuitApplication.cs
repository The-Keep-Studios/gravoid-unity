using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.listeners {

	public class QuitApplication : events.SelfRegisteringEventListener{
	
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