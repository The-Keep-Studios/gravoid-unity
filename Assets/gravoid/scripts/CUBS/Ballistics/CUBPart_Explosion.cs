using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
	using UnityEditor;
#endif
//NOTE If things get buggy/slow with this, make it only retreive location once. 

namespace TheKeepStudios.Gravoid.CUBS.Ballistics {

	public class CUBPart_Explosion : CUBPart {

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

		override public void Activate(GameObject activator) {

		}

		public override void OnCollisionEnter(Collision collision) {
			base.OnCollisionEnter(collision);
			Explode();
		}

		// This will be called in order to make it explode... real helpful comment, isn't it?
		[ContextMenu("EXPLOSIONS?!?!?!?!?!?!")]
		private void Explode() {
			Debug.Log("The object should be exploding");
			ContainingProjectile.Split(this);
			Explosion explosion = new GameObject(name + "_Explosion",typeof(Explosion)).GetComponent<Explosion>();
			explosion.transform.position = transform.position;
			explosion.radius = radius;
			explosion.rateOfExpansion = rateOfExpansion;
			explosion.force = force;
			Despawn();
		}

		//This is used to draw a circle at the radius of the explosion in the editor as a reference
#if UNITY_EDITOR
		void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
#endif
	}
}