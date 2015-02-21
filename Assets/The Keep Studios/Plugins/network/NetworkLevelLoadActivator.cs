using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network{

	public class NetworkLevelLoadActivator : MonoBehaviour{
		
		public GameObject[] serverOnlyObjects;
		
		public GameObject[] clientOnlyObjects;
		
		public GameObject[] anyMachineObjects;
			
		public void OnNetworkLoadedLevel(){
			
			if(Network.isServer || !Network.isClient){
				foreach(GameObject o in serverOnlyObjects){
					o.SetActive(true);
				}
			}
		
			if(Network.isClient || !Network.isServer){
				foreach(GameObject o in clientOnlyObjects){
					o.SetActive(true);
				}
			}
			
			foreach(GameObject o in anyMachineObjects){
				o.SetActive(true);
			}
			
		}
	}
}
