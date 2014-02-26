using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class SpawnArea : MonoBehaviour {
	/**
	 * For tracking the number of colliders which are occupying the area
	 */
	[SerializeField]
	private int numberOccupying = 0;
	
	/*
	 * For telling if the spawn area is already occupied
	 */
	public bool Occupied {
		get {
			return numberOccupying > 0;
		}
	}
	
	// Use this for initialization
	void Start () {
		//this.rigidbody.isKinematic = true; // avoid gravity or other physics effects
		this.collider.isTrigger = true; //turn on trigger effect
		numberOccupying = 0;//init the number of other objects occupying the space
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(!other.isTrigger){//we ignore triggers, (TODO investigate if we should)
			numberOccupying += 1;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(!other.isTrigger){//we ignore triggers, (TODO investigate if we should)
			numberOccupying -= 1;
		}
	}
	
	bool canFit(Collider spawnable){
		
		Vector3 mySize = this.collider.bounds.size;
		
		float myMinSize = Mathf.Min(new float[] {mySize.x, mySize.y, mySize.z});
		
		float myMinHypotonus = ((myMinSize*myMinSize)*2);//it's spelled wrong, and this amuses me
		
		float theirDiagSqrSize = (spawnable.bounds.max - spawnable.bounds.min).sqrMagnitude;
		
		//can the bounds of the spawnable be contained at all possible rotations within my bounds
		return theirDiagSqrSize <= myMinHypotonus;
		
	}
	
	bool canSpawn(Collider spawnable){
			
		return !this.Occupied && this.canFit(spawnable);
		
	}
	
	/**
	 * Attempts to spawns a copy of spawnable from pool at the center of the SpawnAreaBehavior's collider rotated by initRot.
	 * 
	 * @return the transform of the spawned object, null if unsuccessful
	 */
	public Transform spawn(PathologicalGames.SpawnPool pool, Transform spawnable, Vector3 initRot){
		
		Transform thingSpawned = pool.Spawn(spawnable);
		
		if( thingSpawned != null && !this.canSpawn(thingSpawned.collider) ){
			
			//failed to fit the object, despawn it!
			pool.Despawn(thingSpawned);
			
			thingSpawned = null;
			
		}
		else{
				
			Vector3 oldPos = thingSpawned.position;
			
			Vector3 oldRot = thingSpawned.eulerAngles;
			
			Vector3 newPos = this.collider.bounds.center;
			
			Vector3 newRot = initRot;
			
			if( thingSpawned.rigidbody ){
				
				//check the constraints and reset the new coords to old coords where contraints are set
				RigidbodyConstraints c = thingSpawned.rigidbody.constraints;
				
				newPos.x = (c & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX ? oldPos.x : newPos.x;
				newPos.y = (c & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY ? oldPos.y : newPos.y;
				newPos.z = (c & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ ? oldPos.z : newPos.z;
				
				newRot.x = (c & RigidbodyConstraints.FreezeRotationX) == RigidbodyConstraints.FreezeRotationX ? oldRot.x : newRot.x;
				newRot.y = (c & RigidbodyConstraints.FreezeRotationY) == RigidbodyConstraints.FreezeRotationY ? oldRot.y : newRot.y;
				newRot.z = (c & RigidbodyConstraints.FreezeRotationZ) == RigidbodyConstraints.FreezeRotationZ ? oldRot.z : newRot.z;
				
			}
			
			thingSpawned.position = newPos;
			
			thingSpawned.Rotate(newRot);
			
		}
		
		return thingSpawned;//if we've not yet returned, we've failed so return null
		
	}
	
}
