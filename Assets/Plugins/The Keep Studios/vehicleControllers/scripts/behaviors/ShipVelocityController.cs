// 
// ShipVelocityController.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC
using System;
using UnityEngine;
using System.Collections.Generic;
using Fabric;

public class ShipVelocityController : BaseShipMovementController
{
	public float lastDrag = 0.0f;
	public Vector3 lastForce;
	public MovementSettings positionalMovement;/* Helper for translational (position) movement.*/
	
	/* Use this for construction */
	void Awake ()
	{
		
		inputVal = 0.0f;
				
	}
	
	
	/* Use this for initialization */
	void Start ()
	{
		
		inputVal = 0.0f;

	}
	
	
	/* All physics work should occur in FixedUpdate, as this is called during the physics frame. */
	override public void FixedUpdate ()
	{		
		
		base.FixedUpdate ();
		
		rigidbody.drag = positionalMovement.ComputeDrag (inputVal, rigidbody.velocity);
		
		lastDrag = rigidbody.drag;
			
		float forceMagnitude = (inputVal > 0.0) 
			? positionalMovement.positiveAcceleration 
			: positionalMovement.negativeAcceleration;
		
		forceMagnitude *= inputVal;
		
		Vector3 force = positionalMovement.forceNormal * forceMagnitude;
		
		lastForce = force;

		rigidbody.AddRelativeForce (force, ForceMode.Acceleration);
		
	}
	

	/* The Update () function only serves to provide special effects in this case. */
	override public void Update ()
	{
		
		base.Update ();
		
		UpdateSpecialEffect (specialEffects.positiveThrustEffect, inputVal);
		
		UpdateSpecialEffect (specialEffects.negativeThrustEffect, inputVal * -1);

		if (Input.GetButtonDown ("Vertical"))
		{
			Fabric.EventManager.Instance.PostEvent("Player_Ship_Thrusters", Fabric.EventAction.PlaySound, null, gameObject);
		}
		else if (Input.GetButtonUp ("Vertical"))
		{
			Fabric.EventManager.Instance.PostEvent("Player_Ship_Thrusters", Fabric.EventAction.StopSound, null, gameObject);
		}



	}


	private void UpdateSpecialEffect ( List<ThrusterSpecialEffect> thrusters, float val)
	{
		
		foreach (ThrusterSpecialEffect thruster in thrusters) {
		
			if (thruster == null) {
			
				return;
			
			} else if (val > 0.01f) {
			
				thruster.ThrustRatio = val;
			
			} else {
			
				thruster.ThrustRatio = 0;
			
			}
			
		}
		
	}

	/* The Reset () function is called by Unity when you first add a script, and when you choose Reset on the gear popup menu for the script. */
	void Reset ()
	{
		/*
			* Set some nice default values for our MovementSettings.
		*/		// Set some nice default values for our MovementSettings.
		if (positionalMovement == null) {
			positionalMovement = new MovementSettings ();
		}
		
		positionalMovement.maxSpeed = 3.0f;
		positionalMovement.drag = 3.0f;
		positionalMovement.dragWhileBeyondMaxSpeed = 4.0f;
		positionalMovement.positiveAcceleration = 50.0f;
		positionalMovement.negativeAcceleration = 0.0f;// By default, we don't have reverse thrusters.
		
		//we push in this direction
		positionalMovement.forceNormal.x = 0; 
		positionalMovement.forceNormal.y = 1; 
		positionalMovement.forceNormal.z = 0; 
		
		inputAxisName = "Vertical";
		
		this.specialEffects.positiveThrustEffect = null;
		this.specialEffects.negativeThrustEffect = null;
		
	}
	
}
