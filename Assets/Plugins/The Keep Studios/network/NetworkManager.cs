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
			if (!Network.isClient && !Network.isServer && GUI.Button (new Rect (0, 0, 200, 20), "Start Server")) {
				StartServer ();
			}
		}
		
		private void StartServer ()
		{
			Network.InitializeServer (5, 25000, !Network.HavePublicAddress ());
			MasterServer.RegisterHost (typeName, ApplicationValues.Name);
		}
		
		void OnServerInitialized ()
		{
			//SpawnPlayer ();
		}
	}
}
