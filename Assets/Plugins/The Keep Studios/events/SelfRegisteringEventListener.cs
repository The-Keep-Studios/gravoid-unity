using System;

namespace TheKeepStudios.events {
	/// <summary>
	/// Scene loader which loads a given scene when onEvent is triggered.
	/// </summary>
	abstract public class SelfRegisteringEventListener : events.EventListener{

		public EventProducer eventProducer;
		
		// Use this for initialization
		void Start () {
			eventProducer.RegisterListener(this.OnEvent);
		}

	}
}
