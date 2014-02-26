// 
// TKSMath.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC

using UnityEngine;

public static class TKSMath {
	
	public static Vector3 Mulitiply ( Vector3 v1, Vector3 v2 ) {
		
		return new Vector3 ( v1.x * v2.x, v1.y * v2.y, v1.z * v2.z );
		
	}
	
}