using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	[RequireComponent(typeof(CUBPartBase))]
	public class PartSelectionBehavior : MonoBehaviour{


		[SerializeField]
		private Sprite
			icon;
		
		[SerializeField]
		private string
			title;

		[SerializeField]
		private string
			description;

		[SerializeField]
		private string
			flavor;
		
		public CUBPartBase ProjectilePartPrefab{
			get{
				return this.GetComponent<CUBPartBase>();
			}
		}

		public Sprite Icon{
			get{
				return icon;
			}
			set{
				icon = value; 
			}
		}

		public string Title{
			get{
				return title;
			}
			set{
				title = value;
			}
		}

		public string Description{
			get{
				return description;
			}
			set{
				description = value;
			}
		}

		public string Flavor{
			get{
				return flavor;
			}
			set{
				flavor = value;
			}
		}
	}
}
