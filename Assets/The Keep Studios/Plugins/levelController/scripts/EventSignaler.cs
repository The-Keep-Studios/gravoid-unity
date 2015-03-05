using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.levelController
{
	public class EventSignaler : ScriptableObject
	{
	
		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		
		public void activateListeners (List<EventListener> listeners, GameObject triggeringObj, GameObject signallingObj)
		{
			
			foreach (EventListener listener in listeners) {
				
				activateListener (listener, triggeringObj, signallingObj);
				
			}
			
		}
		
		
		public void activateListener (EventListener listener, GameObject triggeringObj, GameObject signallingObj)
		{
			
			listener.OnEvent (triggeringObj, signallingObj);
			
		}
	}
}