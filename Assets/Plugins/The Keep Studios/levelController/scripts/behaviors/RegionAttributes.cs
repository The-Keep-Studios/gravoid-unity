using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RegionAttributes : SpawnProperties
{
	/// <summary>
	/// The size of the spawners within this region.
	/// Zones will attempt to be tesselated as best they can
	/// to be filled with SpawnAreas of this size.
	/// </summary>
	public Vector3 spawnerSize;
	
	/// <summary>
	/// Things which are randomly spawned and distributed in zones of this region when they are
	/// activated.
	/// Their Bounding Boxes must fit at any rotation within the a box the size of spawnerSize.
	/// </summary>
	public List<Transform> ThingsFoundInRegion {
		get {
			return spawnables;
		}
		set{
			spawnables = value;
		}
	}
	
	/// <summary>
	/// Does this region repopulate when it is activated after the first activation.
	/// </summary>
	public bool repopulates;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
}