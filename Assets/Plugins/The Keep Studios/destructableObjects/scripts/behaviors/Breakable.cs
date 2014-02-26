using UnityEngine;
using System.Collections;

public class Breakable : ObjectCluster {
	
	public override void OnSpawned(){
		
		this.detachChildrenImmediately = false; //prevent breaking apart too early
		
		base.OnSpawned();
		
	}
	
	public void OnBreak(){
		
		BreakApart();
		
	}
	
	public void BreakApart(){
		
		this.despawnAfterDetach = true;
		
		this.detachChildrenImmediately = true;
		
		StartCoroutine( CreateCluster() );
		
	}
	
	override protected void DetachChild(Transform child, float angularRotSpeed, float bodyRadius){
		base.DetachChild(child,angularRotSpeed,bodyRadius);
		
		if( child.rigidbody && this.rigidbody ){
			
			//note that we are assuming said centrifugalForce is non-directional, 
			child.rigidbody.AddExplosionForce(angularRotSpeed, this.transform.position, bodyRadius, 0, ForceMode.VelocityChange); 
			
		}
		
	}
	
}
