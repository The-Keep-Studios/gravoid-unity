using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	[RequireComponent(typeof(CUBPart))]
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
		
		public CUBPart ProjectilePartPrefab{
			get{
				return this.GetComponent<CUBPart>();
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
