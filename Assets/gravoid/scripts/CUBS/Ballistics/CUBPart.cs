using UnityEngine;
using System.Collections;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class CUBPart : TheKeepStudios.spawning.Spawnable{

		[SerializeField]
		float
			lengthInAxisY = 0.0F;
		
		[SerializeField]
		FixedJoint
			joint;

		Projectile parent;
		
		CUBPart next;

		public FixedJoint ConnectionJoint{
			get{
				if(!joint){
					joint = gameObject.AddComponent<FixedJoint>();
				}
				return joint;
			}
			set{
				joint = value;
			}
		}

		public CUBPart Next{
			get{
				return next;
			}
			set{
				next = value;
			}
		}

		public CUBPart Previous{
			get{
				return ConnectionJoint.connectedBody ? ConnectionJoint.connectedBody.GetComponent<CUBPart>() : null;
			}
			set{
				ConnectionJoint.connectedBody = value ? value.rigidbody : null;
			}
		}
		
		public Projectile ContainingProjectile{
			get{ return parent;}
			set{ parent = value;}
		}
	
		virtual public float offset{
			get{
				return this.lengthInAxisY / 2;
			}
		}
		
		virtual public void SnapToPosition(Vector3 position, Quaternion rotation){
			if(Previous != null){
				/*
				 * see http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
				 * or  http://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
				 * for an explaination of what multiplying a Quaternion by a Vector3 does
				 */
				Vector3 prevPosition = rigidbody.position - (rotation * (Vector3.up * (Previous.offset + offset)));
				Previous.SnapToPosition(prevPosition, rotation);
			} else{
				Debug.Log("Snapping to position: " + position + " and rotation: " + rotation);
				rigidbody.position = position;
				rigidbody.rotation = rotation;
				if(Next != null){
					SnapToPosition();
				}
			}
		}

		virtual public void SnapToPosition(){
			if(Previous != null){
				/*
				 * see http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
				 * or  http://docs.unity3d.com/ScriptReference/Quaternion-operator_multiply.html
				 * for an explaination of what multiplying a Quaternion by a Vector3 does
				 */
				Vector3 newLocalPos = Vector3.up * (Previous.offset + offset);
				Rigidbody myRb = rigidbody;
				Rigidbody prevRb = Previous.rigidbody;
				myRb.position = (prevRb.rotation * newLocalPos) + prevRb.position;
				myRb.rotation = prevRb.rotation;
				Debug.Log("Snapping to position: " + myRb.position + " and rotation: " + myRb.rotation);
			} else{
				Debug.LogWarning("SnapToPosition called with no arguments and no previous CUBPart");
			}
			if(Next != null){
				Next.SnapToPosition();
			}
		}

		abstract public void Activate(GameObject activator);
		
		virtual public void OnLaunch(GameObject activator){
		}
		
		virtual public void OnCollisionEnter(Collision collision){
			
		}
		
		public void Despawn(){
			
			//stitch over with Previous in data structure
			if(Previous){
				Previous.Next = Next;
			} else if(ContainingProjectile != null){
				ContainingProjectile.Head = Next;
			}
			
			//stitch over with Next in data structure
			if(Next){
				Next.Previous = Previous;
			} else if(ContainingProjectile != null){
				ContainingProjectile.Head = Previous;
			}
			
			//destroy ourselves
			Spawned spawnedComponent = this.GetComponent<Spawned>();
			if(spawnedComponent){
				spawnedComponent.Despawn();
			} else{
				Destroy(this);
			}
		}
	}
}
