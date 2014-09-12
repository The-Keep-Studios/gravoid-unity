using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class ClientLevelLoader : MonoBehaviour{

	public const int gameLevelNetworkDataGroup = 1;
	public int disconnectedLevel = -1;
	private int lastLevelPrefix = 0;

	
	void Awake(){
		// Network level loading is done in a separate channel.
		DontDestroyOnLoad(this);
		networkView.group = gameLevelNetworkDataGroup;
		if(disconnectedLevel < 0 || disconnectedLevel >= Application.levelCount){
			//if 
			disconnectedLevel = Application.loadedLevel;
		}
	}
	
	void OnDisconnectedFromServer(){
		Application.LoadLevel(disconnectedLevel);
	}
	
	#region Remote Procedures
	[RPC]
	IEnumerable NetworkLoadLevel(int levelIdx){
	
		Debug.Log("NetworkLoadLevel called to load level " + levelIdx);

		int levelPrefix = levelIdx;

		lastLevelPrefix = levelPrefix;
		
		// There is no reason to send any more data over the network on the default channel,
		// because we are about to load the level, thus all those objects will get deleted anyway
		Network.SetSendingEnabled(0, false);    
		
		// We need to stop receiving because first the level must be loaded first.
		// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
		Network.isMessageQueueRunning = false;
		
		// All network views loaded from a level will get a prefix into their NetworkViewID.
		// This will prevent old updates from clients leaking into a newly created scene.
		Network.SetLevelPrefix(levelPrefix);
		Application.LoadLevel(levelIdx);
		yield return null;
		yield return null;
		
		// Allow receiving data again
		Network.isMessageQueueRunning = true;
		// Now the level has been loaded and we can start sending out data to clients
		Network.SetSendingEnabled(0, true);
		
		//unsure if this is really necessary... -Ian T Small
		foreach(GameObject gObj in FindObjectsOfType<GameObject>()){
			gObj.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver); 
		}

	}
	#endregion
}
