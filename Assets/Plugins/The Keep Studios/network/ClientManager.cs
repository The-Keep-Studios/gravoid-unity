using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network{
	[RequireComponent(typeof(ServerJoiner))]
	public class ClientManager : MonoBehaviour{

		private HostData[] hostList = new HostData[0];
		
		public string gameTypeName;
		
		private bool isRefreshingHostList = false;
		
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
				MasterServer.RequestHostList(gameTypeName);
				hostList = MasterServer.PollHostList();
			}
		}
		
		public void JoinServer(HostData hostData){
			ServerJoiner sj = this.GetComponent<ServerJoiner>();
			sj.HostToJoin = hostData;
			sj.JoinServer();
		}

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