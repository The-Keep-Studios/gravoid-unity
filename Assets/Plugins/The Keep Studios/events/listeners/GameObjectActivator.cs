using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	
	public class GameObjectActivator : EventListener{
		
		public GameObject objectToActivate;
		
		override public EventHandler OnEvent {
			get {
				return DeactivateObject;
			}
		}
		
		private void DeactivateObject(object requestSource, EventArgs e){
			Debug.Log("Activation of " + objectToActivate + " requested by " + requestSource, this.gameObject);
			if(objectToActivate != null){
				objectToActivate.SetActive(true);
			}
		}
	}
}