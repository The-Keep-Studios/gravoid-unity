using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.events.listeners{
	public abstract class EventListener : MonoBehaviour {
		public abstract EventHandler OnEvent {
			get; 
		}
	}
}
