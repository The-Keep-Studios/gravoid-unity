using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid
{
	public class CursorFollower : MonoBehaviour
	{
	
		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
			
			Vector3 newPosition;
				
			if (Input.touchCount != 1) {
				
				newPosition = Input.mousePosition;
				
			} else {
				
				newPosition = Input.touches [0].position;
				
			}
			
			newPosition.z = this.transform.position.z - Camera.main.transform.position.z;
				
			newPosition = Camera.main.ScreenToWorldPoint (newPosition);
			
			this.transform.position = newPosition;
		}
	}
}
