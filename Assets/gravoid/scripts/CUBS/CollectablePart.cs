using UnityEngine;
using System.Collections;
using PathologicalGames;

namespace TheKeepStudios.Gravoid.CUBS{
	public class CollectablePart : CollectableResource{

		public PartSelectionBehavior part;

		public SpawnPool origin;

		public void OnCollected(){
			Despawn();
		}

		public void Despawn(){
			origin.Despawn(this.gameObject.transform);
		}

		void OnSpawned(SpawnPool pool){
			this.origin = pool;
		}

	}

}
