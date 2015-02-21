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

		abstract public void Activate(GameObject activator);
		
	}
}
