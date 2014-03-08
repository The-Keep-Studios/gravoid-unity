using System.Collections.Generic;
using UnityEngine;

namespace TheKeepStudios
{
	public class TileSprite : MonoBehaviour
	{

		/// The height of the grid in tiles
		[SerializeField] public IntVector2 gridSize;

		/// The size of each individual tile within the tilesprite.
		[SerializeField] public float tileSize;

		/// The tiles in their original starting grid positions stored in row-major order.
		[SerializeField] private List<Transform> tiles;
		[SerializeField] private Vector2 offset;
		[SerializeField] private IntVector2 rotation;
		[SerializeField] private List<Sprite> sourceSprites;
		[SerializeField] private bool fillRandomly;

		[SerializeField] public int seedNumber;


		private bool spritesNeedPositionUpdate;

		public Vector2 TilesOffset {
			get { return offset; }
			set {
				if (Mathf.Abs (offset.x) >= 1 || Mathf.Abs (offset.y) >= 1) {
					throw new System.ArgumentOutOfRangeException ("TilesOffset x and y values magnitude must each be less than 1");
				}
				spritesNeedPositionUpdate = true;
				offset = value;
			}
		}

		public IntVector2 Rotation {
			get { return rotation; }
			set {
				spritesNeedPositionUpdate = true;
				// values greater than a given grid size "loop", so we do that here
				rotation.x = value.x % this.gridSize.x;
				rotation.y = value.y % this.gridSize.y;
			}
		}

		// Use this for initialization
		void Start ()
		{
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
			foreach(Transform tile in this.tiles){
				Transform.Destroy(tile.gameObject); //destroy each existing tile
			}
			this.tiles.Clear();
			this.tiles.Capacity =  gridSize.x * gridSize.y; //Sets the capacity of the list to be the correct number
			for (int i = 0; i<tiles.Capacity;++i)
			{
				SpriteRenderer renderer = new GameObject(this.gameObject.name + "-sprite_"+i).AddComponent<SpriteRenderer>();
				renderer.sprite = sourceSprites[ Random.Range(0,sourceSprites.Count)];
				renderer.transform.parent = this.transform;
				this.tiles.Add ( renderer.transform );
			}

		}

		private void UpdateSpritePositions ()
		{
			spritesNeedPositionUpdate = false;
			for (int tiles_idx = 0; tiles_idx < this.tiles.Count; ++tiles_idx) {
				Transform tile = this.tiles[tiles_idx];
				tile.position = getTilePosition (tiles_idx);
			}
			spritesNeedPositionUpdate = false;
		}

		private Vector3 getTilePosition (int tiles_idx)
		{
			Vector2 point = TilesOffset + (tileSize * ((getRowCol (tiles_idx) + Rotation) - (gridSize / 2)));
			return  new Vector3(point.x, point.y, 0);
		}

		private IntVector2 getRowCol (int tiles_idx)
		{
			int row = Mathf.FloorToInt (tiles_idx / gridSize.y);
			int column = Mathf.FloorToInt (tiles_idx % gridSize.y);
			return new IntVector2 (row, column);
		}
	}
}