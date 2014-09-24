using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Destructable{
	
	public class Detachable : MonoBehaviour{
		
		public void Detach(){
			
			this.gameObject.SetActive(true);

			Transform parent = this.transform.parent;

			Rigidbody rb = this.rigidbody;
						
			if(rb && parent && parent.rigidbody){

				Rigidbody prb = parent.rigidbody;

				Vector3 myWorldPosition = parent.transform.InverseTransformPoint(rb.position);

				rb.velocity = prb.GetPointVelocity(myWorldPosition);
				
			}
			
			//now disconnect us from our parent
			this.transform.parent = parent == null 
				? null 
					: parent.parent;
			
		}
		
	}
}