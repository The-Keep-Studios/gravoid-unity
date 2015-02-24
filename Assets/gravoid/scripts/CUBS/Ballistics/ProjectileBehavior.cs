using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
	public class ProjectileBehavior : Spawnable, ILaunchable{

		public ILaunchable m_state;

		//FIXME needs to be an instance or we need to accept it from somewhere
		public IProjectileConfiguration m_partSelections = null;

		public List<CUBPart> m_parts = new List<CUBPart>();

		public ILaunchable State{
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
	
		public bool CanLaunch(){
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

		/// <summary>
		/// Split the specified splitLocation.
		/// </summary>
		/// <param name="splitLocation">The resultant projectiles</param>
		public List<ProjectileBehavior> Split(CUBPart splitLocation){
			Debug.Log("Splitting  " + this.name + " at " + splitLocation.name);
			if(splitLocation == null){
				throw new NullReferenceException("Split location must not be null");
			} else if(!m_parts.Contains(splitLocation)){
				throw new Exception("Split location CUBPart not a member of this projectile ");
			}
				
			//get the integer location of the split
			int splitIdx = m_parts.IndexOf(splitLocation);
			int numHeadParts = splitIdx;
			int numMidParts = 1;
			int numTailParts = m_parts.Count - splitIdx - 1;
			
			//ready a list of the resultant projectiles
			List<ProjectileBehavior> projectiles = new List<ProjectileBehavior>(3);
			projectiles.Add(this);
				
			if((numHeadParts + numTailParts) == 0){
				//the split is unnecesary as it's the ONLY child
				return projectiles;
			}
			
			//deterime our parts lists
			List<CUBPart> headParts = m_parts.GetRange(0, numHeadParts);
			List<CUBPart> midParts = m_parts.GetRange(splitIdx, numMidParts);
			List<CUBPart> tailParts = m_parts.GetRange(splitIdx + 1, numTailParts);			
			tailParts.Reverse();
			
			//setup ourselves as the "middle" part
			this.m_parts = midParts;
			SetupCollider(PositionParts() / 2);
			
			//make new head and add it if it is not null
			ProjectileBehavior head = MakeNewProjectileFromParts(headParts);
			if(head){
				projectiles.Add(head);
			}
			
			//make new tail and add it if it is not null
			ProjectileBehavior tail = MakeNewProjectileFromParts(tailParts);
			if(tail){
				projectiles.Add(tail);
			}
			return projectiles;
		}

		ProjectileBehavior MakeNewProjectileFromParts(List<CUBPart> parts){
			if(parts.Count <= 0){
				Debug.Log("No new ProjectileBehavior gameobject was made from the empty parts list");
				return null;
			}
			ProjectileBehavior projectile = this.Spawn(parts[0].transform) as ProjectileBehavior;
			projectile.m_parts = parts;
			//position the parts and setup the collider
			projectile.SetupCollider(projectile.PositionParts() / 2);
			Debug.Log("Made new projectile  " + projectile.name);
			return projectile;
		}
		
		void OnCollisionEnter(Collision collision){
			if(m_parts.Count > 0){
				Debug.Log("Collidsion detected, sending signal to  " + m_parts[0].name);
				m_parts[0].OnCollisionEnter(collision);
			}
		}
		
		void InitializeParts(){
			foreach(CUBPart nextPart in m_parts){
				//despawn any existing parts
				nextPart.GetComponent<TheKeepStudios.spawning.Spawned>().Despawn();
			}
			
			// setup the list to contain the new parts
			m_parts.Clear();
			if(m_partSelections != null && m_partSelections.Count != 0){
				m_parts.Capacity = m_partSelections.Count;
				// create the parts
				foreach(PartSelectionBehavior nextSelection in m_partSelections.Parts){
					if(nextSelection == null){
						continue; //skip null parts
					}
					CUBPart prefab = nextSelection.ProjectilePartPrefab;
					CUBPart nextPart = prefab.Spawn(transform).GetComponent<CUBPart>();
					m_parts.Add(nextPart);
				}
			}
			SetupCollider(PositionParts() / 2);
		}
		
		/// <summary>
		/// Positions the parts.
		/// </summary>
		/// <returns>The center point of the parts along the y-axis, whos absolute value doubled is the height of the parts.</returns>
		float PositionParts(){
			float nextPositionStart = 0.0f;
			if(m_partSelections != null && m_partSelections.Count != 0){
				m_parts.Capacity = m_partSelections.Count;
				// create and stack the parts along Vector3.up
				foreach(CUBPart nextPart in m_parts){
					if(nextPart == null){
						continue; //skip null parts
					}
					nextPositionStart -= nextPart.offset / 2;
					//place the part at the center of it's offset
					Vector3 nextPosition = Vector3.up * nextPositionStart;
					nextPart.JoinToLaunchedObject(transform, nextPosition);
					//finish adjusting the offset
					nextPositionStart -= nextPart.offset / 2;
					//link the parts together
					nextPart.Next = null;
					nextPart.Previous = m_parts.Count == 0 ? null : m_parts[m_parts.Count - 1];
				}
			}
			return nextPositionStart;
		}
		
		/// <summary>
		/// Setups the collider.
		/// </summary>
		/// <param name="newCenter">New center along the y axis.</param>
		void SetupCollider(float newCenter){
			//manipulate our collider to conform to the parts
			CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
			Vector3 newColliderCenter = collider.center;
			newColliderCenter.y = newCenter;
			collider.center = newColliderCenter;
			collider.height = Mathf.Abs(newCenter) * 2;
		}
	
	#region ILaunchableStates
		
		public interface ILaunchableState : ILaunchable{
			
			ProjectileBehavior Parent{ get; set; }
			
		}
		
		[System.Serializable]
		public abstract class BaseLaunchState : ILaunchable{
			
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
		public class UnlaunchedState : BaseLaunchState, ILaunchable{
			
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
				//to prevent race conditions we switch to the LaunchingState
				Parent.State = new LaunchingState(Parent);
				Debug.Log("Launching " + Parent + " from Transform " + _tm);
				Parent.transform.position = _tm.position;
				Parent.transform.rotation = _tm.rotation;
				//spawn all our selections to new part instances
				Parent.InitializeParts();
				if(Parent.m_parts.Count > 0){
					//get the tail most object
					Parent.m_parts[Parent.m_parts.Count - 1].OnLaunch(_tm.gameObject);
					Parent.State = new LaunchedState(Parent);
					return true;
				} else{
					Parent.State = new LaunchedState(Parent);
					return false;
				}
			}
			
			override public bool CanLaunch(){
				return true;
			}
		}
		
		[System.Serializable]
		public class LaunchingState : BaseLaunchState, ILaunchable{
			
			public LaunchingState(ProjectileBehavior parent):base(parent){
			}
			
			override public bool Launch(float _force, Transform _tm){
				//HACK works to indicated exceptional behavior but we should handle this more elegantly
				throw new Exception("Race condition detected, ILaunchable.Launch was called while already launching");
			}
		}
		
		[System.Serializable]
		public class LaunchedState : BaseLaunchState, ILaunchable{
			public LaunchedState(ProjectileBehavior parent):base(parent){
			}
		}
		
	#endregion
	}
}