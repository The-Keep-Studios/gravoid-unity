using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace TheKeepStudios.events{
	public class EventRegistrationManager : MonoBehaviour {

		[System.Serializable]
		public class Registration{
			public EventProducer producer;
			public List<EventListener> listeners;
		}

		public List<Registration> registrations;

		// Use this for initialization
		void Start () {
			foreach(Registration r in registrations){
				foreach(EventListener listener in r.listeners){
					r.producer.RegisterListener(listener.OnEvent);
				}
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}