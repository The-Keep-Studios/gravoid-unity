using System.Collections.Generic;
using UnityEngine;

namespace TheKeepStudios
{
	[RequireComponent(typeof(TheKeepStudios.TileSprite))]
	public class TilesSpriteParallaxer : MonoBehaviour
	{
		public float scrollRate;
		private Vector3 lastPosition;
		public Transform relativeObject;
	
		// Use this for initialization
		void Start ()
		{			
			// set parent of tilesprite to achieve a parallaxing effect by sticking (literally) to the object
			transform.parent = relativeObject.transform;

			UpdateLastPosition();
		}
	
		// Update is called once per frame
		void Update ()
		{
			//calculate our tile scroll change
			Vector2 shiftAmount = (lastPosition - relativeObject.transform.position) * scrollRate;
			UpdateLastPosition();
			Debug.Log("Adjusted parallax of tilesprite by : " + shiftAmount.ToString(), this.gameObject);
			TileSprite ts =  this.GetComponent<TileSprite> ();
			ts.ShiftTiles (shiftAmount/ts.tileSize);
		}

		void UpdateLastPosition(){
			
			//store the position for the next update
			lastPosition = relativeObject.transform.position;

		}
	}
}
