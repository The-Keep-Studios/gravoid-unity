using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{

	public class ServerHostListWidget : MonoBehaviour{

		public TheKeepStudios.network.ClientManager clientManager;

		public ServerHostListLineWidget serverDisplayPrefab;

		void Start(){
			Refresh();
		}

		public void Refresh(){
			StartCoroutine(RefreshHostList(this.clientManager));
		}

		private IEnumerator RefreshHostList(TheKeepStudios.network.ClientManager client){
			yield return null; //ensure we are NOT on the GUI update frame
			client.RefreshHostList();
			ServerHostListLineWidget[] displays = GetComponentsInChildren<ServerHostListLineWidget>();
			HostData[] hosts = this.clientManager.HostList;
			for(int hostIdx = 0; hostIdx < hosts.Length || hostIdx < displays.Length; ++hostIdx){
				HostData host = hostIdx < hosts.Length ? hosts[hostIdx] : null; //get the host data if it exists
				ServerHostListLineWidget widget = hostIdx < displays.Length ? displays[hostIdx] : ((GameObject)Instantiate(serverDisplayPrefab)).GetComponent<ServerHostListLineWidget>(); //get or make the matching widget
				widget.transform.SetParent(this.gameObject.transform, false); //set ourselves as the parent object for the new widget
				widget.Host = host;//update the widget host
			}
		}

	}
}
