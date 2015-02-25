using UnityEngine;
using System.Collections.Generic;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public class CUBPart_SpringLauncher : CUBPart{
		
		public float power;
		
		override public void Activate(GameObject activator){
			Debug.Log("Activating the spring launcher: " + this.name);
			List<Projectile> detatchedProjectiles = Detatch();
			List<Rigidbody> bodiesToPush = new List<Rigidbody>();
			foreach(Projectile nextProj in detatchedProjectiles){
				if(!nextProj.Contains(this)){
					if(nextProj != null && nextProj.Head != null && nextProj.Head.rigidbody != null){
						bodiesToPush.Add(nextProj.Head.rigidbody);
					}
				}
			}
			// if our activator is NOT in the list of objects to push already, make sure it is
			if(activator && activator.rigidbody && !bodiesToPush.Contains(activator.rigidbody)){
				bodiesToPush.Add(activator.rigidbody);
			}
			PushObjects(bodiesToPush);
			//for now springs will immediately despawn once they have completed activation
			ContainingProjectile.Head = null;
			ContainingProjectile.Tail = null;
		}
		
		override public void OnLaunch(GameObject activator){
			Activate(activator);
			RefundResources(activator);
		}
		
		public override void OnCollisionEnter(Collision collision){
			Activate(collision.gameObject);
		}
		
		private List<Projectile> Detatch(){
			Projectile parentProjectile = ContainingProjectile;
			if(parentProjectile != null){
				return parentProjectile.Split(this);
			}
			return new List<Projectile>(); 
		}
		
		private void RefundResources(GameObject refundDestination){
			Debug.Log("Refunding  " + refundDestination.name + " for " + this.name);
			//TODO GRA-367 Put this component BACK in the inventory
			throw new System.NotImplementedException("GRA-367 Not yet completed, inventory not refunded CUBPart_SpringLauncher resources");
		}
		
		void PushObjects(List<Rigidbody> rbList){
			Vector3 pushOrigin = transform.position;
			foreach(Rigidbody nextRB in rbList){
				Debug.Log("Pushing " + nextRB.name + " with spring launcher " + this.name);
				nextRB.AddExplosionForce(power, pushOrigin, 0);
			}
		}
		
		[ContextMenu("Simulate Next Part Activating Spring Launcher")]
		void SimulateNextPartActivation(){
			if(Next){
				this.Activate(Next.gameObject);
			} else{
				this.Activate(null);
			}
		}
	}
}