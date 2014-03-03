using System;
using UnityEngine;

namespace TheKeepStudios{
		public struct IntVector2{
		
				public int x;
				public int y ;

				public IntVector2 (int x, int y){
						this.x = x;
						this.y = y;
				}

				public IntVector2 (float x, float y){
						this.x = Mathf.FloorToInt (x);
						this.y = Mathf.FloorToInt (y);
				}
		
				public IntVector2 (Vector2 v){
						this.x = Mathf.FloorToInt (v.x);
						this.y = Mathf.FloorToInt (v.y);
				}
		}
}

