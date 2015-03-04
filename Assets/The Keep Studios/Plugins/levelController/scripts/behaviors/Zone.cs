
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates spawners and spawn areas within a preset zone size.
/// When the zone is activated it will call a one time spawn on the spawners to populate the zone.
/// 
/// "Controls" anything within the zone, capable of mass pausing of the area (aka sleeping a zone).
/// 
/// Can be put into "hibernation", aka a long term sleep which saves the state of the zone and all
/// it's contolled objects, so that they can be unloaded from the engine. Good for both saving the
/// games state and also unloading a zone once it is so far out of sight from any activating force
/// that keeping it in memory is a waste.
/// </summary>
[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(Spawner))]
public class Zone : MonoBehaviour {
	
	public const int sleepThreshold = 0;
	
	[UnityEngine.SerializeField]
	protected Transform spawnAreaPrefab;
	
	[UnityEngine.SerializeField]
	protected Region region;
	
	[UnityEngine.SerializeField]
	protected List<Collider> wakers;


	public int WakeLevel {
		get {
			return this.wakers.Count;
		}
	}

	
	public Vector3 Size {
		get {
			return this.GetComponent<BoxCollider> ().size;
		}
		set {
			this.GetComponent<BoxCollider> ().size = value;
		}
	}

		
	public Vector3 Center {
		get {
			
			return this.GetComponent<BoxCollider> ().center + this.transform.position;
		}
		set {
			this.GetComponent<BoxCollider> ().center = value - this.transform.position;
		}
	}

	
	public ZoneId Id {
		get {
			
			return new ZoneId ( this.Center );
		}
		set {
			this.Center = value.zoneCenter;
		}
	}

	
	public Region Type {
		get {
			return this.region;
		}
	}

	
	public Spawner ZonePopulator {
		get {
			return GetComponent<Spawner> ();
		}
	}
	
	// Use this for initialization
	void Start () {
		
		this.GetComponent<Collider>().isTrigger = true; //turn on trigger effect just in case
		
		if ( wakers == null ) {
			
			wakers = new List<Collider> ();
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter ( Collider other ) {
		
		if ( other.GetComponent<ZoneAnchor> () != null ) {
		
			Debug.Log ( "Zone Entered by \n\t" + other.GetComponent<Collider>().gameObject.ToString () );
			
			SleepZones ( other );
			
			WakeZones ( other );
			
		}
		
	}
	
	/// <summary>
	/// Initialize the zone with the specified size.
	/// 
	/// Tesselates the zone with spawn areas as best it can.
	/// Note that if the spawn area sizes do not evenly divided the area the
	/// tesselation will spill out of the zone.
	/// </summary>
	/// <param name='size'>
	/// Size of the zone.
	/// </param>
	public void Initialize ( ZoneId id, Vector3 size, Region region ) {
		
		this.transform.position = id.zoneCenter;
		
		this.region = region;
		
		this.Size = size;
		
		Debug.Log (
			"Initializing Zone\n"
			+ "\t id:[" + id.zoneCenter.x + "," + id.zoneCenter.y + "," + id.zoneCenter.z + "]"
			+ "\t size:[" + size.x + "," + size.y + "," + size.z + "]"
			+ "\t region" + region.ToString ()
		);
		
		ZonePopulator.spawnPool = region.objectPool;
		
		CreateSpawnAreaMatrix ( region.Attributes );
		
	}

	
	public void WakeUp ( Collider waker ) {
		
		if ( waker == null ) {
			
			// a null waker means this is a dry activation to populate the zone
			this.ZonePopulator.Activate ( this.Type.Attributes );
			
		}
		else if ( waker.GetComponent<ZoneAnchor> () != null ) {
			
			//wake when the collider has a ZoneAnchor component
		
			int oldWakeLevel = this.WakeLevel;
			
			this.wakers.Add ( waker );
			
			if ( oldWakeLevel <= Zone.sleepThreshold && this.WakeLevel > Zone.sleepThreshold ) {
				
				this.ZonePopulator.Activate ( this.Type.Attributes );
				
			}
			
		}
		
	}

	
	public void GoToSleep ( Collider leaver ) {
		
		int oldWakeLevel = this.WakeLevel;
		
		this.wakers.Remove ( leaver );
		
		if ( oldWakeLevel > Zone.sleepThreshold && this.WakeLevel <= Zone.sleepThreshold ) {
			
			Debug.LogError ( "TODO Sleep the zone not implemented." );
			
		}
		
	}


	protected void WakeZones ( Collider waker ) {
		
		LevelController zoneRegistry = LevelController.GetInstance ();
		
		if ( zoneRegistry != null ) {
			
			List<ZoneId> neighbors = zoneRegistry.GetNeighborZoneIds ( Id );
			
			Debug.Log( "Waking neighbor zones of: "+ Id.zoneCenter.ToString() );
			
			foreach ( ZoneId borderZoneId in neighbors ) {
				
				Debug.Log( "Waking zone with center at: "+ borderZoneId.zoneCenter.ToString() );
			
				zoneRegistry.WakeZone ( borderZoneId, waker );
			
			}
			
		}
		else {
			
			Debug.LogError ( "Unable to look up neighbor zones to wake them." );
			
		}
		
		this.WakeUp ( waker );
		
	}

	
	protected void SleepZones ( Collider leaver ) {
		
		LevelController zoneRegistry = LevelController.GetInstance ();
		
		if ( zoneRegistry != null ) {
			
			foreach ( ZoneId borderZoneId in zoneRegistry.GetForeignNeighborZoneIds(Id) ) {
				
				zoneRegistry.SleepZone ( borderZoneId, leaver );
				
			}
			
		}
		else {
			
			Debug.LogError ( "Unable to look up foreign neighbors to put them to sleep." );
			
		}
			
		this.GoToSleep ( leaver );
			
	}

	/// <summary>
	/// Handles converting a zone's dimension  into an array dimension
	/// </summary>
	/// <returns>
	/// The converted matrix dimension.
	/// </returns>
	/// <param name='zoneDimension'>
	/// Zone's dimension. (a width, height or length)
	/// </param>
	/// <param name='spawnerDimension'>
	/// Spawner dimension corrisponding to the Zone's dimension. (a width, height or length)
	/// </param>
	private int convertToMatrixDimension ( float zoneDimension, float spawnerDimension ) {
		return Mathf.Max( Mathf.FloorToInt ( zoneDimension / spawnerDimension ) - 1, 0);
	}

	
	private Vector3 calcTtesselationStart ( Vector3 matrixLayout, Vector3 spawnerSize ) {
		
		Vector3 ts = this.GetComponent<Collider>().bounds.min + (spawnerSize / 2);
		
		if ( matrixLayout.x == 0 ) {
			
			ts.x = 0;
			
		}
		
		if ( matrixLayout.y == 0 ) {
			
			ts.y = 0;
			
		}
		
		if ( matrixLayout.z == 0 ) {
			
			ts.z = 0;
			
		}
		
		return ts;
		
	}

	
	protected void CreateSpawnAreaMatrix ( RegionAttributes attributes ) {
		
		Vector3 matrixLayout = new Vector3 (
				convertToMatrixDimension ( this.Size.x, attributes.spawnerSize.x )
				, convertToMatrixDimension ( this.Size.y, attributes.spawnerSize.y )
				, convertToMatrixDimension ( this.Size.z, attributes.spawnerSize.z )
			);
		
		Vector3 tesselationStart = calcTtesselationStart ( matrixLayout, attributes.spawnerSize );
		
		
		Debug.Log (
			"Creating A Spawn Area Matrix\n"
			+ "\t size:[" + this.Size.x + "," + this.Size.y + "," + this.Size.z + "]"
			+ "\t matrixLayout:[" + matrixLayout.x + "," + matrixLayout.y + "," + matrixLayout.z + "]"
			+ "\t tesselationStart:[" + tesselationStart.x + "," + tesselationStart.y + "," + tesselationStart.z + "]"
			+ "\t spawnerSize:[" + attributes.spawnerSize.x + "," + attributes.spawnerSize.y + "," + attributes.spawnerSize.z + "]"
		);
		
		for ( int nextLayer = 0; nextLayer <= matrixLayout.z; ++nextLayer ) {
			
			for ( int nextColumn = 0; nextColumn <= matrixLayout.y; ++nextColumn ) {
				
				for ( int nextRow = 0; nextRow <= matrixLayout.x; ++nextRow ) {
					
					Vector3 nextPos = new Vector3 ( nextRow, nextColumn, nextLayer );
					
					CreateSpawnAreaAtMatrixCoords ( nextPos, tesselationStart, attributes );
					
				}
				
			}
			
		}
		
	}


	protected void CreateSpawnAreaAtMatrixCoords ( 
		Vector3 matrixPosition, Vector3 matrixMinPoint, RegionAttributes attributes ) {
		
		/*
		Debug.Log (
			"Creating A Spawn Area Within Matrix Cell\n"
			+ "\t matrixPosition:[" + matrixPosition.x + "," + matrixPosition.y + "," + matrixPosition.z + "]"
			+ "\t matrixMinPoint:[" + matrixMinPoint.x + "," + matrixMinPoint.y + "," + matrixMinPoint.z + "]"
		);
		*/
		
		Transform nextSpawnAreaTransform = this.region.spawnAreaPool.Spawn ( this.spawnAreaPrefab );
		
		if ( nextSpawnAreaTransform != null ) {
			
			SpawnArea nextSpawnArea = nextSpawnAreaTransform.GetComponent<SpawnArea> ();
			
			if ( nextSpawnArea != null ) {
				
				//ready to calculate the center point
				Vector3 nextSpawnAreaCenter = 
					VectorOperators.Multiply ( attributes.spawnerSize, matrixPosition ) + matrixMinPoint;
				
				/*
				Debug.Log (
					"Placing The Spawn Area" + nextSpawnArea.ToString () + "\n"
					+ "\t nextSpawnAreaCenter:[" + nextSpawnAreaCenter.x
					+ "," + nextSpawnAreaCenter.y
					+ "," + nextSpawnAreaCenter.z + "]"
				);
				*/
			
				/*
				 * we know that if nextSpawnArea is not null, it has a BoxCollider because it is a
				 * required component
				 */
				BoxCollider bbox = nextSpawnArea.GetComponent<BoxCollider> ();
				
				bbox.size = attributes.spawnerSize;
				
				bbox.center = nextSpawnAreaCenter;
			
				this.ZonePopulator.spawnAreas.Add ( nextSpawnArea );
			
				return; //we can exit now, as we have completed successfully
		
			}
			
		}
		
		//something went wrong, and we didn't create the spawn area as expected
		Debug.LogError ( 
			"Unable to create a new spawn area at " 
				+ (matrixPosition + matrixMinPoint).ToString ()
		);
		
	}
	
}
