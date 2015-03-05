using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics {

	public class Explosion : MonoBehaviour {

		//indicates the radius encompassed by the explosion
		[SerializeField]
		public float
			radius;

		//indicates the force imparted on all objects caught in the explosion
		[SerializeField]
		public float
			force;

		[Tooltip("Rate of explosion radius growth [units/second] (default: 10)")]
		[SerializeField]
		public float
			rateOfExpansion;

		// these will be used to track the passage of time in the FixedUpdate
		private float deltaTime = 0;

		private float currentSize;

		void FixedUpdate() {
			//calculates the current size of the explosion
			Vector3 explosionPos = transform.position; //Sets the origin of the explosion to the current position
			deltaTime += Time.fixedDeltaTime;
			//This should make the current size increase in increments up
			currentSize = deltaTime * rateOfExpansion;
			Debug.Log("Current size of explosion is " + currentSize);
			if (currentSize <= radius) {
				//Gathers all of the rigidbodies which will be affected by the explosion
				Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
				foreach (Collider hit in colliders) {
					if (hit && hit.GetComponent<Rigidbody>()) {
						hit.GetComponent<Rigidbody>().AddExplosionForce(force, explosionPos, currentSize);
					}
				}
			}
			else {
				Debug.Log("Is done asploding");
				Despawn();
			}
		}

		void Despawn() {
			gameObject.Recycle();
		}
		 

		//This is used to draw a circle at the radius of the explosion in the editor as a reference
#if UNITY_EDITOR
		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, currentSize);


		}
#endif
	}
}