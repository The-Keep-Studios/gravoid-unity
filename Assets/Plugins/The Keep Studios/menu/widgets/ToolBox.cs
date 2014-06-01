using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class ToolBox : Widget{

		public int currentSelected = 0;

		public string[] options;
		
		public event EventHandler onChanged;
		
		override public void RegisterListener(EventHandler onEvent){
			onChanged += onEvent;
		}
		
		override public void DeregisterListener(EventHandler onEvent){
			onChanged -= onEvent;
		}
		
		override public void Draw (){
			//display the button and react to it if it had been pressed
			int newSelected = drawSlider();
			if (newSelected != currentSelected) {
				Debug.Log("Toolbox " + this.Label + " value changed to " + options[newSelected]);
				if(onChanged!=null){
					EventArgs args = new TheKeepStudios.events.IntValueChageEventArgs(this.currentSelected, newSelected);
					onChanged(this, args);
				}
				this.currentSelected = newSelected;
			}
		}
		
		private int drawSlider(){
			return GUILayout.Toolbar(currentSelected,options);
		}
	}
}

