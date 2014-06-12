using UnityEngine;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	[System.Serializable]
	public class Box : Widget{

		override public void Draw (){
				GUILayout.Box(Label);
		}

	}
}
