using UnityEngine;
using System.Collections;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics { 
	[RequireComponent(typeof(Rigidbody))]
	public abstract class CUBPart : MonoBehaviour {

		[SerializeField]
		float
			lengthInAxisY = 0.0F;

		[SerializeField]
		FixedJoint
			joint;

		Projectile parent;

		CUBPart next;
		CUBPart previous;

		public FixedJoint ConnectionJoint {
			get {
				if (!joint) {
					joint = gameObject.AddComponent<FixedJoint>();
				}
				return joint;
			}
			set {
				if (value == null) {
					Destroy(joint);
				}
				joint = value;
			}
		}

		public CUBPart Next {
			get {
				return next;
			}
			set {
				next = value;
			}
		}

		public CUBPart Previous {
			get {
				return previous;
			}
			set {
				previous = value;
				if (previous != null && previous.GetComponent<Rigidbody>() != null) {
					ConnectionJoint.connectedBody = previous.GetComponent<Rigidbody>();
				}
				else { ConnectionJoint = null; } 
			}
		}

		public Projectile ContainingProjectile {
			get { return parent; }
			set { parent = value; }
		}

		virtual public float offset {
			get {
				return this.lengthInAxisY / 2;
			}
		}

		virtual public void SnapToPosition(Vector3 position, Quaternion rotation) {
			if (Previous != null) {
				/*
				 * see http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
				 * or  http://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
				 * for an explaination of what multiplying a Quaternion by a Vector3 does
				 */
				Vector3 newPosition = position + (rotation * (Vector3.up * (Previous.offset + offset)));
				Previous.SnapToPosition(newPosition, rotation);
			}
			else {
				Debug.Log("Snapping " + this + " to position: " + position + " and rotation: " + rotation);
				DoSnap(position, rotation);
				if (Next != null) {
					Next.SnapToPosition();
				}
			}
		}

		virtual public void SnapToPosition() {
			if (Previous != null) {
				/*
				 * see http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
				 * or  http://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
				 * for an explaination of what multiplying a Quaternion by a Vector3 does
       			 */
				Debug.Log("Snapping to local position relative to Previous (headward) CUBPart");
				Rigidbody myRb = GetComponent<Rigidbody>();
				Rigidbody prevRb = Previous.GetComponent<Rigidbody>();
				DoSnap(
					prevRb.position - (prevRb.rotation * (Vector3.up * (Previous.offset + offset)))
					, prevRb.rotation);
			}
			else {
				Debug.LogWarning("SnapToPosition called with no arguments and no previous CUBPart");
			}
			if (Next != null) {
				Next.SnapToPosition();
			}
		}

		private void DoSnap(Vector3 pos, Quaternion rot) {
			//temporarily clear Previous so that the "snap" doesn't also move it
			CUBPart originalPrevious = Previous;
			Previous = null;
			transform.position = pos;
			transform.rotation = rot;
			//reset Previous to its original value
			Previous = originalPrevious;
			Debug.Log("Snapped " + this + " to position: " + pos + " and rotation: " + rot);
		}

		abstract public void Activate(GameObject activator);

		virtual public void OnLaunch(GameObject activator) {
		}

		virtual public void OnCollisionEnter(Collision collision) {

		}

		public void Despawn() {

			//stitch over with Previous in data structure
			if (Previous) {
				Previous.Next = Next;
			}
			else if (ContainingProjectile != null) {
				ContainingProjectile.Head = Next;
			}

			//stitch over with Next in data structure
			if (Next) {
				Next.Previous = Previous;
			}
			else if (ContainingProjectile != null) {
				ContainingProjectile.Tail = Previous;
			}

			//destroy ourselves
			Spawned spawnedComponent = this.GetComponent<Spawned>();
			if (spawnedComponent) {
				spawnedComponent.Despawn();
			}
			else {
				gameObject.Recycle();
			}
		}
	}
}
