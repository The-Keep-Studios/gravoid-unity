using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class Slider : Widget{

		public bool horizontal;

		public float lastValue;

		public float minValue;

		public float maxValue;

		public event EventHandler onChanged;
		
		override public void RegisterListener(EventHandler onEvent){
			onChanged += onEvent;
		}
		
		
		override public void DeregisterListener(EventHandler onEvent){
			onChanged -= onEvent;
		}
		
		override public void Draw (){
			//display the button and react to it if it had been pressed
			float newValue = drawSlider();
			if (newValue != lastValue) {
				Debug.Log("Slider " + this.Label + " value changed to " + newValue);
				if(onChanged!=null){
					EventArgs args = new TheKeepStudios.events.FloatValueChageEventArgs(lastValue, newValue);
					onChanged(this, args);
				}
				this.lastValue = newValue;
			}
		}

		private float drawSlider(){
			if(horizontal){
				return GUILayout.HorizontalSlider (lastValue, minValue, maxValue);
			}
			else{
				return GUILayout.VerticalSlider (lastValue, minValue, maxValue);
			}
		}
	}
}

