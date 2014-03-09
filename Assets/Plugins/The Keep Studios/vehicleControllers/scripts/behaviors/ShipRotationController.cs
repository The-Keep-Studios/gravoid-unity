
// 
// ShipRotationController.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC
using System;
using UnityEngine;
using System.Collections.Generic;
using Fabric;

public class ShipRotationController : BaseShipMovementController
{
	
	public MovementSettings rotationalMovement;
	
	/* All physics work should occur in FixedUpdate, as this is called during the physics frame. */
	new void FixedUpdate ()
	{
		
		base.FixedUpdate ();
		
		inputVal *= (inputVal > 0.0) 
			? rotationalMovement.positiveAcceleration 
			: rotationalMovement.negativeAcceleration;

		rigidbody.angularDrag = rotationalMovement.ComputeDrag (inputVal, rigidbody.angularVelocity);
		     
		rigidbody.AddRelativeTorque (rotationalMovement.forceNormal * inputVal * Time.deltaTime, ForceMode.VelocityChange);
	}

	/* The Update () function only serves to provide special effects in this case. */
	override public void Update ()
	{
		
		base.Update ();
		
		UpdateSpecialEffect (specialEffects.positiveTurnEffect, inputVal);
		
		UpdateSpecialEffect (specialEffects.negativeTurnEffect, inputVal * -1);
		
		if (Input.GetButtonDown ("Horizontal"))
		{
			Fabric.EventManager.Instance.PostEvent("Player_Ship_Thrusters", Fabric.EventAction.PlaySound, null, gameObject);
		}
		else if (Input.GetButtonUp ("Horizontal"))
		{
			Fabric.EventManager.Instance.PostEvent("Player_Ship_Thrusters", Fabric.EventAction.StopSound, null, gameObject);
		}

		
	}


	private void UpdateSpecialEffect (List<ThrusterSpecialEffect> thrusters, float val)
	{
		
		foreach (ThrusterSpecialEffect thruster in thrusters) {
			
			if (thruster == null) {
				
				return;
				
			} else if (val > 0.01f) {
				
				thruster.TurnRatio = val;
				
			} else {
				
				thruster.TurnRatio = 0;
				
			}
			
		}
		
	}

	/* The Reset () function is called by Unity when you first add a script, and when you choose Reset on the gear popup menu for the script. */
	void Reset ()
	{
		
		// Set some nice default values for our MovementSettings.
		if (rotationalMovement == null) {
			rotationalMovement = new MovementSettings ();
		}
		
		rotationalMovement.maxSpeed = 2.0f;
		rotationalMovement.dragWhileBeyondMaxSpeed = 16.0f;
		rotationalMovement.drag = 0.1f;
		
		rotationalMovement.positiveAcceleration = 50.0f;
		rotationalMovement.negativeAcceleration = 50.0f;
		
		//we turn around this axis
		rotationalMovement.forceNormal.x = 0; 
		rotationalMovement.forceNormal.y = 0; 
		rotationalMovement.forceNormal.z = -1;
		
		this.inputAxisName = "Horizontal";
		
		this.specialEffects.positiveTurnEffect = null;
		this.specialEffects.negativeTurnEffect = null;
		
	}
	
}
