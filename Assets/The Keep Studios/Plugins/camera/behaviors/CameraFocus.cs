// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.
using UnityEngine;
using System.Collections;

[UnityEngine.RequireComponent ( typeof(CameraScrolling))]
public class CameraFocus : MonoBehaviour
{
	// Script that puts a window on-screen where the player can toggle who he controls
	// It works by sending SetControllable messages to turn the different characters on and off.
	// It also changes who the CameraScrolling scripts looks at.

	// An internal reference to the attached CameraScrolling script
	private CameraScrolling cameraScrolling;

	// Who is the player controlling
	private int selected = 0;

	// List of objects to control
	public Transform[] targets;

	// What to display on the buttons in the window
	public string[] targetButtonNames;

	// What to display on the buttons in the window
	public string[] targetInstructions;
	
	private Rect windowRect = new Rect (20, 20, 250, 50);
	// Make the onscreen GUI to let the player switch control between Lerpz and the spaceship.


// On start up, we send the SetControllable () message to turn the different players on and off.
	void  Awake ()
	{

		// Get the reference to our CameraScrolling script attached to this camera;
		cameraScrolling = GetComponent<CameraScrolling> ();
	
		// Set the scrolling camera's target to be our character at the start.
		cameraScrolling.SetTarget (targets [0], true);
	
		// tell all targets (except the first one) to switch off.
		for (int i=0; i < targets.Length; i++) 
			targets [i].gameObject.SendMessage ("SetControllable", (i == 0), SendMessageOptions.DontRequireReceiver);
	}

	void  OnGUI ()
	{
		// Make a popup window
		windowRect = GUILayout.Window (0, windowRect, DoControlsWindow, "Controls");
	
		// The window can be dragged around by the users - make sure that it doesn't go offscreen.
		windowRect.x = Mathf.Clamp (windowRect.x, 0.0f, Screen.width - windowRect.width);
		windowRect.y = Mathf.Clamp (windowRect.y, 0.0f, Screen.height - windowRect.height);
	}

	// Make the contents of the window
	void  DoControlsWindow (int windowID)
	{
		// Make the window be draggable in the top 20 pixels.
		GUI.DragWindow (new Rect (0, 0, System.Single.MaxValue, 20));
	
		GUILayout.Label ("Currently selected vehicle...");

		// Let the player select the character
		selected = GUILayout.Toolbar (selected, targetButtonNames);

		// If the user has selected a new character, we'll send new SetControllable messages to turn on the other character. 
		// Then we'll change who the CameraScrolling script is tracking.
		if (GUI.changed && targets [selected] != cameraScrolling.GetTarget ()) {
			targets [selected].gameObject.SendMessage ("SetControllable", true, SendMessageOptions.DontRequireReceiver);
			cameraScrolling.GetTarget ().gameObject.SendMessage ("SetControllable", false, SendMessageOptions.DontRequireReceiver);
			cameraScrolling.SetTarget (targets [selected]);
		}
	
		// Show a different instruction label depending on what was selected above.
		GUILayout.Label ("Instructions:\n" + targetInstructions [selected]);
	
	}

}