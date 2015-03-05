using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS{
	public class ConfigurationSelectorBehavior : MonoBehaviour{

		private Ballistics.IProjectileConfiguration configuration;

		virtual public Ballistics.IProjectileConfiguration Configuration{
			get{
				return configuration;
			}
			set{
				configuration = value;
			}
		}

		// Use this for initialization
		void Start(){
			if(Configuration == null){
				Configuration = new Ballistics.ProjectileConfiguration();
			}
		}

		// Update is called once per frame
		void Update(){
		}
	}
}