// 
// BaseShipMovementController.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC
using System;
using UnityEngine;

public class BaseShipMovementController : MonoBehaviour{
	
	[SerializeField]
	private SpecialEffects
		mySpecialEffects;
	[SerializeField]
	private bool
		canControl = true;/* Can the user control the spaceship currently? */
	
	[SerializeField]
	protected float
		inputVal;
	[SerializeField]
	protected string
		inputAxisName;


	public SpecialEffects specialEffects{
		get{
			
			if(this.mySpecialEffects == null){
				
				this.mySpecialEffects = this.gameObject.GetComponent<SpecialEffects>();
				
				if(this.mySpecialEffects == null){ //if STILL not found, add one
					
					this.mySpecialEffects = this.gameObject.AddComponent<SpecialEffects>();
				}
				
			}
			return this.mySpecialEffects;
		}
	}
	
	public bool CanControl{
		get{
			return this.canControl;
		}
	}	
	
	
	/* Use this for construction */
	void Awake(){
		
		inputVal = 0.0f;
				
	}
	
	
	/* Use this for initialization */
	void Start(){

		inputVal = 0.0f;
	
	}
	
	virtual public void Update(){

		if(!CanControl){
			
			inputVal = 0.0f;
			
		}
		
	}
	
	/* All physics work should occur in FixedUpdate, as this is called during the physics frame. */
	virtual public void FixedUpdate(){
		ReadInput();
	}

	private void ReadInput(){
		NetworkView netView = this.gameObject.GetComponent<NetworkView>();
		if(!(Network.isClient || Network.isServer) || (netView == null || netView.isMine)){
			inputVal = CanControl ? Input.GetAxis(inputAxisName) : 0.0f;
		}
	}
	
	// This function allows us to SendMessage to an object to set whether or not the player can control it
	void SetControllable(bool controllable){
		canControl = controllable;
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		if(stream.isWriting){

			//inform of the lenght of the incoming string variable
			int inputNameLen = inputAxisName.Length;
			stream.Serialize(ref inputNameLen);

			//append each character of the string variable one by one
			foreach(char nextChar in inputAxisName){
				char serChar = nextChar;
				stream.Serialize(ref serChar);
			}

			stream.Serialize(ref inputVal);
			stream.Serialize(ref canControl);
		} else{

			//serialize the input name character by character (no string support)
			int inputNameLen = 0;//get the lenght of the input name
			stream.Serialize(ref inputNameLen);
			inputAxisName = ""; //clear the inputAxisName
			for(int inputNameCharIdx = 0; inputNameCharIdx < inputNameLen; ++inputNameCharIdx){
				//append every incoming character
				char inputChar = ' ';
				stream.Serialize(ref inputChar);
				inputAxisName += inputChar;
			}

			stream.Serialize(ref inputVal);

			//always update control value through SetControllable method
			bool newCanControl = false;
			stream.Serialize(ref newCanControl);
			this.SetControllable(newCanControl);
		}
	}
	
	/* Storage for movement settings GameObjects */
	[System.Serializable]
	public class MovementSettings{
		// What is the maximum speed of this movement?
		public float maxSpeed;

		// What's the acceleration in the positive and negative directions associated with this movement?
		public float positiveAcceleration;
		public float negativeAcceleration;

		// How much drag should we apply when there isn't input for this movement?
		public float drag;

		// How much drag should we apply to slow down the movement for speeds above maxSpeed?
		public float dragWhileBeyondMaxSpeed;
		
		// This function determines which drag variable to use and returns one.
		public float ComputeDrag(float input, Vector3 velocity){
			
			return (velocity.sqrMagnitude > (maxSpeed * maxSpeed)) 
				? dragWhileBeyondMaxSpeed 
				: drag;
			
		}
		
		public Vector3 forceNormal;
	}
	
}
