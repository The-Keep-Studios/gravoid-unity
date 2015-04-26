using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public class CUBPart_Grabber : CUBPart{

		[UnityEngine.SerializeField]
		float grabConeAngle;

		override public void Activate(GameObject activator){
			
		}
		
		override public void OnCollisionEnter(Collision collision){
			base.OnCollisionEnter(collision);
			if (collsionInGrabArc(collision)) {
				GrabObject(collision.gameObject);
			}
			else {
				Debug.Log("Not grabbing object " + collision.gameObject + " with " + gameObject + " as it is not in the grab arc");
			}
		}
		
		private void GrabObject(GameObject objectToGrab){
			if(!objectToGrab.GetComponent<Rigidbody>()){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as it has no RidgidBody component");
				return;
			} else if(ConnectionJoint.connectedBody){
				Debug.Log("Not grabbing object " + objectToGrab + " with " + gameObject + " as we already have grabbed " + ConnectionJoint.connectedBody.gameObject);
				return;
			} else{
				Debug.Log("Grabbing object " + objectToGrab + " with " + gameObject);
				ConnectionJoint.connectedBody = objectToGrab.GetComponent<Rigidbody>();
				if(Next){
					Debug.Log(gameObject + " is activating the next object CUBPart");
					Next.Activate(gameObject);
				}
			}
		}

		private bool collsionInGrabArc(Collision collision) {
			foreach (ContactPoint contact in collision.contacts) {
				if(pointInGrabTriggerArea(contact.point)){
					return true;
				}
			}
			return false;
		}

		private bool pointInGrabTriggerArea(Vector3 point) {
			return angleFromFront(point) <= grabConeAngle;
		}

		private float angleFromFront(Vector3 point) {
			return Vector3.Angle(transform.forward, transform.position - point);
		}

	}
}