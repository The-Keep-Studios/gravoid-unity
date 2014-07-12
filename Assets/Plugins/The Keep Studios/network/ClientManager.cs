using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network
{
	public class ClientManager : MonoBehaviour
	{

		private HostData[] hostList = new HostData[0];

		private bool isRefreshingHostList = false;

		[SerializeField]
		private  GameObject clientPrefab;

		[SerializeField]
		private int clientSpawnGroup;
		
		public HostData[] HostList {
			get {
				//never return a null array
				return hostList == null ? new HostData[0] : hostList;
			}
		}
	
		public void RefreshHostList ()
		{
			if (!isRefreshingHostList) {
				Debug.Log ("Refreshing the host list");
				isRefreshingHostList = true;
				hostList = null;
				MasterServer.RequestHostList (ApplicationValues.Name);
			}
		}
	
		public void JoinServer (HostData hostData)
		{
			Network.Connect (hostData);
		}
		
		private void SpawnClient ()
		{
			Debug.Log ("Spawning 'player' object");
			Network.Instantiate (clientPrefab, Vector3.zero, Quaternion.identity, clientSpawnGroup);
		}

		#region Remote Procedures
		[RPC]
		public void NetworkLoadLevel (int levelIdx)
		{
			Debug.Log ("NetworkLoadLevel called to load level " + levelIdx);
			if (Network.isClient) {
				Debug.Log ("Loading level " + levelIdx);
				Application.LoadLevel (levelIdx);
			}
		}
		#endregion

		#region Unity Event Handlers
		
		void Awake ()
		{
			DontDestroyOnLoad (this.gameObject);
		}
		
		void Update ()
		{
			if (isRefreshingHostList) {
				hostList = MasterServer.PollHostList ();
				isRefreshingHostList = hostList.Length == 0;
				Debug.Log ("Retrieved host list of size " + hostList.Length);
			}
		}
		
		void OnConnectedToServer ()
		{
			Debug.Log ("Connected to the server");
			SpawnClient ();
		}
		
		void OnLevelWasLoaded (int level)
		{
			if (Network.isServer) {
				Network.RemoveRPCs (this.networkView.viewID);
				this.networkView.RPC ("NetworkLoadLevel", RPCMode.OthersBuffered, Application.loadedLevel);
			}
		}
		
		void OnPlayerConnected (NetworkPlayer player)
		{
			Debug.Log ("Setup the newly connected player " + player);
		}
		
		void OnPlayerDisconnected (NetworkPlayer player)
		{
			//FIXME this isn't nearly enough but we better put it in for now
			Debug.Log ("Clean up after player " + player);
			Network.RemoveRPCs (player);
			Network.DestroyPlayerObjects (player);
		}

		#endregion
	}
}