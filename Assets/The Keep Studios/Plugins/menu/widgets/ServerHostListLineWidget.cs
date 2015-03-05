using UnityEngine;
using System.Collections;

namespace TheKeepStudios.menu.widgets{

	public class ServerHostListLineWidget : MonoBehaviour{

		public TheKeepStudios.network.ServerJoiner serverJoiner;

		public HostData Host{
			get{
				return serverJoiner.HostToJoin;
			}
			set{
				serverJoiner.HostToJoin = value;
				gameObject.SetActive(Host != null);//widget is active iff we have a host
				if(Host != null){
					UpdateGUIElements();
				}
			}
		}

		private void UpdateGUIElements(){
			UnityEngine.UI.Text text = this.GetComponentInChildren<UnityEngine.UI.Text>();
			text.text = Host.gameName;
		}
	}
}