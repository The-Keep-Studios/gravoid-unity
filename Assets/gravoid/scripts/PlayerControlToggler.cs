using UnityEngine;
using System.Collections;
using System;

//HACK Made specifically to just enable or disable the player controls while the menu is open

namespace TheKeepStudios.events.listeners {
	
	public class PlayerControlToggler : EventListener{

		
		override public EventHandler OnEvent {
			get {
				return DeactivateClick;
			}
		}
		
		private void DeactivateClick(object requestSource, EventArgs e){
			this.CanControl(false);
		}



	GameObject GetLocalPlayer(){
		foreach(GameObject player in GameObject.FindGameObjectsWithTag ("Player")){
			bool networkConnected = Network.isServer || Network.isClient;
			bool networkOwned = player.networkView != null && player.networkView.isMine;
			if(!networkConnected || networkOwned){
				return player;
			}
		}
		return null;
	}

	public void CanControl(bool controlState)
	{
		GameObject player = GetLocalPlayer();
		if(player != null)
		{
			ShipGravitationalVelocityController gvc = player.GetComponent<ShipGravitationalVelocityController>();
			gvc.enabled = controlState;
		}
	}
}

}