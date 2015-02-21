// 
// ShipGravitationalVelocityController.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC
using System;
using UnityEngine;

public class ShipGravitationalVelocityController : MonoBehaviour {
	
	[SerializeField]
	private GravityWell gravityWell;
	
	void Start(){
		
		Screen.showCursor = true; //TODO turn this on somewhere nicer, but for now this works
			
		this.gravityWell.gameObject.SetActive(false);
		
	}
	
    public void Update() {
		
        if (Input.GetButtonDown("Fire1") ||  Input.touchCount == 1){
			
			this.gravityWell.gameObject.SetActive(true);
			
		}
		else if (Input.GetButtonUp("Fire1")){
			
			this.gravityWell.gameObject.SetActive(false);
			
		}
        
    }
	
}
