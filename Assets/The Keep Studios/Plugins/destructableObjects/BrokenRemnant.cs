using UnityEngine;
using System.Collections;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Destructable{

	public class BrokenRemnant : Detachable{

		public GameObject remnantToSpawn;

		// Use this for initialization
		void Start(){
	
		}
	
		// Update is called once per frame
		void Update(){
	
		}

		override public void Detach(){

			GameObject remnant = remnantToSpawn.Spawn(null, transform.position, transform.rotation);

			remnant.SetActive(true);
			
			Transform parent = this.transform.parent;
			
			Rigidbody rb = remnant.GetComponent<Rigidbody>();
			
			if(rb && parent && parent.GetComponent<Rigidbody>()){
				
				Rigidbody prb = parent.GetComponent<Rigidbody>();
				
				Vector3 myWorldPosition = parent.transform.InverseTransformPoint(rb.position);
				
				rb.velocity = prb.GetPointVelocity(myWorldPosition);
				
			}
		}
	}
}