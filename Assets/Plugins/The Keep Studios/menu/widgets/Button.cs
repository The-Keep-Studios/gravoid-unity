using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class Button : Widget{
		public event EventHandler onClicked;
		
		override public void RegisterListener(EventHandler onEvent){
			onClicked += onEvent;
		}
		
		
		override public void DeregisterListener(EventHandler onEvent){
			onClicked -= onEvent;
		}

		 override public void Draw (){
			//display the button and react to it if it had been pressed
			if (GUILayout.Button (Label)) {
				Debug.Log("Button " + this.Label + " clicked");
				if(onClicked!=null){
					onClicked(this, new EventArgs());
				}
			}
		}
	}
}
