using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class OpenServerWidget : Widget	{

		private string serverName = "";

		public string ServerName {
			get {
				return serverName.Length != 0 
					? serverName 
						: string.Format ("{0} {1}"
		                   , TheKeepStudios.ApplicationValues.Name
		                   , TheKeepStudios.ApplicationValues.Version);
			}
			set {
				serverName = value;
			}
		}

		override public void Draw () {
			string currentServerName = ServerName;
			if (!Network.isClient && !Network.isServer) {
				GUILayout.BeginHorizontal ();
				ServerName = GUILayout.TextField (ServerName);
				if (GUILayout.Button (Label)) {
					Debug.Log ("Button " + this.Label + " clicked");
					OnTKSEvent (EventArgs.Empty);
				}
				GUILayout.EndHorizontal ();
			}
		}
	}
}
