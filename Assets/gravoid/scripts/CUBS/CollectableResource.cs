using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid
{
	public class CollectableResource : TheKeepStudios.CollectableItem
	{
		void OnCollected()
		{
			Destroy(gameObject);
		}
	}
}
