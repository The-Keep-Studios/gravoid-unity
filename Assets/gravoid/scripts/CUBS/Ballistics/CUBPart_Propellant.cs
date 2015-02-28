using UnityEngine;
using System.Collections;

//TODO Needs to know which way to force things
//TODO What happens if it is not yet atached to something?
//FIXME Need to know how the grabber is handling "attatching" to items. Is it all one rigidbody?

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	public class CUBPart_Propellant : CUBPart{
		[Tooltip("The amount of time for which the engine will burn [sec]")]
		[SerializeField]
		float
			burnTime;

		[Tooltip("The force which will be imparted on the object [units]")]
		[SerializeField]
		float
			force;

		//is the rocket firing?
		private bool isFiring = false;
		private bool canStillFire = true;
		//is used as a counter to track how much time has passed while the physics is updating.
		private float deltaTime = 0;


		override public void Activate(GameObject activator){
			IgnitePropellant();
		}
				
		override public void OnLaunch(GameObject activator){
			base.OnLaunch(activator);
			IgnitePropellant();
		}

		//When the part is activated it will call this function
		[ContextMenu("Light this candle")]
		private void IgnitePropellant(){
			Debug.Log("the propellent should be active");
			isFiring = true;


		}

		void FixedUpdate(){
			//Is the engine firing?
			if(isFiring && canStillFire){
				//Taking the current position
				deltaTime += Time.fixedDeltaTime;
								
								
				if(deltaTime <= burnTime){
					//transform.parent.rigidbody
					Debug.Log("Current timestep" + deltaTime);
										
					//Should apply the force in the +y direction of the nozzle
					transform.rigidbody.AddForce(transform.TransformDirection (Vector3.up) * force);


				} else{
					canStillFire = false;
					Debug.Log("All Done!");
				}
			}



		}




		
	}
}

