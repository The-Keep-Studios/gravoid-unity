using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CapsuleCollider))]
public class ProjectileBehavior : MonoBehaviour {
	
	#region PUBLIC PROPERTIES
	public ILaunchableState m_state = new CBaseState ();
	public List<ProjectilePartBehavior> m_partPrefabs = new List<ProjectilePartBehavior> ();
	public List<ProjectilePartBehavior> m_parts = new List<ProjectilePartBehavior> ();
	#endregion
	#region PRIVATE PROPERTIES
	private const string m_partPoolName = "Part";
	private const string m_poolName = "Projectile";
	#endregion	
	#region PUBLIC METHODS

	
	public void Start () {
		
		if (this.rigidbody == null) {
			
			this.gameObject.AddComponent<Rigidbody> ();
			
		}
		
	}


	public void Update () {
		
		this.m_state.Update (this);
		
	}


	public void FixedUpdate () {
		
		this.m_state.FixedUpdate (this);
		
	}
	
	
	public void SetParts (List<ProjectilePartBehavior> _partPrefabs) {
		
		this.m_partPrefabs = _partPrefabs;
		
	}


	public static ProjectileBehavior Spawn (List<ProjectilePartBehavior> _partPrefabs, ProjectileBehavior _prefab) {
		
		ProjectileBehavior newInstance = null;
		
		_partPrefabs.RemoveAll (part => part == null);
		
		//empty launchables are invalid, do not instantiate prefab if empty
		if (_partPrefabs.Count != 0) {
			
			Transform newParentObject = PathologicalGames.PoolManager.Pools [m_poolName].Spawn (_prefab.transform);
			
			//try to get the new launchable and set it's components
			if (newParentObject != null) {
				
				newInstance = newParentObject.GetComponent<ProjectileBehavior> ();
		
				if (newInstance != null) {
				
					newInstance.SetParts (_partPrefabs);
				
				}
				
			}
			
		}
		
		return newInstance;
		
	}


	public void Despawn () {
			
		PathologicalGames.PoolManager.Pools [m_poolName].Despawn (this.transform);
	}

	
	public bool CanBeLaunched () {
		
		return this.m_state.CanLaunch (this);
		
	}


	public bool SetPosition (Vector2 _pos) {
		
		this.transform.position = new Vector3 (_pos.x, _pos.y);
		
		return true;
		
	}


	public bool Launch (float _force, Transform _tm) {
		
		return this.m_state.Launch (this, _force, _tm);
		
	}
	
	
	public void ignoreCollisionsWith (Collider _collider) {
		if (_collider != null && this.collider != null) {
			
			Physics.IgnoreCollision (this.collider, _collider);
			
		}
		
	}
	#endregion
	#region PRIVATE METHODS
	#endregion

	
	#region INTERNAL CLASSES, STRUCTS AND INTERFACES

	public interface ILaunchableState {

		void Start (ProjectileBehavior _parent);


		void Update (ProjectileBehavior _parent);


		void FixedUpdate (ProjectileBehavior _parent);


		bool Launch (ProjectileBehavior _parent, float _force, Transform _tm);


		bool CanLaunch (ProjectileBehavior _parent);
		
	}

	[System.Serializable]
	internal struct CBaseState : ILaunchableState {

	
		#region PUBLIC PROPERTIES
		#endregion
		
		#region PRIVATE PROPERTIES
		#endregion	
		
		#region PUBLIC METHODS
		public void Start (ProjectileBehavior _parent) {
			
			
		}


		public void Update (ProjectileBehavior _parent) {
			
			
		}


		public void FixedUpdate (ProjectileBehavior _parent) {
			
			
		}


		public bool Launch (ProjectileBehavior _parent, float _force, Transform _tm) {
			_parent.m_state = new CLaunchingState ();
			
			_parent.m_state.Launch (_parent, _force, _tm);
			
			return true;
			
		}


		public bool CanLaunch (ProjectileBehavior _parent) {
			
			return true;
			
		}
		#endregion
		
		#region PRIVATE METHODS
		#endregion
		
		#region INTERNAL CLASSES, STRUCTS AND INTERFACES
		#endregion
		
	}

	[System.Serializable]
	internal struct CLaunchedState : ILaunchableState {

	
		#region PUBLIC PROPERTIES
		#endregion
		
		#region PRIVATE PROPERTIES
		#endregion	
		
		#region PUBLIC METHODS
		public void Start (ProjectileBehavior _parent) {
			
			
		}


		public void Update (ProjectileBehavior _parent) {
			
			
		}


		public void FixedUpdate (ProjectileBehavior _parent) {
			
			
		}


		public bool Launch (ProjectileBehavior _parent, float _force, Transform _tm) {
			
			return false;
			
		}


		public bool CanLaunch (ProjectileBehavior _parent) {
			
			return false;
			
		}
		#endregion
		
		#region PRIVATE METHODS
		#endregion
		
		#region INTERNAL CLASSES, STRUCTS AND INTERFACES
		#endregion
		
	}

	[System.Serializable]
	internal struct CLaunchingState : ILaunchableState {
		
		#region PUBLIC PROPERTIES
		#endregion
		
		#region PRIVATE PROPERTIES
		#endregion	
		
		#region PUBLIC METHODS
		public void Start (ProjectileBehavior _parent) {
			
			
		}


		public void Update (ProjectileBehavior _parent) {
			
			
		}


		public void FixedUpdate (ProjectileBehavior _parent) {
			
			
		}


		public bool Launch (ProjectileBehavior _parent, float _force, Transform _tm) {
			
			Transform transform = _parent.transform;
			
			Rigidbody rigidbody = _parent.rigidbody;
			
			Vector3 position = _tm.position;
			
			Vector3 force = _force * _tm.up;
			
			float nextPositionStart = 0.0f;
			
			_parent.m_parts.Clear ();
			
			_parent.m_parts.Capacity = _parent.m_partPrefabs.Count;
			
			/**
			 * Now we tell each of the ILaunchableComponents that we are ready for them to 
			 * join the parent, stacking them in a row along x
			 * */
			foreach (ProjectilePartBehavior nextPrefab in _parent.m_partPrefabs) {
				
				ProjectilePartBehavior nextPart = instantiatePart (nextPrefab);
				
				nextPositionStart -= nextPart.offset/2; //place the part at the center of it's offset
				
				nextPart.JoinToLaunchedObject (transform, Vector3.up * nextPositionStart);
				
				nextPositionStart -= nextPart.offset/2; //finish adjusting the offset
				
				_parent.m_parts.Add (nextPart);
				
			}
			
			CapsuleCollider collider = _parent.gameObject.GetComponent<CapsuleCollider> ();
			
			Vector3 center = collider.center;
			
			center.y = nextPositionStart / 2; //position the center midway along the y axis
			
			collider.center = center;
			
			collider.height = -nextPositionStart;
			
			transform.position = position;
			
			transform.rotation = _tm.rotation;
			
			rigidbody.AddForce (force, ForceMode.VelocityChange);
			
			_parent.m_state = new CLaunchedState ();
			
			return true;
			
		}


		public bool CanLaunch (ProjectileBehavior _parent) {
			
			return false;
			
		}
	
		
		
		#endregion
		
		#region PRIVATE METHODS
		
		private static ProjectilePartBehavior instantiatePart (ProjectilePartBehavior partPrefab) {
			
			ProjectilePartBehavior part = null;
			
			if (partPrefab != null) {
			
				PathologicalGames.SpawnPool pool = PathologicalGames.PoolManager.Pools [m_partPoolName];
			
				Transform obj = pool.Spawn (partPrefab.transform);
				
				if (obj != null) {
				
					part = obj.GetComponent<ProjectilePartBehavior> ();
				
					if (part == null) {
					
						pool.Despawn (obj);
					
						return null;
					
					}
				
				}
				
			}
			
			return part;
		
		}
		#endregion
		
		#region INTERNAL CLASSES, STRUCTS AND INTERFACES
		#endregion
		
	}
	
	#endregion
}
