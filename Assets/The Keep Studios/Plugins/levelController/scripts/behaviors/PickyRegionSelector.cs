using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickyRegionSelector : RegionSelector
{
	
	/// <summary>
	/// The fallback region, which is returned if NO other region was found which was valid to be
	/// selected.
	/// </summary>
	public List<Region> fallbackRegions;
	[UnityEngine.SerializeField]
	private List<Stratum> stratums;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	override public Region GetRandomConnectableRegion (List<Region> neighboringRegions)
	{
		
		//todo fix this so we aren't using a cast.
		List<Stratum> remainingStratums = Randomizer.GetRandomList<Stratum> (this.stratums);
		
		/*
		 * neighboringRegions are currently ignored, later they will be used to determine 
		 * candidate regions
		 */
		foreach (Stratum next in remainingStratums) {
			
			if (next.isValidSelection (neighboringRegions)) {
				
				return next.Select ();
				
			}
			
		}
		
		//we didn't find a suitable region type, use one of the fallbacks which may be placed anywhere.
		if (fallbackRegions.Count > 0) {
			
			int randomDefault_idx = Random.Range (0, fallbackRegions.Count - 1);
		
			return fallbackRegions [randomDefault_idx];
			
		}
		
		Debug.LogError (
			"No useable region type found, we are filled with much sadness and we give up."
			+ "Note: This is VERY bad, as it means we cannot determine a suitable region type, for a zone."
		);
		
		return null; //
		
	}
	
	[System.Serializable]
	internal class Stratum
	{
		
		[UnityEngine.SerializeField]
		private Region myRegion;
		[UnityEngine.SerializeField]
		private Criteria selectionCriteria = new Criteria ();
		private uint timesSelected = 0;
		
		[System.Serializable]
		public class Criteria
		{
				
			public bool isLimitedNumber = true;
			public int maxInExistance = 0;
			public List<Region> possibleNeighborTypes;
			public int maxNeighborMismatches = 8;
				
		}
			
	
		// Use this for initialization
		void Start ()
		{
			
		}
			
		public bool isValidSelection (List<Region> neighboringRegions)
		{
				
			//attempt to find reasons we fail, returning false the instant we have failed
				
			//check if we've exceeded our limit
			if (this.selectionCriteria.isLimitedNumber 
					&& this.timesSelected >= this.selectionCriteria.maxInExistance 
				) {
					
				return false;
					
			}
				
			int unmatchedNeighbors = 0;
				
			foreach (Region neighbor in neighboringRegions) {
				
				bool matched = false;
					
				foreach (Region allowedNeighbor in this.selectionCriteria.possibleNeighborTypes) {
						
					if (allowedNeighbor == neighbor) {
							
						matched = false;
							
						break; //found a match, we can stop looking for this
							
					}
						
				}
					
				if (!matched) {
					
					if ((++unmatchedNeighbors) >= this.selectionCriteria.maxNeighborMismatches) {
					
						return false;
						
					}
					
				}
					
			}
			
			return true;
				
		}
			
		public Region Select ()
		{
				
			++(this.timesSelected);
				
			return this.myRegion;
				
		}
			
	}
	
}
