// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.
using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour
{
	/// The object in our scene that our camera is currently tracking.
	public Transform target;
	/// How far back should the camera be from the target?
	public float distance = 15.0f;
	/// How strict should the camera follow the target?  Lower values make the camera more lazy.
	public float springiness = 4.0f;
	private Vector3 lastGoalPosition = Vector3.zero;
	[SerializeField] private string defaultTargetTag = "Player";

	/// Keep handy reference sto our level's attributes.  We set up these references in the Awake () function.
	/// This also is very slightly more performant, but it's mostly just convenient.
	//private LevelAttributes levelAttributes;
	private Rect levelBounds;
	//private bool targetLock = false;

	/// This is for setting interpolation on our target, but making sure we don't permanently
	/// alter the target's interpolation setting.  This is used in the SetTarget () function.
	private RigidbodyInterpolation savedInterpolationSetting = RigidbodyInterpolation.None;

	void  Awake ()
	{
		if(this.target == null){
			Debug.LogWarning("No target set for the camera scrolling, so seeking one with the default tag now");
			//default to following the locally owned player
			GameObject[] players = GameObject.FindGameObjectsWithTag (defaultTargetTag);
			if(players.Length == 1){
				//only one player, so we are going to use that
				SetTarget(players[0].transform, true);
			}
			else{
				//multiple players, so we must assume that this
				foreach(GameObject player in players){
					NetworkView netview = player.GetComponent<NetworkView>();
					if((netview != null && netview.isMine) || player){
						SetTarget(player.transform, true);
					}
				}
			}
			Debug.LogError("No default tagged target found for camera scrolling to target");
		}
	}

	public void  SetTarget (Transform newTarget, bool snap)
	{
		// If there was a target, reset its interpolation value if it had a rigidbody.
		if (target) {
			// Reset the old target's interpolation back to the saved value.
			Rigidbody targetRigidbody = target.GetComponent<Rigidbody> ();
			if (targetRigidbody)
				targetRigidbody.interpolation = savedInterpolationSetting;
		}
	
		// Set our current target to be the value passed to SetTarget ()
		target = newTarget;
	
		// Now, save the new target's interpolation setting and set it to interpolate for now.
		// This will make our camera move more smoothly.  Only do this if we didn't set the
		// target to null (nothing).
		if (target) {
			Rigidbody targetRigidbody = target.GetComponent<Rigidbody> ();
			if (targetRigidbody) {
				savedInterpolationSetting = targetRigidbody.interpolation;
				targetRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
		}
	
		// If we should snap the camera to the target, do so now.
		// Otherwise, the camera's position will change in the LateUpdate () function.
		if (snap) {
			transform.position = GetGoalPosition ();
		}
	}

// Provide another version of SetTarget that doesn't require the snap variable to set.
// This is for convenience and cleanliness.  By default, we will not snap to the target.
	public void  SetTarget (Transform newTarget) {
		SetTarget (newTarget, false);
	}

// This is a simple accessor function, sometimes called a "getter".  It is a publically callable
// function that returns a private variable.  Notice how target defined at the top of the script
// is marked "private"?  We can not access it from other scripts directly.  Therefore, we just
// have a function that returns it.  Sneaky!
	public Transform  GetTarget () {
		return target;
	}

	void  FixedUpdate () {
		// Where should our camera be looking right now?
		lastGoalPosition = GetGoalPosition ();
	
		// Interpolate between the current camera position and the goal position.
		// See the documentation on Vector3.Lerp () for more information.
		transform.position = Vector3.Lerp (transform.position, lastGoalPosition, Time.deltaTime * springiness);	
	}

	// Based on the camera attributes and the target's special camera attributes, find out where the
	// camera should move to.
	Vector3  GetGoalPosition ()
	{

		// If there is no target, don't move the camera.  So return the camera's current position as the goal position.
		if (!target) {
			return transform.position;
		}
	
		// find camera target attributes if possible, if not use the defaults
		CameraTargetAttributes cameraTargetAttributes = target.GetComponent <CameraTargetAttributes> ();
		float heightOffset = cameraTargetAttributes ? cameraTargetAttributes.heightOffset : 0.0f;
		float distanceModifier = cameraTargetAttributes ? cameraTargetAttributes.distanceModifier : 1.0f;
		float velocityLookAhead = cameraTargetAttributes ? cameraTargetAttributes.velocityLookAhead : 0.0f;
		Vector2 maxLookAhead = cameraTargetAttributes ? cameraTargetAttributes.maxLookAhead : new Vector2 (0.0f, 0.0f);
	
		// First do a rough goalPosition that simply follows the target at a certain relative height and distance.
		Vector3 goalPosition = target.position + new Vector3 (0, heightOffset, -distance * distanceModifier);
	
		// Next, we refine our goalPosition by taking into account our target's current velocity.
		// This will make the camera slightly look ahead to wherever the character is going.
		
		// If we find a Rigidbody on the target, that means we can access a velocity!
		Rigidbody targetRigidbody = target.GetComponent<Rigidbody> ();
		Vector3 targetVelocity = targetRigidbody ? targetRigidbody.velocity : Vector3.zero;
	
		// Here we estimate what the target's position will be in velocityLookAhead seconds.
		Vector3 lookAhead = targetVelocity * velocityLookAhead;
	
		// Clamp the values to something sane, could be more dynamic but good in practice
		lookAhead.x = Mathf.Clamp (lookAhead.x, -maxLookAhead.x, maxLookAhead.x);
		lookAhead.y = Mathf.Clamp (lookAhead.y, -maxLookAhead.y, maxLookAhead.y);
		// Clear z adjustement as we are only concerned about looking ahead in 2d
		lookAhead.z = 0.0f;
	
		// Now add in our lookAhead calculation.  Our camera following is now a bit better!
		goalPosition += lookAhead;
	 
		// Send back our spiffily calculated goalPosition back to the caller!
		return goalPosition;
	}
}