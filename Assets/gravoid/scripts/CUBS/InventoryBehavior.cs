using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid
{
	public class InventoryBehavior: MonoBehaviour
	{
	
	#region PRIVATE PROPERTIES
		[SerializeField]
		private Map
			m_counts;
	#endregion

	#region PUBLIC METHODS
		public bool Insert (PartSelectionBehavior _selection)
		{
		
			PartSelectionBehavior type = _selection.GetPartType ();
		
			int count = this.GetCount (type);
		
			this.SetCount (type, count + 1);
		
			_selection.Despawn ();
		
			return true;
		
		}

		public PartSelectionBehavior Take (PartSelectionBehavior _selectionPrefab)
		{
		
			PartSelectionBehavior takenselection = null;
		
			if (this.GetCount (_selectionPrefab) > 0) {
			
				takenselection = PartSelectionBehavior.Spawn (_selectionPrefab);
			
				if (takenselection != null) {
				
					//successfully retrieved selection, decriment the count
					int newCount = this.GetCount (_selectionPrefab) - 1;
				
					this.SetCount (_selectionPrefab, newCount);
				
				}
			
			}
		
			return takenselection;
		
		}
	
		public ProjectileBehavior GetProjectile (List<PartSelectionBehavior> selection, ProjectileBehavior projectilePrefab)
		{
		
			ProjectileBehavior projectile = null;
	
			/*
		 * TRY and get the selected selections from the inventory, and instantiate a
		 * projectile (all this logic SHOULD be moved to the inventory)
		 */
		
			List<PartSelectionBehavior> selections = this.GetSelections (selection);
		
			List<ProjectilePartBehavior> partPrefabs = selections.ConvertAll<ProjectilePartBehavior> (PartSelectionBehavior.GetPartPrefabFromSelection);
		
			projectile = ProjectileBehavior.Spawn (partPrefabs, projectilePrefab);
		
			if (projectile == null || !projectile.CanBeLaunched ()) {
			
				foreach (PartSelectionBehavior nextSelection in selections) {
				
					//place each of the selections back in the inventory, as we cannot use them after all
					this.Insert (nextSelection);
				
				}
			
			} else {
			
				foreach (PartSelectionBehavior nextSelection in selections) {
				
					nextSelection.Despawn ();//selections are now considered "used" and should be despawned 
				
				}
			
				projectile.ignoreCollisionsWith (this.collider);
			
			}
	
			return projectile;
	
		}
	
		public void Start ()
		{
		
			if (this.m_counts == null) {
				this.m_counts = new Map ();
			}
		
			this.m_counts.valueWhenKeyIsMissing = 0;
		}

		public void Update ()
		{
		
		}
	
		private void SetCount (PartSelectionBehavior _selectionPrefab, int newVal)
		{
		
			this.m_counts.setData (_selectionPrefab, newVal);
		
		}

	#endregion
	#region PRIVATE METHODS
		private int GetCount (PartSelectionBehavior _selectionPrefab)
		{
			return this.m_counts.getData (_selectionPrefab);
		
		}
	
		private List<PartSelectionBehavior> GetInsufficientselections (List<PartSelectionBehavior> _selectionPrefabList)
		{
		
			List<PartSelectionBehavior> missing = new List<PartSelectionBehavior> (_selectionPrefabList.Count);
		
			Dictionary<PartSelectionBehavior,int> counts = new Dictionary<PartSelectionBehavior, int> (_selectionPrefabList.Count);
		
			if (_selectionPrefabList != null) {
			
				foreach (PartSelectionBehavior next in _selectionPrefabList) {
				
					if (!counts.ContainsKey (next)) {
					
						counts.Add (next, 0);
					
					}
				
					int available = GetCount (next);
				
					/*
				 * this check is a bit screwy looking, because it must activate 
				 * ONLY on the first found insufficient selection.
				 */
					if (counts [next] == available) {
						//if the last count is equal to the available, then we are lacking the selection
						missing.Add (next.GetComponent<PartSelectionBehavior> ());
					
					}
				
					counts [next] += 1;
				
				}
			
			}
		
			return missing;
		
		}
	
		private List<PartSelectionBehavior> GetSelections (List<PartSelectionBehavior> _selection)
		{
		
			List<PartSelectionBehavior> selections = new List<PartSelectionBehavior> (_selection.Count);
		
			//TODO refactor this, the local variable is unnecessary
			List<PartSelectionBehavior> lackingComponents = this.GetInsufficientselections (_selection);
		
			if (lackingComponents.Count == 0) {
			
				foreach (PartSelectionBehavior nextselectionType in _selection) {
				
					PartSelectionBehavior nextselection = this.Take (nextselectionType);
				
					selections.Add (nextselection);
				
				}
			
			} else {
			
				this.DisplayInsufficientselections (lackingComponents);
			
			}
		
			return selections;
		
		}
	
		private void DisplayInsufficientselections (List<PartSelectionBehavior> _selectionTypes)
		{
			string output = "Missing: %s" + _selectionTypes.ToArray ().ToString () + "; ";
	
			//string missingselectionName = _selectionTypes[1].GetType().ToString();
	
			//TODO Send notification to the user
	
		}
	#endregion
	
	#region INTERNAL CLASSES
		[System.Serializable]
		internal class Map: DataMap<PartSelectionBehavior,int,Link>
		{
		}
	
		[System.Serializable]
		internal class Link: Link<PartSelectionBehavior,int>
		{ 
		
			public PartSelectionBehavior m_selectionType;
			public int m_remainingInInventory;

			public override PartSelectionBehavior getKey ()
			{
				return this.m_selectionType;
			}
	
			public override void setKey (PartSelectionBehavior _key)
			{
				this.m_selectionType = _key;
			}
		
			public override int getData ()
			{
				return this.m_remainingInInventory;
			}
		
			public override void setData (int _data)
			{
				this.m_remainingInInventory = _data;
			}
		}
	
	#endregion
	}
}
