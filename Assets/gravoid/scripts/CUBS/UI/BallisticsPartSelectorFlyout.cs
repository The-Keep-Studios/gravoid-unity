using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS {
	public class BallisticsPartSelectorFlyout : MonoBehaviour {

		public Text partName;
		public Text partMechanics;
		public Text partFlavor;

		public void PartSelect(PartSelectionBehavior partInfo){
			partName.text = partInfo.Title;
			partMechanics.text = partInfo.Description;
			partFlavor.text = partInfo.Flavor;

		}

	}
}