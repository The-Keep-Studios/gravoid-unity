using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.Gravoid.CUBS.Ballistics;
using TheKeepStudios.Gravoid.CUBS.Inventory;

namespace TheKeepStudios.Gravoid.CUBS{
/**
 * Component Utility Ballistic System. ( AKA C.U.B.S. )
 * Logic system for launched projectiles. 
 * */
	public class CUBSBehavior : MonoBehaviour{
	
	#region public properties
	
		public bool m_disabled;
		public ProjectileBehavior m_projectilePrefab;
		public List<CUBSBehavior.SubSystemLink> m_SubSystemLinks = new List<CUBSBehavior.SubSystemLink>();
	
	#endregion
	
	
	#region private properties
	
	#endregion
	
	
	#region public methods
	
		// Use this for initialization
		void Start(){
		
			foreach(SubSystemLink nextMap in this.m_SubSystemLinks){
			
				nextMap.Start(this.gameObject, this.m_projectilePrefab);
			
			}
		
		}


		// Update is called once per frame
		void Update(){
		
			if(!this.m_disabled){
			
				foreach(SubSystemLink nextMap in this.m_SubSystemLinks){
				
					nextMap.Update();
				
				}
			
			}
		
		}
	
	#endregion

	
	#region private methods
	
	#endregion

	#region INTERNAL CLASSES
	
		[System.Serializable]
		public class SubSystemLink{
			public string launchKeyName = "";
			public LauncherBehavior m_launcher;
			public ConfigurationSelectorBehavior m_componentSelector;
			public InventoryBehavior m_inventory;
			private ProjectileBehavior m_projectilePrefab;
			private GameObject m_parent;

			public LauncherBehavior launcher{
				get { return this.m_launcher; }
			}

			public ConfigurationSelectorBehavior componentSelector{
				get { return this.m_componentSelector; }
			}

			public InventoryBehavior inventory{
				get { return this.m_inventory; }
			}
		
			public void Start(GameObject _parent, ProjectileBehavior _projectilePrefab){
			
				if(this.m_launcher == null){
				
					this.m_launcher = _parent.GetComponent<LauncherBehavior>();
				
				}
			
				if(this.m_componentSelector == null){
				
					this.m_componentSelector = _parent.GetComponent<ConfigurationSelectorBehavior>();
				
				}
			
				if(this.m_inventory == null){
				
					this.m_inventory = _parent.GetComponent<InventoryBehavior>();
				
				}
			
				this.m_parent = _parent;
			
				this.m_projectilePrefab = _projectilePrefab;
			
			}
		
		
			// Update is called once per frame
			public void Update(){
			
				if(this.ShouldLaunch()){
					
					this.Launch();
				
				}
			
			}
	
			private bool ShouldLaunch(){
		
				return Input.GetButtonDown(this.launchKeyName)
					&& this.launcher != null 
					&& this.launcher.ReadyToLaunch();
		
			}
		
			private void Launch(){

				Ballistics.IProjectileConfiguration selection = this.componentSelector.GetCurrentSelection();
			
				ProjectileBehavior launchable = this.inventory.GetProjectile(selection, this.m_projectilePrefab);

				this.m_launcher.Launch(launchable);
			
			}
		
		}
	
	#endregion
	
	}
}