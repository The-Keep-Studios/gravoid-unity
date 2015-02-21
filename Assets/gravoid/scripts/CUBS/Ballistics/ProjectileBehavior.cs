using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
	public class ProjectileBehavior : TheKeepStudios.spawning.Spawnable{

		public ILaunchableState m_state;

		//FIXME needs to be an instance or we need to accept it from somewhere
		public IProjectileConfiguration m_partSelections = null;

		public List<CUBPartBase> m_parts = new List<CUBPartBase>();

		public ILaunchableState State{
			get{
				if(m_state == null){
					State = new UnlaunchedState(this);
				}
				return m_state;
			}
			set{
				m_state = value;
			}
		}
	
		public void Start(){
			this.State.Start();
		}

		public void Update(){
			this.State.Update();
		}

		public void FixedUpdate(){
			this.State.FixedUpdate();
		}
	
		void SetPartSelections(IProjectileConfiguration config){
			this.m_partSelections = config;
		}

		public static ProjectileBehavior Spawn(IProjectileConfiguration config, ProjectileBehavior _prefab){	
			ProjectileBehavior projectile = null;
			//empty launchables are invalid, do not instantiate prefab if empty
			TheKeepStudios.spawning.Spawnable projectileTransform = config.Count != 0 ? _prefab.GetComponent<TheKeepStudios.spawning.Spawnable>().Spawn(_prefab.transform) : null;			
			//try to get the new launchable and set it's components
			projectile = projectileTransform != null ? projectileTransform.GetComponent<ProjectileBehavior>() : null;
			//only spawn the parts for the projectile if has been validly spawned
			if(projectile != null){
				//set the parts in the projectile
				projectile.SetPartSelections(config);
			}		
			return projectile;		
		}
	
		public bool CanBeLaunched(){
			return this.State.CanLaunch();
		}

		public bool SetPosition(Vector2 _pos){
			this.transform.position = new Vector3(_pos.x, _pos.y);
			return true;
		}

		public bool Launch(float _force, Transform _tm){
			return this.State.Launch(_force, _tm);
		}
	
		public void ignoreCollisionsWith(Collider _collider){
			if(_collider != null && this.collider != null){
				Physics.IgnoreCollision(this.collider, _collider);
			}
		}
	
	#region ILaunchableStates

		public interface ILaunchableState{
			
			ProjectileBehavior Parent{ get; set; }

			void Start();

			void Update();

			void FixedUpdate();

			bool Launch(float _force, Transform _tm);

			bool CanLaunch();
		
		}
		
		[System.Serializable]
		public abstract class BaseLaunchState : ILaunchableState{
		
			private ProjectileBehavior parent;			
			
			public ProjectileBehavior Parent{ get { return parent; } set { parent = value; } }
			
			public BaseLaunchState(ProjectileBehavior parent){
				Parent = parent;
			}
			
			virtual public void Start(){
			}
			
			virtual public void Update(){
			
			}
			virtual public void FixedUpdate(){
			}
			
			virtual public bool Launch(float _force, Transform _tm){
				return false;
			}
			
			virtual public bool CanLaunch(){
				return false;
			}
			
		}


		[System.Serializable]
		public class UnlaunchedState : BaseLaunchState, ILaunchableState{
			
			public UnlaunchedState(ProjectileBehavior parent):base(parent){
			}
			
			override public void Start(){
				if(Parent.rigidbody == null){
					Parent.gameObject.AddComponent<Rigidbody>();
				}
				if(Parent.GetComponent<CapsuleCollider>() == null){
					Parent.gameObject.AddComponent<CapsuleCollider>();
				}
			}

			override public bool Launch(float _force, Transform _tm){
				Parent.State = new LaunchingState(Parent);
				Parent.State.Launch(_force, _tm);
				return true;
			}

			override public bool CanLaunch(){
				return true;
			}		
		}


		[System.Serializable]
		public class LaunchingState : BaseLaunchState, ILaunchableState{
			
			public LaunchingState(ProjectileBehavior parent):base(parent){
			}

			override public bool Launch(float _force, Transform _tm){
				Parent.transform.position = _tm.position;
				Parent.transform.rotation = _tm.rotation;
				//spawn all our selections to new part instances
				InitializeParts();
				if(Parent.m_parts.Count > 0){
					//get the tail most object
					Parent.m_parts[Parent.m_parts.Count - 1].Activate(_tm.gameObject);
					Parent.State = new LaunchedState(Parent);
					return true;
				} else{
					Parent.State = new LaunchedState(Parent);
					return false;
				}
			}
			
			void InitializeParts(){
				foreach(CUBPartBase nextPart in Parent.m_parts){
					//despawn any existing parts
					nextPart.GetComponent<TheKeepStudios.spawning.Spawned>().Despawn();
				}
				
				// setup the list to contain the new parts
				Parent.m_parts.Clear();
				float nextPositionStart = 0.0f;
				if(Parent.m_partSelections != null && Parent.m_partSelections.Count != 0){
					Parent.m_parts.Capacity = Parent.m_partSelections.Count;
					// create and stack the parts along Vector3.up
					foreach(PartSelectionBehavior nextSelection in Parent.m_partSelections.Parts){
						if(nextSelection == null){
							continue; //skip null parts
						}
						CUBPartBase prefab = nextSelection.ProjectilePartPrefab;
						CUBPartBase nextPart = prefab.Spawn(prefab.transform).GetComponent<CUBPartBase>();
						Parent.m_parts.Add(nextPart);
						nextPositionStart -= nextPart.offset / 2;
						//place the part at the center of it's offset
						nextPart.JoinToLaunchedObject(Parent.transform, Vector3.up * nextPositionStart);
						//finish adjusting the offset
						nextPositionStart -= nextPart.offset / 2;
					}
				}
				
				//manipulate our collider to conform to the parts
				CapsuleCollider collider = Parent.gameObject.GetComponent<CapsuleCollider>();
				Vector3 newColliderCenter = collider.center;
				newColliderCenter.y = nextPositionStart / 2;
				//position the center midway along the y axis
				collider.center = newColliderCenter;
				collider.height = -nextPositionStart;
			}
		}
		
		[System.Serializable]
		public class LaunchedState : BaseLaunchState, ILaunchableState{
			public LaunchedState(ProjectileBehavior parent):base(parent){
			}
		}
	
	#endregion
	}
}