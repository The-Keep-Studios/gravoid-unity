using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Menu{
	[System.Serializable]
	public class Button : Widget{
		public delegate void ClickAction();

		public event ClickAction OnClicked;

		[UnityEngine.SerializeField]
		private string label;

		public string Label {
			get { return label; }
			set { label = value; }
		}

		void RegisterClickListener(ClickAction OnClicked)
		{
			OnClicked += OnClicked;
		}
		
		
		void DeregisterClickListener(ClickAction OnClicked)
		{
			OnClicked -= OnClicked;
		}

		 override public void Draw (){
			//display the button and react to it if it had been pressed
			if (GUILayout.Button (Label)) {
				Debug.Log("Button " + this.Label + " clicked");
				if(OnClicked!=null){
					OnClicked();
				}
			}
		}
	}
}
