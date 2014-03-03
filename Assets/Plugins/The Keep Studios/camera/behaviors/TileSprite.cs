using System.Collections.Generic;
using UnityEngine;
namespace TheKeepStudios{
	public class TileSprite : MonoBehaviour {

		[SerializeField] private Sprite[] sourceSprites;

		[SerializeField] private Vector2 tilesOffset;

		public Vector2 TilesOffset {
			get { return tilesOffset; }
			set { tilesOffset = value; }
		}
		
		// Use this for initialization
		void Start () {
			
		}

		void Update(){

		}

		/// <summary>
		/// Shifts the tiles. Decimal values move the tile's offsets, and whole numbers are 
		/// </summary>
		/// <param name="shiftAmount">Amount to shift the tiles in the x and y.</param>
		public void ShiftTiles(Vector2 shiftAmount){
			IntVector2 rotAmt = new IntVector2(shiftAmount);
			float offsetX = shiftAmount.x - rotAmt.x;
			float offsetY = shiftAmount.y - rotAmt.y;
			TilesOffset = new Vector2(offsetX,offsetY);
			RotateTiles(rotAmt);
		}

		/// <summary>
		/// Rotates the tiles t.x positions right and t.y positions up.
		/// </summary>
		/// <param name="t">Number of places to rotate the tiles toward the right and up directions.</param>
		public void RotateTiles(IntVector2 t){
		}
	}
}