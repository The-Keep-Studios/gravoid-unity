using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid
{
	public class CollectableResource : CollectableItem
	{
		void OnCollected()
		{
			Destroy(gameObject);
		}
	}
}
