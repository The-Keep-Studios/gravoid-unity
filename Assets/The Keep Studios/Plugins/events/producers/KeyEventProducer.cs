using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.events.producers{
	public class KeyEventProducer : EventProducer {
		public string keyName;
		
		public string KeyName {
			get { return keyName; }
			set { keyName = value; }
		}

		void Update() {
			if (Input.GetButtonDown(KeyName)) {
				OnTKSEvent(new TheKeepStudios.events.eventArgs.KeyPressedEventArgs(KeyName));
			}
		}
	}
}
