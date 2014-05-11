using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.events{
	public abstract class EventProducer : MonoBehaviour{

		abstract public void RegisterListener(EventHandler onEvent);

		abstract public void DeregisterListener(EventHandler onEvent);

	}
}

