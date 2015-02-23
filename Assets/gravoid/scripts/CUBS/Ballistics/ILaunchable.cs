using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.spawning;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{
	public interface ILaunchable{
	
		void Start();
	
		void Update();
	
		void FixedUpdate();
	
		bool Launch(float _force, Transform _tm);
	
		bool CanLaunch();
	
	}
}