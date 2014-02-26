using UnityEngine;
using System.Collections;

public class GravityWell : Gravity {
	
	[UnityEngine.SerializeField]
	private float forceFactor;
	
	[UnityEngine.SerializeField]
	private float distanceCurvePower;
	
	[UnityEngine.SerializeField]
	private float minimumEffectIncreaseRange;
	
	[UnityEngine.SerializeField]
	private bool createGravityForces = false;

	override public float Mass {
		
		get {
			
			//simulatedForce is a property of the mass
			return forceFactor / gravitationalConstant;
			
		}
		
	}
	
	//kinda sorta accurate but... not
	/// <summary>
	/// Calculates the gravitational force. This is an approximation.
	/// </summary>
	/// <returns>
	/// The gravitational force.
	/// </returns>
	/// <param name='foreignMass'>
	/// Foreign mass.
	/// </param>
	override public Vector3 CalculateGravitationalForce(Rigidbody foreignBody){
		
		Vector3 offset = transform.position - foreignBody.transform.position;
		
		float distance = Mathf.Clamp( offset.magnitude, minimumEffectIncreaseRange, range);
		
		//the force as computed as a linear variant of the real gravitational equation
		float force = gravitationalConstant 
			* foreignBody.mass 
			* Mass  
			/ Mathf.Pow(distance, distanceCurvePower);
		
		//we want to be able to vary the curve
		return force * offset.normalized;
		
	}
	
	
	void Reset(){
		
		range = 1000;
		
		forceFactor = 1;
	
		distanceCurvePower = 0.5f;
		
		minimumEffectIncreaseRange = range/1000;
		
	}
	
}
