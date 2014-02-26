// The object in our scene that our camera is currently tracking.
var target : Transform;
// How far back should the camera be from the target?
var distance = 15.0;
// How strict should the camera follow the target?  Lower values make the camera more lazy.
var springiness = 4.0;

var lastGoalPosition :Vector3 = Vector3(0,0,0);

// Keep handy reference sto our level's attributes.  We set up these references in the Awake () function.
// This also is very slightly more performant, but it's mostly just convenient.
//private var levelAttributes : LevelAttributes;
private var levelBounds : Rect;

private var targetLock = false;

// This is for setting interpolation on our target, but making sure we don't permanently
// alter the target's interpolation setting.  This is used in the SetTarget () function.
private var savedInterpolationSetting = RigidbodyInterpolation.None;

function Awake () {
	// Set up our convenience references.
	//levelAttributes = LevelAttributes.GetInstance ();
	//levelBounds = levelAttributes.bounds;
}

function SetTarget (newTarget : Transform, snap : boolean) {
	// If there was a target, reset its interpolation value if it had a rigidbody.
	if  (target) {
		// Reset the old target's interpolation back to the saved value.
		targetRigidbody = target.GetComponent (Rigidbody);
		if  (targetRigidbody)
			targetRigidbody.interpolation = savedInterpolationSetting;
	}
	
	// Set our current target to be the value passed to SetTarget ()
	target = newTarget;
	
	// Now, save the new target's interpolation setting and set it to interpolate for now.
	// This will make our camera move more smoothly.  Only do this if we didn't set the
	// target to null (nothing).
	if (target) {
		targetRigidbody = target.GetComponent (Rigidbody);
		if (targetRigidbody) {
			savedInterpolationSetting = targetRigidbody.interpolation;
			targetRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
	}
	
	// If we should snap the camera to the target, do so now.
	// Otherwise, the camera's position will change in the LateUpdate () function.
	if  (snap) {
		transform.position = GetGoalPosition ();
	}
}

// Provide another version of SetTarget that doesn't require the snap variable to set.
// This is for convenience and cleanliness.  By default, we will not snap to the target.
function SetTarget (newTarget : Transform) {
	SetTarget (newTarget, false);
}

// This is a simple accessor function, sometimes called a "getter".  It is a publically callable
// function that returns a private variable.  Notice how target defined at the top of the script
// is marked "private"?  We can not access it from other scripts directly.  Therefore, we just
// have a function that returns it.  Sneaky!
function GetTarget () {
	return target;
}

function FixedUpdate () {
	// Where should our camera be looking right now?
	lastGoalPosition = GetGoalPosition ();
	
	// Interpolate between the current camera position and the goal position.
	// See the documentation on Vector3.Lerp () for more information.
	transform.position = Vector3.Lerp (transform.position, lastGoalPosition, Time.deltaTime * springiness);	
}

// Based on the camera attributes and the target's special camera attributes, find out where the
// camera should move to.
function GetGoalPosition () {

	// If there is no target, don't move the camera.  So return the camera's current position as the goal position.
	if  (!target){
		return transform.position;
	}
	
	// find camera target attributes if possible, if not use the defaults
	var cameraTargetAttributes = target.GetComponent ("CameraTargetAttributes");
	var heightOffset = cameraTargetAttributes ? cameraTargetAttributes.heightOffset : 0.0;
	var distanceModifier = cameraTargetAttributes ? cameraTargetAttributes.distanceModifier : 1.0;
	var velocityLookAhead = cameraTargetAttributes ? cameraTargetAttributes.velocityLookAhead : 0.0;
	var maxLookAhead = cameraTargetAttributes ? cameraTargetAttributes.maxLookAhead : Vector2 (0.0, 0.0);
	
	// First do a rough goalPosition that simply follows the target at a certain relative height and distance.
	var goalPosition = target.position + new Vector3(0,heightOffset,-distance * distanceModifier);
	
	// Next, we refine our goalPosition by taking into account our target's current velocity.
	// This will make the camera slightly look ahead to wherever the character is going.
		
	// If we find a Rigidbody on the target, that means we can access a velocity!
	var targetRigidbody = target.GetComponent(Rigidbody);
	var targetVelocity = targetRigidbody ? targetRigidbody.velocity : Vector3.zero;
	
	// Here we estimate what the target's position will be in velocityLookAhead seconds.
	var lookAhead = targetVelocity * velocityLookAhead;
	
	// Clamp the values to something sane, could be more dynamic but good in practice
	lookAhead.x = Mathf.Clamp (lookAhead.x, -maxLookAhead.x, maxLookAhead.x);
	lookAhead.y = Mathf.Clamp (lookAhead.y, -maxLookAhead.y, maxLookAhead.y);
	// Clear z adjustement as we are only concerned about looking ahead in 2d
	lookAhead.z = 0.0;
	
	// Now add in our lookAhead calculation.  Our camera following is now a bit better!
	goalPosition += lookAhead;
	 
	// Send back our spiffily calculated goalPosition back to the caller!
	return goalPosition;
}