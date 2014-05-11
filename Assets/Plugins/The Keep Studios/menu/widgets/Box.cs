using UnityEngine;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	[System.Serializable]
	public class Box : Widget{

		public override void RegisterListener (System.EventHandler onEvent){
			throw new System.NotImplementedException ();
		}

		public override void DeregisterListener (System.EventHandler onEvent){
			throw new System.NotImplementedException ();
		}

		override public void Draw (){
				GUILayout.Box(Label);
		}

	}
}
