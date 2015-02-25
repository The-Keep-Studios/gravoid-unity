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
			get { return this.lengthInAxisY / 2; }
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
