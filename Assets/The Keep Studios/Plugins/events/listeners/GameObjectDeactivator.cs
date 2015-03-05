using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {

	public class GameObjectDeactivator : EventListener{

		public GameObject objectToDeactivate;
	
		override public EventHandler OnEvent {
			get {
				return DeactivateObject;
			}
		}
		
		private void DeactivateObject(object requestSource, EventArgs e){
			Debug.Log("Deactivation of " + objectToDeactivate + " requested by " + requestSource, this.gameObject);
			if(objectToDeactivate != null){
				objectToDeactivate.SetActive(false);
			}
		}
	}
}