using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS{

	public class PartSelectionBehavior : MonoBehaviour{

		[SerializeField]
		private ProjectilePartBehavior
			partPrefab;

		[SerializeField]
		private Sprite
			icon;
		
		public ProjectilePartBehavior ProjectilePartPrefab{
			get{
				return partPrefab;
			}
		}

		public Sprite Icon{
			get{
				return icon;
			}
		}

		static public ProjectilePartBehavior GetPartPrefabFromSelection(PartSelectionBehavior _selection){
			return _selection.ProjectilePartPrefab;
		}

	}
}
