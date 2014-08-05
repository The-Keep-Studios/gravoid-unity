using UnityEngine;
using System.Collections;

public class NewGameInitialzor : MonoBehaviour {

	public string postInitializationLevelToLoad;

	void Awake () {
		if(Network.isServer){
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("GameController")){
				obj.SendMessage("OnConnectInternal");
			}
		}
		//load the post initialization level
		Application.LoadLevel(postInitializationLevelToLoad);
	}

}
