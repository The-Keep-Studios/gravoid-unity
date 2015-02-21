using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	
	public class CUBPart_Propellant : CUBPartBase
	{
		//The amount of time for which the engine will burn
		[SerializeField]
		float burnTime;

		//The force which will be imparted on the object
		[SerializeField]
		float force;


		override public void Activate(GameObject activator){
		
		}




		
	}
}

