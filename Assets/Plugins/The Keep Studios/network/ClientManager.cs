using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network
{
	public class ClientManager : MonoBehaviour
	{

		private HostData[] hostList = new HostData[0];
		private bool isRefreshingHostList = false;
		[SerializeField] private  GameObject playerPrefab;
		[SerializeField] private int playerSpawnGroup;
		
		public HostData[] HostList {
			get {
				//never return a null array
				return hostList == null ? new HostData[0] : hostList;
			}
		}

		void Awake() {
			DontDestroyOnLoad(this.gameObject);
		}
	
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
			Debug.Log("Connected to the server");
			SpawnPlayer ();
		}

		void OnLevelWasLoaded(int level) {
			if(Network.isServer){
				Network.RemoveRPCs(this.networkView.viewID);
				this.networkView.RPC("NetworkLoadLevel", RPCMode.OthersBuffered, Application.loadedLevel);
			}
		}
	
		private void SpawnPlayer ()
		{
			Debug.Log("Spawning 'player' object");
			Network.Instantiate (playerPrefab, Vector3.zero, Quaternion.identity, playerSpawnGroup);
		}    

		void OnPlayerDisconnected(NetworkPlayer player) {
			//FIXME this isn't nearly enough but we better put it in for now
			Debug.Log("Clean up after player " + player);
			Network.RemoveRPCs(player);
			Network.DestroyPlayerObjects(player);
		}

		[RPC]
		public void NetworkLoadLevel(int levelIdx) {
			Debug.Log("NetworkLoadLevel called to load level " + levelIdx);
			if(Network.isClient){
				Debug.Log("Loading level " + levelIdx);
				Application.LoadLevel(levelIdx);
			}
		}
	}
}