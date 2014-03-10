using UnityEngine;
using System.Collections;

public class ProjectilePartBehavior : MonoBehaviour {
	[SerializeField]
	private float lengthInAxisY = 0.0F;
	
	#region MonoBehaviour Overrides
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion
	
	virtual public void JoinToLaunchedObject (Transform _parent, Vector3 _relativePosition) {
		
		this.transform.parent = _parent;
		
		this.transform.position = _relativePosition;
		
	}


	virtual public float offset {
		get { return this.lengthInAxisY; }
	}
	
}
