using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public abstract class CUBPartBase : TheKeepStudios.spawning.Spawnable{

		[SerializeField]
		private float
			lengthInAxisY = 0.0F;
		
		virtual public void JoinToLaunchedObject(Transform _parent, Vector3 _relativePosition){
			this.transform.parent = _parent;
			this.transform.position = _relativePosition;
		}
	
		virtual public float offset{
			get { return this.lengthInAxisY; }
		}
		
		protected ProjectileBehavior getParentProjectile(){
			Transform parentTM = this.transform.parent;
			if(parentTM != null){
				return parentTM.GetComponent<ProjectileBehavior>();
			}
			return null;
		}

		abstract public void Activate(GameObject activator);
		
		virtual public void OnLaunch(GameObject activator){
		}
		
		virtual public void OnCollisionEnter(Collision collision){
			
		}
		
	}
}