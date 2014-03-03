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
		[SerializeField] private Transform[] tiles;
		[SerializeField] private Vector2 offset;
		[SerializeField] private IntVector2 rotation;
		[SerializeField] private Sprite[] sourceSprites;
		[SerializeField] private bool fillRandomly;
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
			//no clue how I want to do this yet, will have to figure it out
			throw new System.NotImplementedException ();
		}

		private void UpdateSpritePositions ()
		{
			spritesNeedPositionUpdate = false;
			for (uint tiles_idx = 0; tiles_idx < this.tiles.Length; ++tiles_idx) {
				Transform tile = this.tiles [tiles_idx];
				tile.position = getTilePosition (tiles_idx);
			}
			spritesNeedPositionUpdate = false;
		}

		private Vector3 getTilePosition (uint tiles_idx)
		{
			return TilesOffset + (tileSize * ((getRowCol (tiles_idx) + Rotation) - (gridSize / 2)));
		}

		private IntVector2 getRowCol (uint tiles_idx)
		{
			int row = Mathf.FloorToInt (tiles_idx / gridSize.y);
			int column = Mathf.FloorToInt (tiles_idx % gridSize.y);
			return new IntVector2 (row, column);
		}
	}
}