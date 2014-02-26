using UnityEngine;
using System.Collections.Generic;

public class TilesSpriteScroller : MonoBehaviour
{
	//FIXME we need a new tilesprite object, OR to have this work without the tilesprite behavior
	#if false
	public float scrollRate;
	private Vector3 lastPosition;
	public OTTilesSprite tilesSprite;
	private bool initialized = false;

	// Use this for initialization
	void Start ()
	{
		lastPosition = this.transform.position;	
	}

	public bool Initialized {
		get {
			// make sure orthello is initialized and all containers are ready to go
			if (!OT.isValid || !OT.ContainersReady ()) {
				return false;
			} else if (!initialized) {
				// find the tilemap object from this scene
				//tilemap = GameObject.Find("TileMap").GetComponent<OTTileMap>();	
				tilesSprite.visible = true;
				tilesSprite.FillWithRandomTiles (true);	
				initialized = true;
			}
			return true;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		
		if (Initialized) {
		
			//calculate our tile scroll change
			Vector3 currentPos = this.transform.position;
			
			Vector3 scrollChange = ((this.transform.position - lastPosition) * scrollRate);

			//store the position for the next update
			lastPosition = this.transform.position;
			
			Vector2 deltaPos = scrollChange + tilesSprite.transform.localPosition;
			
			IVector2 tilesToScroll = new IVector2 (
					Mathf.FloorToInt (deltaPos.x / tilesSprite.tileSize.x)
					, Mathf.FloorToInt (deltaPos.y / tilesSprite.tileSize.y)
			);
			
			Vector2 nextPosition = new Vector2 (
					(deltaPos.x - (tilesSprite.tileSize.x * tilesToScroll.x))
					, (deltaPos.y - (tilesSprite.tileSize.y * tilesToScroll.y))
			);
			
			/*
			Debug.Log (
				"deltaPos: " + deltaPos.ToString () + 
				"Scroll Change: " + scrollChange.ToString () + 
				", Scrolling by: " + tilesToScroll + 
				", Moving to map offset: " + nextPosition.ToString () +
				", Tilesize: " + tilesSprite.tileSize
			);
			*/
			
			// set position of sprite to achieve a smooth scrolling effect
			tilesSprite.position = nextPosition;
		}
		
	}
	#endif
	
}
