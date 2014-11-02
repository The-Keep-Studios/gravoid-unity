using System.Collections.Generic;
using UnityEngine;

namespace TheKeepStudios
{
	public class TileSprite : MonoBehaviour
	{

		/// The height of the grid in tiles
		public IntVector2 gridSize;

		/// The size of each individual tile within the tilesprite.
		public float tileSize;
		public int seedNumber;
		public bool fillRandomly;

		/// The tiles in their original starting grid positions stored in row-major order.
		[SerializeField]
		private List<Transform>
			tiles;
		[SerializeField]
		private List<Sprite>
			sourceSprites;
		private Vector2 offset = Vector2.zero;
		private IntVector2 rotation = new IntVector2 (Vector2.zero);
		private bool spritesNeedPositionUpdate;

		public Vector2 TilesOffset {
			get { return offset; }
			set {
				if (Mathf.Abs (offset.x) >= 1 || Mathf.Abs (offset.y) >= 1) {
					Debug.LogWarning ("TilesOffset x and y values magnitude must each be less than 1");
				}
				//make sure to only use the fractional part
				offset.x = value.x % 1;
				offset.y = value.y % 1;
				spritesNeedPositionUpdate = true;

			}
		}

		public IntVector2 Rotation {
			get { return rotation; }
			set {
				// values greater than a given grid size "loop", so we do that here
				if (value.x < 0) {
					value.x = value.x + this.gridSize.x;
				}
				if (value.y < 0) {
					value.y = value.y + this.gridSize.y;
				}
				rotation.x = value.x % this.gridSize.x;
				rotation.y = value.y % this.gridSize.y;
				spritesNeedPositionUpdate = true;
			}
		}

		// Use this for initialization
		void Start ()
		{
			TilesOffset = Vector2.zero;
			Rotation = new IntVector2 (Vector2.zero);
			if (fillRandomly) {
				FillSpritesRandomly ();
			}
			UpdateSpritePositions ();
		}

		void Update ()
		{
			if (spritesNeedPositionUpdate) {
				UpdateSpritePositions ();
			}
		}

		void Reset ()
		{
			tiles = new List<Transform> ();
			TilesOffset = Vector2.zero;
			Rotation = new IntVector2 (Vector2.zero);
			sourceSprites = new List<Sprite> ();
			fillRandomly = false;
		}

		/// <summary>
		/// Shifts the tiles in the direction of the Vector2 supplied by that many tiles.
		/// Shifts right by shiftAmount.x tiles, and up by shiftAmount.y tiles.
		/// Fractional values indicate offsetting each of the tiles from the grid that fraction of a tile size.
		/// </summary>
		/// <param name="shiftAmount">Amount to shift the tiles in the x and y.</param>
		public void ShiftTiles (Vector2 shiftAmount)
		{
			//calculates actual next shift. Note that yes, this will cause a bit of crazy over rotation but the assignment of rotation should end up taking care of that
			Vector2 nextShiftedPosition = shiftAmount + TilesOffset + Rotation;
			IntVector2 rotAmt = new IntVector2 (nextShiftedPosition);
			float offsetX = nextShiftedPosition.x - rotAmt.x;
			float offsetY = nextShiftedPosition.y - rotAmt.y;
			TilesOffset = new Vector2 (offsetX, offsetY);
			Rotation = rotAmt;
		}

		public void FillSpritesRandomly ()
		{
			Random.seed = seedNumber;
			foreach (Transform tile in this.tiles) {
				Transform.Destroy (tile.gameObject); //destroy each existing tile
			}
			this.tiles.Clear ();
			this.tiles.Capacity = gridSize.x * gridSize.y; //Sets the capacity of the list to be the correct number
			for (int i = 0; i<tiles.Capacity; ++i) {
				SpriteRenderer renderer = new GameObject (this.gameObject.name + "-sprite_" + i).AddComponent<SpriteRenderer> ();
				renderer.sprite = sourceSprites [Random.Range (0, sourceSprites.Count)];
				renderer.transform.parent = this.transform;
				this.tiles.Add (renderer.transform);
			}

		}

		private void UpdateSpritePositions ()
		{
			spritesNeedPositionUpdate = false;
			for (int tiles_idx = 0; tiles_idx < this.tiles.Count; ++tiles_idx) {
				Transform tile = this.tiles [tiles_idx];
				if(tile != null){
					tile.transform.localPosition = getTilePosition (tiles_idx);
				}
			}
			spritesNeedPositionUpdate = false;
		}

		/// <summary>
		/// Gets the tile position relative to this.transform.position
		/// </summary>
		/// <returns>The tile position relative to this.transform.position</returns>
		/// <param name="tiles_idx">The index number of the tile in #tiles.</param>
		private Vector3 getTilePosition (int tiles_idx)
		{
			Vector2 gridCenter = gridSize / 2;
			Vector2 gridCoords = getRowCol (tiles_idx);
			Vector2 gridPointUnscaledCoord = TilesOffset + (gridCoords - gridCenter);
			Vector2 tileCoord = tileSize * gridPointUnscaledCoord;
			return new Vector3 (tileCoord.x, tileCoord.y, 0);
		}

		private IntVector2 getRowCol (int tiles_idx)
		{
			//find the row and column via a row-major algorithm
			int row = Mathf.FloorToInt (tiles_idx / gridSize.y);
			int column = Mathf.FloorToInt (tiles_idx % gridSize.y);

			//now adjust the row and column by the current Rotation (use modulus to create the "looping" effect of rotation
			row = (Rotation.x + row) % gridSize.x;
			column = (Rotation.y + column) % gridSize.y;

			//slap them together into a Int
			return new IntVector2 (row, column);
		}
	}
}