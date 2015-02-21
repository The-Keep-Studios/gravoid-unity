/// <summary>
/// Gravity class from community wiki, modified for our purposes.
/// @author http://wiki.unity3d.com/index.php?title=Gravity
/// </summary>
using UnityEngine;
using System.Collections.Generic;
 
public class Gravity : MonoBehaviour {	
	
	public float range;
	
	public const float gravitationalConstant = 6.67384e-11f;

	public virtual float Mass {
		
		get {
			
			return this.rigidbody ? this.rigidbody.mass : 0;
			
		}
		
	} 
	
	void FixedUpdate () {
		
		Collider[] cols  = Physics.OverlapSphere(transform.position, range); 
 
		foreach(Collider c in cols)	{
			
			Rigidbody rb = c.attachedRigidbody;
			
			if(rb != null && rb != rigidbody ){//&& !rbs.Contains(rb)
				
				rb.AddForce( CalculateGravitationalForce(rb) , ForceMode.Force);
				
			}
			
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
	virtual public Vector3 CalculateGravitationalForce(Rigidbody foreignBody){
		
		Vector3 offset = transform.position - foreignBody.transform.position;
		
		return (Mass*foreignBody.mass) * (offset / offset.sqrMagnitude); //a "close enough to give the right impression" calculation
		
	}
	
}
