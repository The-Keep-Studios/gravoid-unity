using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS{
	public class CollectableResource : CollectableItem{
		void OnCollected(){
			Destroy(gameObject);
		}
	}
}
