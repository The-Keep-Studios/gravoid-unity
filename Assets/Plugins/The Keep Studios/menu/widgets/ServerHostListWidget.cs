using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class ServerHostListWidget : Widget{

		public TheKeepStudios.network.ClientManager clientManager;
		public Vector2 scrollPosition;
	
		override public void Draw (){
			if (!Network.isClient && !Network.isServer) {			
				if (GUILayout.Button ("Refresh Hosts")) {
					StartCoroutine (RefreshHostList(this.clientManager));
				}
				
				GUILayout.BeginScrollView (scrollPosition);
				foreach (HostData host in this.clientManager.HostList) {
					if (GUILayout.Button (host.gameName)) {
						StartCoroutine (JoinServer (this.clientManager, host));
					}
				}
				GUILayout.EndScrollView ();
			}
		}

		private IEnumerator JoinServer(TheKeepStudios.network.ClientManager client, HostData host){
			yield return null; //ensure we are NOT on the GUI update frame
			client.JoinServer(host);
		}

		private IEnumerator RefreshHostList(TheKeepStudios.network.ClientManager client){
			yield return null; //ensure we are NOT on the GUI update frame
			client.RefreshHostList();
		}

	}
}
