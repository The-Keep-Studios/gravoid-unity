using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.menu.listeners {
	/// <summary>
	/// Scene loader which loads a given scene when onEvent is triggered.
	/// </summary>
	public class SceneLoader : events.EventListener{
		public string sceneToLoad;
		
		public EventHandler onEvent;
		
		override public EventHandler OnEvent {
			get {
				return LoadScene;
			}
		}

		private void LoadScene(object source, EventArgs e){
			Application.LoadLevel(sceneToLoad);
		}
	}
}
