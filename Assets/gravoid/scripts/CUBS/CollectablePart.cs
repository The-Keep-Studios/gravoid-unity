using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid
{
	public class CollectablePart : CollectableResource
	{

		public PartSelectionBehavior part;

		public void OnCollected(){
			Despawn();
		}

		public void Despawn(){
			Destroy(this.gameObject);
		}

	}

}
