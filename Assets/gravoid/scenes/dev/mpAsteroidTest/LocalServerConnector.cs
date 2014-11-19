using UnityEngine;
using System.Collections;

public class LocalServerConnector : MonoBehaviour{

	public string ip;

	public int port;

	//[Obsolete("This is a hack test method and should NEVER be used.")]
	public void JoinServer(){
		StartCoroutine(DoServerJoin());
	}

	IEnumerator DoServerJoin(){
		yield return null; 
		bool connected = false;
		while(!connected){
			try{
				Network.Connect(ip, port); //screw it, lets just join the first one as a test. This is just a test behavior anyways
				connected = true;
			} catch(System.Exception e){
				Debug.LogException(e);
			}
			yield return null; //unable to connect, wait a frame to try again
		}
	}

}
