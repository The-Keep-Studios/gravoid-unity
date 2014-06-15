using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network
{
	public class ClientManager : MonoBehaviour
	{

		private HostData[] hostList = new HostData[0];
		private bool isRefreshingHostList = false;
		public GameObject playerPrefab;
		public int playerSpawnGroup;

		void OnGUI ()
		{
			if (!Network.isClient && !Network.isServer) {
				GUILayout.BeginArea (new Rect (0, 21, 200, 200));
			
				if (GUILayout.Button ("Refresh Hosts")) {
					RefreshHostList ();
				}

				GUILayout.BeginScrollView (new Vector2 (190, 190));
				foreach (HostData host in hostList) {
					if (GUILayout.Button (host.gameName)) {
						JoinServer (host);
					}
				}
				GUILayout.EndScrollView ();
				GUILayout.EndArea ();
			}
		}
	
		void Update ()
		{
			if (isRefreshingHostList && MasterServer.PollHostList ().Length > 0) {
				isRefreshingHostList = false;
				hostList = MasterServer.PollHostList ();
			}
		}
	
		private void RefreshHostList ()
		{
			if (!isRefreshingHostList) {
				isRefreshingHostList = true;
				MasterServer.RequestHostList (ApplicationValues.Name);
			}
		}
	
		public void JoinServer (HostData hostData)
		{
			Network.Connect (hostData);
		}
	
		void OnConnectedToServer ()
		{
			SpawnPlayer ();
		}
	
		private void SpawnPlayer ()
		{
			Network.Instantiate (playerPrefab, Vector3.zero, Quaternion.identity, playerSpawnGroup);
		}
	}
}