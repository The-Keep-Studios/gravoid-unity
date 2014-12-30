using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	[RequireComponent(typeof(ProjectilePartBehavior))]
	public class PartSelectionBehavior : MonoBehaviour{

		[SerializeField]
		private Sprite
			icon;
		
		public ProjectilePartBehavior ProjectilePartPrefab{
			get{
				return this.GetComponent<ProjectilePartBehavior>();
			}
		}

		public Sprite Icon{
			get{
				return icon;
			}
		}

	}
}
