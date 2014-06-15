using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network
{
	public class NetworkManager : MonoBehaviour
	{

		public string typeName = "UniqueGameName";
	
		// Use this for initialization
		void Start (){
		
		}
		
		void OnGUI () {
			if (!(Network.isClient || Network.isServer) && GUI.Button (new Rect (0, 0, 200, 20), "Start Server")) {
				StartServer ();
			}
		}
		
		public void StartServer ()
		{
			Debug.Log("Starting Server");
			Network.InitializeServer (5, 25000, !Network.HavePublicAddress ());
			MasterServer.RegisterHost (ApplicationValues.Name, typeName);
		}
		
		void OnServerInitialized ()
		{
			//SpawnPlayer ();
		}
	}
}
