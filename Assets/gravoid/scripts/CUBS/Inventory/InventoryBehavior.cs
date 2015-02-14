using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS.Inventory{

	public class InventoryBehavior: MonoBehaviour{
		
		[SerializeField]
		bool infiniteResources;
	
		[SerializeField]
		private Map
			m_counts;

		public bool Insert(PartSelectionBehavior _selection){
			int count = this.GetCount(_selection);
			this.SetCount(_selection, count + 1);
			return true;
		}

		public PartSelectionBehavior Take(PartSelectionBehavior _selectionPrefab){
			PartSelectionBehavior taken = null;
			if(this.GetCount(_selectionPrefab) > 0 || infiniteResources){
				taken = _selectionPrefab;
				if(taken != null && !infiniteResources){
					//successfully retrieved selection, decriment the count
					int newCount = this.GetCount(_selectionPrefab) - 1;
					this.SetCount(_selectionPrefab, newCount);
				}
			}
			return taken;
		}
	
		public ProjectileBehavior GetProjectile(Ballistics.IProjectileConfiguration configuration, ProjectileBehavior projectilePrefab){
			ProjectileBehavior projectile = null;
			Debug.Log("Creating a projectile from inventory resources for " + configuration);
			//TRY and get the selected selections from the inventory, and instantiate a projectile
			IProjectileConfiguration selections = this.GetSelections(configuration);
			projectile = ProjectileBehavior.Spawn(selections, projectilePrefab);
			if(projectile == null || !projectile.CanBeLaunched()){
				foreach(PartSelectionBehavior nextSelection in selections.Parts){
					//place each of the selections back in the inventory, as we cannot use them after all
					this.Insert(nextSelection);
				}
			} else{
				projectile.ignoreCollisionsWith(this.collider);
			}
			return projectile;
		}
	
		public void Start(){
			if(this.m_counts == null){
				this.m_counts = new Map();
			}
			this.m_counts.valueWhenKeyIsMissing = 0;
		}

		public void Update(){
		}
	
		private void SetCount(PartSelectionBehavior _selectionPrefab, int newVal){
			this.m_counts.setData(_selectionPrefab, newVal);
		}

		private int GetCount(PartSelectionBehavior _selectionPrefab){
			return this.m_counts.getData(_selectionPrefab);
		}
	
		private List<PartSelectionBehavior> GetInsufficientselections(List<PartSelectionBehavior> _selectionPrefabList){
			List<PartSelectionBehavior> missing = new List<PartSelectionBehavior>(_selectionPrefabList.Count);
			Dictionary<PartSelectionBehavior,int> counts = new Dictionary<PartSelectionBehavior, int>(_selectionPrefabList.Count);
			if(_selectionPrefabList != null){
				foreach(PartSelectionBehavior next in _selectionPrefabList){
					if(!counts.ContainsKey(next)){
						counts.Add(next, 0);
					}
					int available = GetCount(next);
					/*
					 * this check is a bit screwy looking, because it must activate 
					 * ONLY on the first found insufficient selection.
					 */
					if(counts[next] == available){
						//if the last count is equal to the available, then we are lacking the selection
						missing.Add(next.GetComponent<PartSelectionBehavior>());
					}
					counts[next] += 1;
				}
			}
			return missing;
		}
	
		private IProjectileConfiguration GetSelections(Ballistics.IProjectileConfiguration configuration){
			List<PartSelectionBehavior> lackingComponents = this.GetInsufficientselections(configuration.Parts);
			if(lackingComponents.Count == 0 || infiniteResources){
				IProjectileConfiguration retrievedConfiguration = new ProjectileConfiguration();
				foreach(PartSelectionBehavior nextselectionType in configuration.Parts){
					retrievedConfiguration.Add(this.Take(nextselectionType));
				}
				return retrievedConfiguration;
			} else{
				this.DisplayInsufficientselections(lackingComponents);
				return null;
			}
		}
		
		private void DisplayInsufficientselections(List<PartSelectionBehavior> _selectionTypes){
			string output = "Missing: %s" + _selectionTypes.ToArray().ToString();
			Debug.Log(output);
			//TODO Send notification to the user
		}
	
	#region INTERNAL CLASSES
		[System.Serializable]
		internal class Map: DataMap<PartSelectionBehavior,int,Link>{
		}
	
		[System.Serializable]
		internal class Link: Link<PartSelectionBehavior,int>{
		
			public PartSelectionBehavior m_selectionType;

			public int m_remainingInInventory;

			public override PartSelectionBehavior getKey(){
				return this.m_selectionType;
			}
	
			public override void setKey(PartSelectionBehavior _key){
				this.m_selectionType = _key;
			}
		
			public override int getData(){
				return this.m_remainingInInventory;
			}
		
			public override void setData(int _data){
				this.m_remainingInInventory = _data;
			}
		}
	
	#endregion
	}
}
