using System;

namespace TheKeepStudios.events.listeners {
	/// <summary>
	/// Scene loader which loads a given scene when onEvent is triggered.
	/// </summary>
	abstract public class SelfRegisteringEventListener : EventListener{

		public producers.EventProducer eventProducer;
		
		// Use this for initialization
		void Start () {
			eventProducer.RegisterListener(this.OnEvent);
		}

	}
}
