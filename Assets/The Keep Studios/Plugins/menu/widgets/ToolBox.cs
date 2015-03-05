using UnityEngine;
using System;
using System.Collections;
using TheKeepStudios.events.eventArgs;

namespace TheKeepStudios.menu.widgets{
	public class ToolBox : Widget{

		public int currentSelected = 0;

		public string[] options;
		
		override public void Draw (){
			//display the toolbox and react to it if it had been changed
			int newSelected = GUILayout.Toolbar(currentSelected,options);
			if (newSelected != currentSelected) {
				Debug.Log("Toolbox " + this.Label + " value changed to " + options[newSelected]);
				this.OnTKSEvent(new IntValueChageEventArgs(this.currentSelected, newSelected));
				this.currentSelected = newSelected;
			}
		}
	}
}

