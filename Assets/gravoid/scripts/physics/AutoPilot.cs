using UnityEngine;
using System.Collections;

/// <summary>
/// Thrust controller which acts as a simple physics based movement controller.
/// Useful for mimicing simple space craft movement.
/// </summary>
public class AutoPilot : MonoBehaviour{
	
	/// <summary>
	/// The tolerance in radians which is used to determine if the angle from Vector3.up vector to a target Vector3 
	/// point is considered "facing" a target.
	/// </summary>
	[SerializeField] private float facingAngleTolerance;
	
	/// <summary>
	/// The tolerance in system units which is used to determine if the distance from this to the target Vector3 point 
	/// is considered "at" the target.
	/// </summary>
	[SerializeField] private float distanceTolerance;
	
	[SerializeField] private float maxPropulsionSpeed;
	
	[SerializeField] private float maxAcceleration;
	
	[SerializeField] private float maxRotationSpeed;
	
	[SerializeField] private float rotationAcceleration;
	
	[SerializeField] private bool autopilotToTarget;
	
	[SerializeField] private Vector3 target;

	public bool AutopilotToTarget {
		get {
			return this.autopilotToTarget;
		}
		set {
			autopilotToTarget = value;
		}
	}

	public float DistanceTolerance {
		get {
			return this.distanceTolerance;
		}
		set {
			distanceTolerance = value;
		}
	}

	public float FacingAngleTolerance {
		get {
			return this.facingAngleTolerance;
		}
		set {
			facingAngleTolerance = value;
		}
	}

	public float MaxAcceleration {
		get {
			return this.maxAcceleration;
		}
		set {
			maxAcceleration = value;
		}
	}

	public float MaxPropulsionSpeed {
		get {
			return this.maxPropulsionSpeed;
		}
		set {
			maxPropulsionSpeed = value;
		}
	}

	public float MaxRotationSpeed {
		get {
			return this.maxRotationSpeed;
		}
		set {
			maxRotationSpeed = value;
		}
	}

	public float RotationAcceleration {
		get {
			return this.rotationAcceleration;
		}
		set {
			rotationAcceleration = value;
		}
	}
	
	public Vector3 Target {
		get {
			return target;
		}
		set {
			
			target = value;
			
			this.AutopilotToTarget = Vector3.Distance(target, this.transform.position) > this.DistanceTolerance;
			
		}
	}

	public bool TargetReached {
		get {
			return Vector3.Distance(this.target, this.transform.position) <= this.distanceTolerance;
		}
	}
	
	private Vector3 VectorToTarget{
		get{
			return this.Target - this.transform.position;
		}
	}
	
	private float AngleToTarget{
		get{
			return Vector3.Angle(this.transform.forward, this.VectorToTarget);
		}
	}
	
	private bool FacingTarget{
		get{
			return AngleToTarget <= this.facingAngleTolerance;
		}
	}
	
	public void FixedUpdate(){
		
		if(!this.AutopilotToTarget || this.TargetReached){
			
			return;
			
		}
			
		/* 
		 * Turn toward the target.
		 * We always try this, as we don't mind trying to be MORE precise with our facing),aim tolerance mearly allows 
		 * us to being thrusting toward the target before we are EXACTLY facing it.
		 */		
		RotateTowardsPoint();
		
		if( FacingTarget ) {
			
			//move forward to point			
			PropelTowardsPoint();
			
		}
		
	}
	
	private void PropelTowardsPoint(){
		
		//store the delta time for repeated reference
		float dT = Time.deltaTime;
		
		//determine the max distance we can travel at max speed over the given time frame
		float maxDistance = this.maxPropulsionSpeed*dT;
		
		//find the target distance we want to travel
		Vector3 targetDistance = Vector3.MoveTowards( this.transform.position, Target, maxDistance);
		
		//find the velocity we want to be traveling in 
		Vector3 targetV = targetDistance / dT;
		
		//find how much of a change in velocity we need (i.e. our needed delta velocity or dV)
		Vector3 dV = targetV - rigidbody.velocity;
		
		//find our acceleration but NEVER exceed our max accelleration
		Vector3 a = Vector3.ClampMagnitude(dV/dT, this.maxAcceleration); 
		
		//determine our desired thrust as force in newtons (a product of the ridged body mass and the desired accelleration)
		Vector3 f = rigidbody.mass * a;
		
		Debug.Log("Propelling towards " + this.Target.ToString() + " with force " + f.ToString() , this.gameObject);
		
		rigidbody.AddForce(f,ForceMode.Force);
		
	}
	
	private void RotateTowardsPoint(){
		
		//TODO improve this GREATELY, probably needs gradual slowing down code, ect.
		
		// get its cross product, which is the axis of rotation to get from one vector to the other
		Vector3 cross = Vector3.Cross(this.transform.forward, this.VectorToTarget);
		
		Vector3 torque = cross * this.AngleToTarget * rotationAcceleration;
		
		Debug.Log("Turning to face " + this.Target.ToString() + " with torque " + torque.ToString() , this.gameObject);
		
		//TODO actually calculate this to reach our goal exactly so we don't overshoot (and also slow DOWN when needed) 
		// apply torque along that axis according to the magnitude of the angle.
		rigidbody.AddTorque(torque, ForceMode.Acceleration);
		
	}
	
	//TODO add reset and other initialization functions
	
}
