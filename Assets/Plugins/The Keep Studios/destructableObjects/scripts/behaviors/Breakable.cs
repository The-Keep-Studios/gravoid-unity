using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Destructable{

	[RequireComponent(typeof(Spawned))]
	public class Breakable : ObjectCluster{
	
		public void OnBreak(){
		
			StartCoroutine(BreakApart());
		
		}
	
		public IEnumerator BreakApart(){
			
			yield return new WaitForFixedUpdate(); //wait until the fixed (physics) update frame
			
			foreach(Detachable child in this.GetComponentsInChildren<Detachable>(true)){
				
				child.Detach();
				
			}
			
			this.GetComponent<Spawned>().Despawn();
		
		}
	
	}

}