
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public List<SpawnArea> spawnAreas;

	public PathologicalGames.SpawnPool spawnPool;

	public string propertiesFilePath;

	public SpawnProperties randomizationConstraints;

	protected List<SpawnArea> unactivatedSpawnAreas;

	protected List<Transform> thingsSpawned = new List<Transform>();
	
	public void Activate ( SpawnProperties props ) {
		
		this.randomizationConstraints = props;
		
		unactivatedSpawnAreas = Randomizer.GetRandomList ( spawnAreas );
		
		StartCoroutine ( Spawn () );
		
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected IEnumerator Spawn () {
		
		thingsSpawned.Clear();
		
		yield return new WaitForFixedUpdate(); //wait until the fixed (physics) update frame
		
		bool spawningPossible = true;
		
		while ( this.unactivatedSpawnAreas.Count > 0 && spawningPossible ) {
		
			bool successFullySpawned = false;
			
			spawningPossible = false;
			
			for (
				int nextSpawnArea_idx = 0;
				nextSpawnArea_idx < this.unactivatedSpawnAreas.Count && !successFullySpawned;
				++nextSpawnArea_idx 
			) {
			
				SpawnArea nextSpawnArea = this.unactivatedSpawnAreas [ nextSpawnArea_idx ];
				
				successFullySpawned = this.SpawnNext ( nextSpawnArea );
				
				if ( successFullySpawned ) {
					
					this.unactivatedSpawnAreas.RemoveAt ( nextSpawnArea_idx );
					
				}
				
			}
			
			if(this.randomizationConstraints.secondsDelayedUntilSpawn > 0){
					
				//pause spawning for a period of time
				yield return new WaitForSeconds(
						this.randomizationConstraints.secondsDelayedUntilSpawn
					);
				
			}
			
			spawningPossible = successFullySpawned;
			
		}
		
		//print ( "Halting spawning ..." + this.ToString () );
		
	}

	
	protected bool SpawnNext ( SpawnArea spawnArea ) {
		
		if ( spawnArea == null ) {
			throw new System.ArgumentNullException ( "spawnArea" );
		}

		
		//get a list of random tranforms (things)
		List<Transform> things = 
			Randomizer.GetRandomList<Transform> ( this.randomizationConstraints.spawnables );
		
		//find a thing that can be spawned in the area!
		foreach ( Transform randThing in things ) {
			
			Transform thingSpawned = spawnArea.spawn ( this.spawnPool, randThing, Random.rotation.eulerAngles );
			
			if ( thingSpawned != null ) {
				
				this.thingsSpawned.Add(thingSpawned);
				
				return true;
				
			}
			
		}
		
		return false;
		
	}
	
}
