using UnityEngine;
using System.Collections;

//TODO Needs to know which way to force things
//TODO What happens if it is not yet atached to something?
//FIXME Need to know how the grabber is handling "attatching" to items. Is it all one rigidbody?

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	
	public class CUBPart_Propellant : CUBPartBase
	{
		//The amount of time for which the engine will burn
		[SerializeField]
		float burnTime;

		//The force which will be imparted on the object
		[SerializeField]
		float force;

		//is the rocket firing?
		private bool isFiring = false;
		//is used as a counter to track how much time has passed while the physics is updating.
		private float timeCounter = 0;
		private float currentTime;



		override public void Activate(GameObject activator){
		
		}

		//When the part is activated it will call this function
		[ContextMenu("Light this candle")]
		private void IgnitePropellant()
		{
			Debug.Log ("the propellent should be active");
			isFiring = true;


		}


		void FixedUpdate()
		{
			//Is the engine firing?
			if (isFiring)
			{
				//Taking the current position
				currentTime = (++timeCounter)*Time.fixedDeltaTime;

				if(currentTime <= burnTime)
				{
					//transform.parent.rigidbody
					Debug.Log ("Current timestep" +currentTime );




				}
			}



		}




		
	}
}

