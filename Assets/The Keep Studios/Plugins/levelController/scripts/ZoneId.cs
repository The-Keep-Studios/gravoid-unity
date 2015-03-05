// 
// ZoneId.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC

using UnityEngine;

public struct ZoneId {
	
	public ZoneId ( float x, float y, float z ) {
		
		zoneCenter = new Vector3 ( x, y, z );
		
	}

	
	public ZoneId ( Vector3 id ) {
		
		zoneCenter = id;
		
	}

	
	public static bool operator == ( ZoneId id1, ZoneId id2 ) {
		
		return id1.zoneCenter == id2.zoneCenter;
		
	}

	
	public static bool operator != ( ZoneId id1, ZoneId id2 ) {
		
		return !(id1.zoneCenter == id2.zoneCenter);
		
	}
	
	public override int GetHashCode () {
		return this.zoneCenter.GetHashCode (); //we hash by the zoneCenter, the basis of the id
	}
	
	public override bool Equals (object obj) {
		
		if( obj != null && obj.GetType() ==  typeof(ZoneId) ){
			
			ZoneId other = (ZoneId) obj;
			
			return this == other;
			
		}
		else{
			
			return base.Equals (obj);
			
		}
	}

	
	public Vector3 zoneCenter;
	
}

