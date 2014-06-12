﻿using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	/// <summary>
	/// Scene loader which loads a given scene when onEvent is triggered.
	/// </summary>
	public class SceneLoader : SelfRegisteringEventListener{
		public string sceneToLoad;
		
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