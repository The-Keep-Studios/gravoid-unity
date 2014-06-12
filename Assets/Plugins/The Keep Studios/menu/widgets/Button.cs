using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class Button : Widget{

		 override public void Draw (){
			//display the button and react to it if it had been pressed
			if (GUILayout.Button (Label)) {
				Debug.Log("Button " + this.Label + " clicked");
				OnTKSEvent(EventArgs.Empty);
			}
		}

	}
}
