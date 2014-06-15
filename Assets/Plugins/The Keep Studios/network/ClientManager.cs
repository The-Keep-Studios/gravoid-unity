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
	
		void Update ()
		{
			if (isRefreshingHostList ) {
				hostList = MasterServer.PollHostList ();
				isRefreshingHostList = hostList.Length == 0;
				Debug.Log("Retrieved host list of size " + hostList.Length);
			}
		}
	
		public void RefreshHostList ()
		{
			if (!isRefreshingHostList) {
				Debug.Log("Refreshing the host list");
				isRefreshingHostList = true;
				hostList = null;
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

		public HostData[] HostList {
			get {
				//never return a null array
				return hostList == null ? new HostData[0] : hostList;
			}
		}
	}
}