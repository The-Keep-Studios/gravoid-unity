using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Destructable{
	
	public class Detachable : MonoBehaviour{
		
		virtual public void Detach(){
			
			this.gameObject.SetActive(true);

			Transform parent = this.transform.parent;

			Rigidbody rb = this.GetComponent<Rigidbody>();
						
			if(rb && parent && parent.GetComponent<Rigidbody>()){

				Rigidbody prb = parent.GetComponent<Rigidbody>();

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