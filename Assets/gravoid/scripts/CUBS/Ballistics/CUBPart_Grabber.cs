using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public class CUBPart_Grabber : CUBPart{
		override public void Activate(GameObject activator){
			
		}
		
		override public void OnCollisionEnter(Collision collision){
			base.OnCollisionEnter(collision);
			GrabObject(collision.gameObject);
		}
		
		private void GrabObject(GameObject objectToGrab){
			if(!objectToGrab.rigidbody){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as it has no RidgidBody component");
				return;
			} else if(ConnectionJoint.connectedBody){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as we already have grabbed " + ConnectionJoint.connectedBody.gameObject);
				return;
			} else{
				Debug.Log("Grabbing object " + objectToGrab + " with " + gameObject);
				ConnectionJoint.connectedBody = objectToGrab.rigidbody;
				if(Next){
					Debug.Log(gameObject + " is activating the next object CUBPart");
					Next.Activate(gameObject);
				}
			}
		}
		
	}
}