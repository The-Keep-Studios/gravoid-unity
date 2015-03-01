using UnityEngine;
using System;
using System.Collections.Generic;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics {

	public class Projectile : ILaunchable, ICollection<CUBPart> {

		public ILaunchable m_state;

		//FIXME needs to be an instance or we need to accept it from somewhere
		public IProjectileConfiguration m_partSelections = null;

		CUBPart head = null;

		CUBPart tail = null;

		Quaternion defaultRotation;

		Vector3 defaultPosition;

		public CUBPart Head {
			get {
				return head;
			}
			set {
				head = value;
			}
		}

		public CUBPart Tail {
			get {
				if (tail == null) {
					//tail unset, so we should try and set it
					for (CUBPart nextPart = Head; nextPart != null; nextPart = nextPart.Next) {
						tail = nextPart;
					}
				}
				return tail;
			}
			set {
				tail = value;
			}
		}

		public ILaunchable State {
			get {
				if (m_state == null) {
					State = new UnlaunchedState(this);
				}
				return m_state;
			}
			set {
				m_state = value;
			}
		}

		public Vector3 Position {
			get {
				return Head != null ? Head.rigidbody.position : defaultPosition;
			}
			set {
				defaultPosition = value;
				if (value != Position) {
					foreach (CUBPart nextPart in this) {
						SnapToPosition(nextPart);
					}
				}
			}
		}

		public Quaternion Rotation {
			get {
				return Head != null ? Head.rigidbody.rotation : defaultRotation;
			}
			set {
				defaultRotation = value;
				if (value != Rotation) {
					foreach (CUBPart nextPart in this) {
						SnapToPosition(nextPart);
					}
				}
			}
		}

		public Projectile() {

		}

		public Projectile(IProjectileConfiguration config) {
			//set the parts in the projectile
			SetPartSelections(config);
		}

		Projectile(List<CUBPart> parts) {
			if (parts != null) {
				Debug.LogWarning("ProjectileBehavior made from the null parts list");
			}
			else if (parts.Count <= 0) {
				Debug.LogWarning("ProjectileBehavior made from empty parts list");
			}
			else {
				foreach (CUBPart nextPart in parts) {
					Add(nextPart);
				}
			}
		}

		void SnapToPosition(CUBPart partToPosition) {
			if (partToPosition == Head) {
				partToPosition.SnapToPosition(Position, Rotation);
			}
			else {
				partToPosition.SnapToPosition();
			}
		}

		void SetPartSelections(IProjectileConfiguration config) {
			this.m_partSelections = config;
		}

		public bool CanLaunch() {
			return this.State.CanLaunch();
		}

		public bool Launch(float _force, Transform _tm) {
			return this.State.Launch(_force, _tm);
		}

		/// <summary>
		/// Split the specified splitLocation.
		/// </summary>
		/// <param name="splitLocation">The resultant projectiles</param>
		public List<Projectile> Split(CUBPart splitLocation) {

			Debug.Log("Splitting  " + this + " at " + splitLocation.name);

			if (splitLocation == null) {
				throw new NullReferenceException("Split location must not be null");
			}
			else if (!Contains(splitLocation)) {
				throw new Exception("Split location CUBPart not a member of this projectile ");
			}

			//make new head and tail projectile
			Projectile headSection = new Projectile();
			Projectile tailSection = new Projectile();

			//setup the list of projectiles we will return
			List<Projectile> projectiles = new List<Projectile>();
			projectiles.Add(headSection);
			projectiles.Add(this);
			projectiles.Add(tailSection);

			//set initial positions
			headSection.Position = Position;
			headSection.Rotation = Rotation;

			if (splitLocation.Next != null) {
				headSection.Position = splitLocation.Next.rigidbody.position;
				headSection.Rotation = splitLocation.Next.rigidbody.rotation;
			}

			//put the parts into the proper places
			Boolean hasSeenSplitPoint = false;
			foreach (CUBPart nextPart in this) {
				if (hasSeenSplitPoint) {
					tailSection.Add(nextPart);
				}
				else if (nextPart != splitLocation) {
					//we're still creating the new head projectile
					headSection.Add(nextPart);
				}
				else {
					//we do not rearranging here, to leave the split location in the original projectile
					hasSeenSplitPoint = true;
				}
			}

			return projectiles;
		}

		[ContextMenu("InitializeParts")]
		void InitializeParts() {
			foreach (CUBPart nextPart in this) {
				//despawn any existing parts
				nextPart.GetComponent<TheKeepStudios.spawning.Spawned>().Despawn();
			}

			if (m_partSelections != null && m_partSelections.Count != 0) {
				// create the parts
				foreach (PartSelectionBehavior nextSelection in m_partSelections.Parts) {
					if (nextSelection == null) {
						continue; //skip null parts
					}
					CUBPart prefab = nextSelection.ProjectilePartPrefab;
					CUBPart nextPart = prefab.Spawn().GetComponent<CUBPart>();
					Add(nextPart);
				}
			}
		}

		public bool Contains(CUBPart containedPart) {
			for (CUBPart nextPart = Head; nextPart != null; nextPart = nextPart.Next) {
				if (nextPart == containedPart) {
					return true;
				}
			}
			return false;
		}

		#region ICollection

		public void Add(CUBPart item) {
			//we only allow the addition of non-null parts
			if (item != null) {
				//items may only be a member of one projectile at a time, so stitch up any gaps we are leaving behind
				if (item.ContainingProjectile != null) {
					item.ContainingProjectile.Remove(item);
				}
				item.Next = null;
				item.Previous = null;
				item.ContainingProjectile = this;
				if (Head == null) {
					Head = item;
					Tail = item;
					item.SnapToPosition(defaultPosition, defaultRotation);
				}
				else {
					/*
					 * see http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
					 * or  http://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
					 * for an explaination of what multiplying a Quaternion by a Vector3 does
					 */
					item.Previous = Tail;
					Tail.Next = item;
					Tail = item;
					item.SnapToPosition();
				}
			}
		}

		public void Clear() {
			foreach (CUBPart nextPart in this) {
				nextPart.ContainingProjectile = null;
				Head = null;
				Tail = null;
			}
		}

		public void CopyTo(CUBPart[] array, int arrayIndex) {
			//HACK sloppy but quick to implement
			List<CUBPart> parts = new List<CUBPart>(this);
			parts.CopyTo(array, arrayIndex);
		}

		public bool Remove(CUBPart item) {
			if (item == null || item.ContainingProjectile != this) {
				return false;
			}

			//stitch next over
			if (item.Previous != null) {
				item.Previous.Next = item.Next;
			}

			//stitch previous over
			if (item.Next != null) {
				item.Next.Previous = item.Previous;
			}

			//move head if this was the head
			if (item == Head) {
				Head = item.Next;
			}

			//move tail if this was the tail
			if (item == Tail) {
				Tail = item.Previous;
			}

			item.Next = null;
			item.Previous = null;
			item.ContainingProjectile = null;

			return true;
		}

		public int Count {
			get {
				int count = 0;
				foreach (CUBPart nextPart in this) {
					if (nextPart != null) {
						++count;
					}
				}
				return count;
			}
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public IEnumerator<CUBPart> GetEnumerator() {
			if (Head == null) {
				yield break;
			}
			for (CUBPart nextPart = Head; nextPart != null; nextPart = nextPart.Next) {
				yield return nextPart;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		#endregion

		#region ILaunchableStates

		public interface ILaunchableState : ILaunchable {

			Projectile Parent { get; set; }

		}

		[System.Serializable]
		public abstract class BaseLaunchState : ILaunchable {

			private Projectile parent;

			public Projectile Parent { get { return parent; } set { parent = value; } }

			public BaseLaunchState(Projectile parent) {
				Parent = parent;
			}

			virtual public void Start() {
			}

			virtual public void Update() {

			}

			virtual public void FixedUpdate() {
			}

			virtual public bool Launch(float _force, Transform _tm) {
				return false;
			}

			virtual public bool CanLaunch() {
				return false;
			}

		}

		[System.Serializable]
		public class UnlaunchedState : BaseLaunchState, ILaunchable {

			public UnlaunchedState(Projectile parent)
				: base(parent) {
			}

			override public bool Launch(float _force, Transform _tm) {
				//to prevent race conditions we switch to the LaunchingState
				Parent.State = new LaunchingState(Parent);
				Debug.Log("Launching " + Parent + " from Transform " + _tm);
				Parent.Position = _tm.position;
				Parent.Rotation = _tm.rotation;
				//spawn all our selections to new part instances
				Parent.InitializeParts();
				if (Parent.Tail) {
					//get the tail most object
					Parent.Tail.OnLaunch(_tm.gameObject);
					Parent.State = new LaunchedState(Parent);
					return true;
				}
				else {
					Parent.State = new LaunchedState(Parent);
					return false;
				}
			}

			override public bool CanLaunch() {
				return true;
			}
		}

		[System.Serializable]
		public class LaunchingState : BaseLaunchState, ILaunchable {

			public LaunchingState(Projectile parent)
				: base(parent) {
			}

			override public bool Launch(float _force, Transform _tm) {
				//HACK works to indicated exceptional behavior but we should handle this more elegantly
				throw new Exception("Race condition detected, ILaunchable.Launch was called while already launching");
			}
		}

		[System.Serializable]
		public class LaunchedState : BaseLaunchState, ILaunchable {
			public LaunchedState(Projectile parent)
				: base(parent) {
			}
		}

		#endregion
	}
}