using UnityEngine;
using System.Collections;
using UnityEditor;

//NOTE If things get buggy/slow with this, make it only retreive location once. 

namespace TheKeepStudios.Gravoid.CUBS.Ballistics
{
		public class CUBPart_Explosion : CUBPartBase
		{	
				//indicates the radius encompassed by the explosion
				[SerializeField]
				public float radius;
				
				//indicates the force imparted on all objects caught in the explosion
				[SerializeField]
				public float force;
						
				[Tooltip("Rate of explosion radius growth [units/second] (default: 10)")]
				[SerializeField]
				public float rateOfExpansion;
				
				//the flag which will initiate the explosion
				private bool isExploding = false;
				
				//the flag which will fly once the explosion is done (to stop everything)
				private bool canStillExplode = true;   
				
				// these will be used to track the passage of time in the FixedUpdate
				private float deltaTime = 0;
				private float currentSize;

				override public void Activate (GameObject activator)
				{


				}



				// This will be called in order to make it explode... real helpful comment, isn't it?
				[ContextMenu("EXPLOSIONS?!?!?!?!?!?!")]
				private void Explode ()
				{
						Debug.Log ("The object should be exploding");
						
						
						transform.parent.GetComponent<ProjectileBehavior>().Split(this);
						//this flag/EXPLOSIONS?!?!?! probablly won't work until the split code is fixed. If this is not the case, other things be fucked
						isExploding = true;		
		

				}
				
				void FixedUpdate ()
				{
						//should it be asploding?
						if (isExploding && canStillExplode) {
								//calculates the current size of the explosion
								Vector3 explosionPos = transform.position; //Sets the origin of the explosion to the current position
						
								deltaTime += Time.fixedDeltaTime;
																
								//This should make the current size increase in increments up
								currentSize = deltaTime * rateOfExpansion;
								
								Debug.Log ("Current size of explosion is " +currentSize);
						
								if (currentSize <= radius) { 
						
						
										//Gathers all of the rigidbodies which will be affected by the explosion
										Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
										foreach (Collider hit in colliders) {
												if (hit && hit.rigidbody) {
														hit.rigidbody.AddExplosionForce (force, explosionPos, currentSize);
														//Debug.Log ("The explosion has hit " + hit.name);
								
												}
										}
										
							
								
								}
								else
								{
								canStillExplode = false;
								Debug.Log ("Is done asploding");
								}
						
						}
				}
				
				
				
				


				//This is used to draw a circle at the radius of the explosion in the editor as a reference
		#if UNITY_EDITOR
		void OnDrawGizmosSelected(){
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, radius);
			
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, currentSize);
			
			
		}
		#endif
		}
}