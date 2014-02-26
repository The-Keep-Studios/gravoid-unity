
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Regions contain data about laying out zones of their specific region type.
/// They can spawn new GameObjects with ZoneControllerBehaviors.
/// This spawned ZoneControllerBehaviors are tracked by the Region, and if the
/// region type wishes it may later modify the properties of the zones through
/// the ZoneControllerBehaviors. For example, if an new enemy type should
/// spawn in the zones the region could tell all the existing and all future
/// ZoneControllerBehaviors about this event.
/// 
/// This behavior is not as of yet implemented, but it is planned for in the 
/// design as a "core" feature.
/// </summary>
[UnityEngine.RequireComponent(typeof(RegionAttributes))]
public class Region : MonoBehaviour {
	
	[UnityEngine.SerializeField]
	/// <summary>
	/// The zone template used to create new zones of this region type
	/// </summary>
	protected Zone zoneTemplate;
	
	[UnityEngine.SerializeField]
	protected List<Zone> zones;
	
	/// <summary>
	/// The zone pool to spawn the zones from
	/// </summary>
	[UnityEngine.SerializeField]
	protected PathologicalGames.SpawnPool zonePool;
	
	public PathologicalGames.SpawnPool objectPool;
	
	public PathologicalGames.SpawnPool spawnAreaPool;

	
	public RegionAttributes Attributes {
		get {
			return this.GetComponent<RegionAttributes> ();
		}
	}

	// Use this for initialization
	void Start () {
		
		//create a new zone spawn pool if we have not been provided withone
		if ( this.zonePool == null ) {
			
			string zonePoolName = "Zones";
			
			if ( !PathologicalGames.PoolManager.Pools.ContainsKey ( zonePoolName ) ) {
				
				PathologicalGames.PoolManager.Pools.Create ( zonePoolName );
				
			}
			
			this.zonePool = PathologicalGames.PoolManager.Pools [ zonePoolName ];
			
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Spawns the zone controller.
	/// </summary>
	/// <returns>
	/// The zone controller.
	/// </returns>
	public Zone SpawnZone ( ZoneId id, Vector3 size ) {
		
		Transform spawnedTransform = this.zonePool.Spawn ( this.zoneTemplate.transform );
		
		Zone zone = spawnedTransform.GetComponent<Zone> ();
		
		if ( zone != null ) {
		
			zones.Add ( zone );
			
			zone.Initialize ( id, size, this );
			
		}
		
		return zone;
		
	}
	
}
