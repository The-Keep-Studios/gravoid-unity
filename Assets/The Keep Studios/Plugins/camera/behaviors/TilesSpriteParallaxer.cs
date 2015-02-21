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

			UpdateLastPosition ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (relativeObject.transform.position != lastPosition) {
				//calculate our tile scroll change
				TileSprite ts = this.GetComponent<TileSprite> ();
				Vector2 shiftAmount = (lastPosition - relativeObject.transform.position) * scrollRate / ts.tileSize;
				UpdateLastPosition ();
				// Debug.Log ("Adjusted parallax of tilesprite by : " + shiftAmount.ToString (), this.gameObject);
				ts.ShiftTiles (shiftAmount );
			}
		}

		void UpdateLastPosition ()
		{
			
			//store the position for the next update
			lastPosition = relativeObject.transform.position;

		}
	}
}
