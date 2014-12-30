using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Inventory{

	[RequireComponent(typeof(TheKeepStudios.spawning.Spawned))]
	public class CollectableResource : CollectableItem{
		public void OnCollected(){
			GetComponent<TheKeepStudios.spawning.Spawned>().Despawn();
		}
	}
}
