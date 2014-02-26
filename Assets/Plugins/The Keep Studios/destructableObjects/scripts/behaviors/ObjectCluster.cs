using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCluster : MonoBehaviour {
	
	public bool detachChildrenImmediately;
	
	public bool despawnAfterDetach;
	
	public string originSpawnPoolName;

	virtual public void OnSpawned(){
		
		StartCoroutine( this.CreateCluster() );
		
	}
	
	protected void DetachAll(){
		
		//this.transform.DetachChildren(); no longer used because we need to work on individal children anyways
		
		Transform[] children = this.GetComponentsInChildren<Transform>(true);
		
		Vector3 parentAngVel = Vector3.zero;
		
		Vector3 parentVel = Vector3.zero;
		
		Vector3 pointMomentum = Vector3.zero;
		
		float myRadius = this.collider ? this.collider.bounds.extents.magnitude : 0;
		
		float myRotationSpeed = this.rigidbody ? this.rigidbody.rigidbody.angularVelocity.magnitude : 0;
		
		foreach(Transform child in children){
			
			DetachChild(child,myRotationSpeed,myRadius);
			
		}
		
		if(despawnAfterDetach){
			
			Despawn();
			
		}
		
	}
	
	protected void Despawn(){
		
		if( PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName) ){
			
			PathologicalGames.PoolManager.Pools[originSpawnPoolName].Despawn(this.transform);
			
		}
		else{
			
			Destroy(  this.gameObject );
			
		}
		
	}

	protected IEnumerator CreateCluster () {
		
		yield return new WaitForFixedUpdate(); //wait until the fixed (physics) update frame
		
		Transform[] children = this.GetComponentsInChildren<Transform>(true);
		
		foreach(Transform child in children){
			
			child.gameObject.SetActive(true);
			
		}
		
		if(detachChildrenImmediately){
		
			DetachAll();
			
		}
		
	}
	
	virtual protected void DetachChild(Transform child, float angularRotSpeed, float bodyRadius){				
		
		if (child == null){
			
			throw new System.ArgumentNullException ("child");
			
		}
		
		child.gameObject.SetActive(true);
			
		if(child.rigidbody && this.rigidbody){
			
			Vector3 childVel = Vector3.zero;
			
			 child.rigidbody.velocity = this.rigidbody.velocity;
			
			child.rigidbody.AddTorque( this.transform.localToWorldMatrix.MultiplyVector(this.rigidbody.angularVelocity),ForceMode.VelocityChange );
			
		}
		
		child.parent = this.transform.parent;
		
	}
	
}
