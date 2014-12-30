using UnityEngine;
using System.Collections.Generic;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS{
	public class LauncherBehavior : MonoBehaviour{
	
		public float launchingForce;

		public float launchDelayTime = 0.0f;

		public float launchCooldownTime = 0.0f;

		private bool m_readyToLaunch = false;

		public void Start(){
		
			this.m_readyToLaunch = true;
		
		}

		public bool ReadyToLaunch(){
		
			return this.m_readyToLaunch;
		
		}

		public bool Launch(ProjectileBehavior _projectile){
		
			bool success = false;
		
		
			if(this.ReadyToLaunch() && _projectile != null && _projectile.CanBeLaunched()){
			
				this.startLaunch(_projectile);
			
				success = true;
			
			}
		
			return success;
		
		}

		protected void startLaunch(ProjectileBehavior _projectile){
		
			m_readyToLaunch = false;
		
			_projectile.ignoreCollisionsWith(this.gameObject.collider);
		
			_projectile.Launch(launchingForce, this.transform); //need to get the world, not the local transform
		
			this.StartCoroutine(this.startCooldown());
		
		}

		protected System.Collections.IEnumerator startCooldown(){
		
			m_readyToLaunch = false;
		
			yield return new WaitForSeconds(this.launchCooldownTime);
		
			m_readyToLaunch = true;
		
		}

	}
}