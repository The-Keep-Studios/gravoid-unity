using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegionSelector : MonoBehaviour {
	
		
	[SerializeField]
	protected List<Region> regions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	virtual public Region GetRandomConnectableRegion(List<Region> neighboringRegions){
		
		//neighboringRegions are currently ignored, later they will be used to determine candidate regions
		if( regions.Count != 0 ){
			
			return this.regions[ Random.Range(0, regions.Count) ];
			
		}
		else{
			
			return null ;
			
		}
		
	}
		
}
