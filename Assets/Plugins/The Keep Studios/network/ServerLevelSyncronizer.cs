using UnityEngine;
using System.Collections;

public class ServerLevelSyncronizer : MonoBehaviour {

	void OnLevelWasLoaded(int level) {
		if (Network.isServer) {
			Debug.Log("Loading level idx [" + level + "] and sending out the RPC to warn the clients");
			Network.RemoveRPCs (this.networkView.viewID);
			this.networkView.RPC ("LoadLevel", RPCMode.OthersBuffered, Application.loadedLevel);
		}
	} 

	[RPC]
	void LoadLevel(int levelIdx){
		Debug.Log("Syncronizing to server's level idx [" + levelIdx + "]");
		Application.LoadLevel (levelIdx);
	}
}
