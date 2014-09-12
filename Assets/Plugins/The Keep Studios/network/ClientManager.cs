using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network{
	public class ClientManager : MonoBehaviour{

		private HostData[] hostList = new HostData[0];

		private bool isRefreshingHostList = false;

		public  GameObject clientPrefab;

		public int clientSpawnGroup;

		public string gameTypeName;
		
		public HostData[] HostList{
			get{
				//never return a null array
				return hostList == null ? new HostData[0] : hostList;
			}
		}
	
		public void RefreshHostList(){
			if(!isRefreshingHostList){
				Debug.Log("Refreshing the host list");
				isRefreshingHostList = true;
				hostList = null;
				MasterServer.RequestHostList(gameTypeName);
			}
		}
	
		public void JoinServer(HostData hostData){
			Network.Connect(hostData);
		}
		
		private void SpawnClient(){
			Debug.Log("Spawning 'player' object");
			Network.Instantiate(clientPrefab, Vector3.zero, Quaternion.identity, clientSpawnGroup);
		}

		#region Message Listeners

		/// <summary>
		/// Raises the connect internal client event.
		/// </summary>
		/// When this message is recieved we create a new client for the local player.
		/// This is done so that we maintain the client/server logic of the game consistantly with externally connecting players and the local hosting player.
		public void OnConnectInternal(){
			//local client has 
			SpawnClient();
		}

		#endregion

		#region Unity Event Handlers
		
		void Awake(){
			DontDestroyOnLoad(this.gameObject);
		}
		
		void Update(){
			if(isRefreshingHostList){
				hostList = MasterServer.PollHostList();
				isRefreshingHostList = false;
				Debug.Log("Retrieved host list of size " + hostList.Length);
			}
		}
		
		void OnConnectedToServer(){
			Debug.Log("Connected to the server");

			SpawnClient();

		}
		
		void OnPlayerConnected(NetworkPlayer player){
			Debug.Log("Setup the newly connected player " + player);
		}
		
		void OnPlayerDisconnected(NetworkPlayer player){
			//FIXME this isn't nearly enough but we better put it in for now
			Debug.Log("Clean up after player " + player);
			Network.RemoveRPCs(player);
			Network.DestroyPlayerObjects(player);
		}

		#endregion
	}
}