
using UnityEngine;
using System.Collections.Generic;

public class ZoneDemo : MonoBehaviour {
	
	public Region region;
	
	public Vector3 size;
	
	public Zone generatedZone;
	
	public Transform zoneAnchorPrefab;
	
	public PathologicalGames.SpawnPool pool;
	
	public Transform zoneAnchor;
		
	public Vector3 zoneCenter = Vector3.zero;

	// Use this for initialization
	void Start () {
		
		print ( "Creating a zone to demonstrate it's spawning properties." );
		
		ZoneId id = new ZoneId ( zoneCenter );
		
		generatedZone = this.region.SpawnZone ( id, this.size );
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate(){
		
		if( zoneAnchor == null ){
		
			print ( "Creating a zone anchor in the zone's area" );
			
			Quaternion rotation = new Quaternion ();
			
			rotation.SetLookRotation ( Vector3.up );
			
			zoneAnchor = pool.Spawn ( zoneAnchorPrefab, generatedZone.Center, rotation );
			
		}
		
	}
	
}
