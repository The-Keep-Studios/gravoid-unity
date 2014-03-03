using System.Collections.Generic;
using UnityEngine;

namespace TheKeepStudios{
		[RequireComponent(typeof(TheKeepStudios.TileSprite))]
		public class TilesSpriteParallaxer : MonoBehaviour{
				public float scrollRate;
				private Vector3 lastPosition;
				public TileSprite tilesSprite;
				private bool initialized = false;
	
				// Use this for initialization
				void Start (){
						lastPosition = this.transform.position;	
				}
	
				// Update is called once per frame
				void Update (){
			
						//calculate our tile scroll change		
						tilesSprite.ShiftTiles ((this.transform.position - lastPosition) * scrollRate);
				
						// set position of tilesprite to achieve a parallaxing effect
						tilesSprite.transform.position = this.transform.position;
			
						//store the position for the next update
						lastPosition = this.transform.position;
			
				}
		}
}
