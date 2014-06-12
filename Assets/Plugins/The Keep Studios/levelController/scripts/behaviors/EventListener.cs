using UnityEngine;
using System.Collections;

namespace TheKeepStudios.levelController
{
	abstract public class EventListener : MonoBehaviour
	{
	
		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
		
		abstract public void OnEvent (GameObject triggeringObj, GameObject signallingObj);
			
	}
}
