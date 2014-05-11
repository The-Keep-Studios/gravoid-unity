using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.menu.listeners {

	public class QuitApplication : events.EventListener{

		public EventHandler onEvent;
		
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