using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class ClientLevelLoader : MonoBehaviour {
	
	#region Remote Procedures
	[RPC]
	public void NetworkLoadLevel (int levelIdx)
	{
		Debug.Log ("NetworkLoadLevel called to load level " + levelIdx);
		if (Network.isClient) {
			Debug.LogWarning ("Client is loading level idx [" + levelIdx + "]");
			Application.LoadLevel (levelIdx);
		}
	}
	#endregion
}
