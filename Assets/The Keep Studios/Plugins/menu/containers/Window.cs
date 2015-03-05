using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.menu.containers
{
	public class Window : MonoBehaviour
	{
		protected static int windowID = 0;
		private int myWindowId = ++windowID; //increment the global window id and take that as our id
		public string menuName;
		public GUISkin skin;
		public Rect windowRect;
		[Range(0,100)] public float defaultWindowScreenPercentHeight;
		[Range(0,100)] public float defaultWindowScreenPercentWidth;

		// Use this for initialization
		void Start ()
		{
			SetWindowToDefaultSize ();
		}
	
		void  OnGUI ()
		{
			GUI.skin = skin;
			windowRect = GUILayout.Window (myWindowId, windowRect, this.Draw, menuName);
		}
	
		void Draw (int windowID)
		{
			GUI.skin = skin;
			//Widgets
			foreach (widgets.Widget nextWidget in this.gameObject.GetComponents<widgets.Widget>()) {
				nextWidget.Draw ();
			}
		}

		void SetWindowToDefaultSize ()
		{
			// FIXME for some reason this doesn't place the box correctly for screen precentages less than 50%
			float height = Screen.height * defaultWindowScreenPercentHeight / 100;
			float width = Screen.width * defaultWindowScreenPercentWidth  / 100;
			float top = (Screen.height - height) / 2 ;
			float left =  (Screen.width - width) / 2 ;
			windowRect.Set ( left, top,  width, height);
		}
	}
}