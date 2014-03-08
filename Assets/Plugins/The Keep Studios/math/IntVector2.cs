using System;
using UnityEngine;

namespace TheKeepStudios
{
	[Serializable]
		public class IntVector2
		{
		[SerializeField]
				public int x;
		[SerializeField]
				public int y ;

				public IntVector2 (int x, int y)
				{
						this.x = x;
						this.y = y;
				}

				public IntVector2 (float x, float y)
				{
						this.x = Mathf.FloorToInt (x);
						this.y = Mathf.FloorToInt (y);
				}
		
				public IntVector2 (Vector2 v)
				{
						this.x = Mathf.FloorToInt (v.x);
						this.y = Mathf.FloorToInt (v.y);
				}

				public Vector2 toVector2 ()
				{
						return new Vector2 (x, y);
				}

				public static IntVector2 operator + (IntVector2 i1, IntVector2 i2)
				{
						return new IntVector2 (i1.x + i2.x, i1.y + i2.y);
				}
		
				public static IntVector2 operator - (IntVector2 i1, IntVector2 i2)
				{
						return new IntVector2 (i1.x - i2.x, i1.y - i2.y);
				}
		
				public static IntVector2 operator * (IntVector2 i1, IntVector2 i2)
				{
						return new IntVector2 (i1.x * i2.x, i1.y * i2.y);
				}
		
				public static IntVector2 operator / (IntVector2 i1, IntVector2 i2)
				{
						return new IntVector2 (i1.x / i2.x, i1.y / i2.y);
				}
		
				public static IntVector2 operator * (IntVector2 v, float i)
				{
						return new IntVector2 (v.x * i, v.y * i);
				}
		
				public static Vector2 operator / (IntVector2 v, float i)
				{
						return v.toVector2 () / i;
				}

				public static implicit operator Vector2 (IntVector2 i)
				{
						return i.toVector2 ();
				}
		}
}

