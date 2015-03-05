// 
// SpawnProperties.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC

using UnityEngine;
using System.Collections.Generic;
	
[System.Serializable]
public class SpawnProperties: MonoBehaviour {
	/*
	 * TODO
	 * at some time adding an ability to "weight" the randomization
	 * factors would be good (for instance, allowing a form of bell
	 * curve
	 */
	/// <summary>
	/// The seconds delayed until spawn.
	/// </summary>
	public float secondsDelayedUntilSpawn = 5;

	public Vector3 minInitSpin;

	public Vector3 maxInitSpin;

	public Vector3 maxInitVelocity;

	public Vector3 minInitVelocity;

	public List<Transform> spawnables;
}