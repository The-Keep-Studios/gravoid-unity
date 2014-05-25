using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.events{
	public abstract class EventListener : MonoBehaviour {
		public abstract EventHandler OnEvent {
			get; 
		}
	}
}
