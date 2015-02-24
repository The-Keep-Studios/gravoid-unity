using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	
	public class CUBPart_Grabber : CUBPart{
	
		[SerializeField]
		FixedJoint
			grabJoint;
		
		void Start(){
			if(!grabJoint){
				grabJoint = transform.parent.GetComponent<FixedJoint>();
				if(!grabJoint){
					grabJoint = transform.parent.gameObject.AddComponent<FixedJoint>();
				}
			}
			grabJoint.connectedBody = null;
		}
		override public void Activate(GameObject activator){
			
		}
		
		override public void OnCollisionEnter(Collision collision){
			base.OnCollisionEnter(collision);
			GrabObject(collision.gameObject);
		}
		
		private void GrabObject(GameObject objectToGrab){
			if(grabJoint.connectedBody){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as we already have grabbed " + grabJoint.connectedBody.gameObject);
				return;
			} else if(!objectToGrab.rigidbody){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as it has no RidgidBody component");
				return;
			} else{
				Debug.Log("Grabbing object " + objectToGrab + " with " + gameObject);
				grabJoint.connectedBody = objectToGrab.rigidbody;
				if(Next){
					Debug.Log(gameObject + " is activating the next object CUBPart");
					Next.Activate(gameObject);
				}
			}
		}
		
	}
}