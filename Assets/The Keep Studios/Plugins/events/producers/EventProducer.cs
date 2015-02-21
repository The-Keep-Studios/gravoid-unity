using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.events.producers{
	public abstract class EventProducer : MonoBehaviour{
		
		private event EventHandler tksEvent;

		protected virtual void OnTKSEvent (EventArgs e)
		{
			StartCoroutine(triggerEvent(e));
		}

		protected virtual IEnumerator triggerEvent(EventArgs e){
			//trigger events during coroutine processing
			yield return null;
			var handler = tksEvent;
			if (handler != null)
				handler (this, e);
		}
		
		virtual public void RegisterListener(EventHandler onEvent){
			tksEvent += onEvent;
		}
		
		virtual public void DeregisterListener(EventHandler onEvent){
			tksEvent -= onEvent;
		}

	}
}

