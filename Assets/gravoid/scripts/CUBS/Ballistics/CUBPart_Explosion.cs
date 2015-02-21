using UnityEngine;
using System.Collections;
using UnityEditor;

//TODO Make the explosion propagate over time rather than everywhere instantaneously

namespace TheKeepStudios.Gravoid.CUBS.Ballistics
{
		public class CUBPart_Explosion : CUBPartBase
		{	
				[SerializeField]
				float
						radius; //indicates the radius encompassed by the explosion
				
				
				[SerializeField]
				float
						force;  //indicates the force imparted on all objects caught in the explosion

				override public void Activate (GameObject activator)
				{


				}



				// This will be called in order to make it explode... real helpful comment, isn't it?
				[ContextMenu("EXPLOSIONS?!?!?!?!?!?!")]
				private void Explode ()
				{
						Debug.Log ("The object should have exploded");
						
					

						Vector3 explosionPos = transform.position; //Sets the origin of the explosion to the current position
		
						//Gathers all of the rigidbodies which will be affected by the explosion
						Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
						foreach (Collider hit in colliders) {
								if (hit && hit.rigidbody) {
										hit.rigidbody.AddExplosionForce (force, explosionPos, radius);
										Debug.Log ("The explosion has hit " + hit.name);
								}
						}
		

				}
		//This is used to draw a circle at the radius of the explosion in the editor as a reference
		#if UNITY_EDITOR
		void OnDrawGizmosSelected(){
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
		#endif
		}
}