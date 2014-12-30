using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Destructable{

	[RequireComponent(typeof(Spawned))]
	public class ObjectCluster : MonoBehaviour{

		public void OnSpawned(){
		
			StartCoroutine(CreateCluster());
		
		}

		protected IEnumerator CreateCluster(){
		
			yield return new WaitForFixedUpdate(); //wait until the fixed (physics) update frame
		
			foreach(Detachable child in this.GetComponentsInChildren<Detachable>(true)){
				
				child.Detach();
				
			}
			
			this.GetComponent<Spawned>().Despawn();
		
		}
	
	}

}