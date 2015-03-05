using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.levelController
{
	public class TriggerEventRelayer : MonoBehaviour
	{
		
		[UnityEngine.SerializeField]
		protected List<EventListener>
			onEnterListeners;
			
		[UnityEngine.SerializeField]
		protected List<EventListener>
			onExitListeners;
		
		[UnityEngine.SerializeField]
		protected List<EventListener>
			onAllListeners;
		
		public EventSignaler onEnterSignaler;
		
		public EventSignaler onExitSignaler;
		
		public EventSignaler onAllSignaler;
		
		// Use this for initialization
		void Start ()
		{
			
			this.GetComponent<Collider>().isTrigger = true; //turn on trigger effect
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}	
		
		void OnTriggerEnter (Collider other)
		{
			
			this.onEnterSignaler.activateListeners (this.onEnterListeners, other.gameObject, this.gameObject);
			
			this.onAllSignaler.activateListeners (this.onAllListeners, other.gameObject, this.gameObject);
			
		}
		
		void OnTriggerExit (Collider other)
		{
			
			this.onExitSignaler.activateListeners (this.onExitListeners, other.gameObject, this.gameObject);
			
			this.onAllSignaler.activateListeners (this.onAllListeners, other.gameObject, this.gameObject);
			
		}
		
		
		
	}
}
