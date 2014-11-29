using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS{
//SIMPLE PROTOTYPE IMPLEMENTATION
	public class ComponentSelectorBehavior : MonoBehaviour{

	#region PUBLIC PROPERTIES
		public ICUBSConfiguration configuration;
	#endregion

	#region PUBLIC METHODS
		// Use this for initialization
		void Start(){
		}


		// Update is called once per frame
		void Update(){
		
		}
	
		virtual public ICUBSConfiguration GetCurrentSelection(){
		
			return this.configuration;
		
		}
	#endregion
	}
}