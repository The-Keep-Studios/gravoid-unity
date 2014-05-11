
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[UnityEngine.RequireComponent(typeof(RegionSelector))]
[UnityEngine.RequireComponent(typeof(LevelAttributes))]
/// <summary>
/// Simple Level controller. 
/// TODO Should be split into a "BaseLevelController" and "SimpleLevelController".
/// </summary>
public class LevelController : MonoBehaviour {

	[UnityEngine.SerializeField]
	/// <summary>
	/// The zones which have been created.
	/// </summary>
	protected Dictionary<ZoneId, Zone> zones = new Dictionary<ZoneId, Zone> ();
	
	protected Vector3 lastUsedZoneDimensions;

	protected List<Vector3> neighborsRelativeGridLocation = new List<Vector3> ( 8 );

	protected List<Vector3> foreignNeighborRelativeGridLocation = new List<Vector3> ( 16 );

	protected List<Vector3> neighborOffsets = new List<Vector3> ( 8 );

	protected List<Vector3> foreignNeighborOffsets = new List<Vector3> ( 16 );

	static protected LevelController instance;


	public static LevelController  GetInstance () {
		if ( !LevelController.instance ) {
			LevelController.instance = (LevelController)FindObjectOfType ( typeof(LevelController) );
			if ( !LevelController.instance ) {
				Debug.LogError ( "There needs to be one active LevelControllerBehavior script on a GameObject in your scene." );
			}
		}
		return LevelController.instance;
	}

	// Use this for initialization
	void Start () {
		
		LevelController.instance = this;
		
		//Setup neighbor relative positions
		this.neighborsRelativeGridLocation.Add ( Vector3.up );
		this.neighborsRelativeGridLocation.Add ( Vector3.down );
		this.neighborsRelativeGridLocation.Add ( Vector3.left );
		this.neighborsRelativeGridLocation.Add ( Vector3.right );
		this.neighborsRelativeGridLocation.Add ( Vector3.right + Vector3.up );
		this.neighborsRelativeGridLocation.Add ( Vector3.left + Vector3.up );
		this.neighborsRelativeGridLocation.Add ( Vector3.right + Vector3.down );
		this.neighborsRelativeGridLocation.Add ( Vector3.left + Vector3.down );
		
		//Setup foreign neighbor relative positions
		//cardinal directions
		this.foreignNeighborRelativeGridLocation.Add ( 2 * Vector3.up );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * Vector3.down );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * Vector3.left );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * Vector3.right );
		
		//corners
		this.foreignNeighborRelativeGridLocation.Add ( 2 * (Vector3.right + Vector3.up) );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * (Vector3.left + Vector3.up) );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * (Vector3.right + Vector3.down) );
		this.foreignNeighborRelativeGridLocation.Add ( 2 * (Vector3.left + Vector3.down) );
		
		//offcenter sides
		this.foreignNeighborRelativeGridLocation.Add ( (2 * Vector3.right) + Vector3.up );
		this.foreignNeighborRelativeGridLocation.Add ( (2 * Vector3.left) + Vector3.up );
		this.foreignNeighborRelativeGridLocation.Add ( (2 * Vector3.right) + Vector3.down );
		this.foreignNeighborRelativeGridLocation.Add ( (2 * Vector3.left) + Vector3.down );
		
		//offcenter top/bottom
		this.foreignNeighborRelativeGridLocation.Add ( Vector3.right + (2 * Vector3.up) );
		this.foreignNeighborRelativeGridLocation.Add ( Vector3.left + (2 * Vector3.up) );
		this.foreignNeighborRelativeGridLocation.Add ( Vector3.right + (2 * Vector3.down) );
		this.foreignNeighborRelativeGridLocation.Add ( Vector3.left + (2 * Vector3.down) );
		//done setting up foriegn neighbors
			
		UpdateNeighborOffsets ( true );
		
		//wakes up the zone at {0,0,0} and creates it if it's missing
		WakeZone ( new ZoneId ( Vector3.zero ), null );
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// Wakes the zone. If the zone is not yet instantiated will spawn a new zone and wake that.
	/// </summary>
	/// <param name='id'>
	/// Identifier of the zone. If a new zone is needed this will also define it's center.
	/// </param>
	/// <param name='zoneEnterer'>
	/// Zone enterer that triggered the zone's awakening.
	/// </param>
	public void WakeZone ( ZoneId id, Collider zoneEnterer ) {
		
		Zone zone = GetZoneById ( id, true );
		
		if ( zone != null ) {
		
			zone.WakeUp ( zoneEnterer );
			
		}
		else {
			
			Debug.LogError ( "Zone creation failed when attempting to wake the zone at:\n" + id.ToString () );
			
		}
		
	}

	
	public void SleepZone ( ZoneId id, Collider zoneExiter ) {
		
		Zone zone = GetZoneById ( id, false );
		
		if ( zone != null ) {
		
			zone.GoToSleep ( zoneExiter );
			
		}
		
	}

	
	protected void UpdateNeighborOffsets () {
	
		UpdateNeighborOffsets ( false );
		
	}

	
	protected void UpdateNeighborOffsets ( bool forceRecalculation ) {
		
		Vector3 zoneDimensions = this.GetComponent<LevelAttributes> ().zoneDimensions;
		
		if ( forceRecalculation || zoneDimensions != this.lastUsedZoneDimensions ) {
			
			this.neighborOffsets.Clear ();
			
			this.foreignNeighborOffsets.Clear ();
			
			this.lastUsedZoneDimensions = zoneDimensions;
			
			//update the neighbors
			foreach ( Vector3 nextLoc in this.neighborsRelativeGridLocation ) {

				Vector3 neighborPosition = VectorOperators.Multiply ( nextLoc, zoneDimensions );
				
				this.neighborOffsets.Add ( neighborPosition );
				
				
			}
			
			//now update foreign neighbors
			foreach ( Vector3 nextLoc in this.foreignNeighborRelativeGridLocation ) {
				
				Vector3 fNeighborPosition = VectorOperators.Multiply ( nextLoc, zoneDimensions );
				
				this.foreignNeighborOffsets.Add ( fNeighborPosition );
				
			}
			
		}
		
	}

	
	public List<ZoneId> GetNeighborZoneIds ( ZoneId id ) {
		
		//assume we start with 8 neighbors, since this is fairly normal
		List<ZoneId> neighborIds = new List<ZoneId> ( 8 );
		
		UpdateNeighborOffsets ();
		
		foreach ( Vector3 nextNeighborDir in this.neighborOffsets ) {
			
			ZoneId nextZoneId;
			
			//calculate the neighbors center location, this determines the id
			nextZoneId.zoneCenter = nextNeighborDir + id.zoneCenter;
			
			neighborIds.Add ( nextZoneId );
			
		}
		
		return neighborIds;
		
	}
	
	
	public List<ZoneId> GetForeignNeighborZoneIds ( ZoneId id ) {
		
		//assume we start with 8 neighbors, since this is fairly normal
		List<ZoneId> neighborIds = new List<ZoneId> ( 8 );
			
		UpdateNeighborOffsets ();
		
		foreach ( Vector3 nextNeighborDir in this.foreignNeighborOffsets ) {
			
			ZoneId nextZoneId;
			
			//calculate the neighbors center location, this determines the id
			nextZoneId.zoneCenter = nextNeighborDir + id.zoneCenter;
			
			neighborIds.Add ( nextZoneId );
			
		}
		
		return neighborIds;
		
	}

	
	protected List<Region> GetNeighborRegions ( ZoneId id ) {
		
		List<ZoneId> zoneIds = GetNeighborZoneIds ( id );
		
		//assume we start with 8 neighbors, since this is fairly normal
		List<Region> regions = new List<Region> ( 8 );
		
		foreach ( ZoneId nextId in zoneIds ) {
			
			Zone nextInfo = GetZoneById ( nextId, false );
			
			if ( nextInfo != null && nextInfo.Type != null ) {
				
				regions.Add ( nextInfo.Type );
				
			}
			
		}
		
		return regions;
	}

	
	protected Zone GetZoneById ( ZoneId id, bool spawnIfMissing ) {
		
		Zone zone = null;
		
		bool exists = this.zones.ContainsKey ( id );
		
		if ( !exists && spawnIfMissing ) {
			
			List<Region> neighborRegionTypes = this.GetNeighborRegions ( id );
			
			Region regionType = 
				this.GetComponent<RegionSelector> ().GetRandomConnectableRegion ( 
					neighborRegionTypes
				);
			
			zone = regionType.SpawnZone (
					id
					, this.GetComponent<LevelAttributes> ().zoneDimensions
				);
			
			this.zones.Add ( id, zone );
			
		}
		else if ( exists ) {
		
			zone = this.zones [ id ];
			
		}
		
		return zone;
		
	}
	
}
