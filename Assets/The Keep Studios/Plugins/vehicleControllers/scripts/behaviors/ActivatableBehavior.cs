using UnityEngine;
using System.Collections;

public class ActivatableBehavior : MonoBehaviour {
	
	[UnityEngine.SerializeField]
	private bool behaviorActive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(this.behaviorActive == true){
			
			this.ActiveUpdate();
		
		} else{
			
			this.InactiveUpdate();
		
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(this.behaviorActive == true){
			
			this.ActiveFixedUpdate();
		
		} else{
			
			this.InactiveFixedUpdate();
		
		}
		
	}
	
	// Update is called once per frame
	void ActiveUpdate () {
	
	}
	
	// Update is called once per frame
	void InactiveUpdate () {
	
	}
	
	// Update is called once per frame
	void ActiveFixedUpdate () {
	
		
	}
	
	// Update is called once per frame
	void InactiveFixedUpdate () {
	
		
	}
	
	void Reset(){
		
		behaviorActive = false;
		
	}
	
	public void Activate(){
		
		this.behaviorActive = true;
		
	}
	
	public void Deactivate(){
		
		this.behaviorActive = false;
		
	}
}
