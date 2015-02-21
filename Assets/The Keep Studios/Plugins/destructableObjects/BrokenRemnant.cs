using UnityEngine;
using System.Collections;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Destructable{

	public class BrokenRemnant : Detachable{

		public TheKeepStudios.spawning.Spawnable remnantToSpawn;

		// Use this for initialization
		void Start(){
	
		}
	
		// Update is called once per frame
		void Update(){
	
		}

		override public void Detach(){

			Spawnable remnant = remnantToSpawn.Spawn(this.transform);
			
			remnant.gameObject.SetActive(true);
			
			Transform parent = this.transform.parent;
			
			Rigidbody rb = remnant.rigidbody;
			
			if(rb && parent && parent.rigidbody){
				
				Rigidbody prb = parent.rigidbody;
				
				Vector3 myWorldPosition = parent.transform.InverseTransformPoint(rb.position);
				
				rb.velocity = prb.GetPointVelocity(myWorldPosition);
				
			}
		}
	}
}