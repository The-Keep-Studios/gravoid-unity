using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Destructable{

	[RequireComponent(typeof(TheKeepStudios.spawning.Spawned))]
	public class Breakable : MonoBehaviour{
	
		public void OnBreak(){
			Break();
		}

		protected void Break(){
			StartCoroutine(BreakApart());
		}
	
		private IEnumerator BreakApart(){
			
			yield return new WaitForFixedUpdate(); //wait until the fixed (physics) update frame

			//TODO Consider using BrokenRemnant instead of Detachable here
			foreach(Detachable child in this.GetComponentsInChildren<Detachable>(true)){
				
				child.Detach();
				
			}
			
			this.GetComponent<TheKeepStudios.spawning.Spawned>().Despawn();
		
		}
	
	}

}