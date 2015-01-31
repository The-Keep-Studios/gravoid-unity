using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TheKeepStudios.Gravoid.CUBS.Ballistics;



namespace TheKeepStudios.Gravoid.CUBS.UI {
	public class BallisticsInfoChanger : MonoBehaviour {

		public Text partName;
		public Text partMechanics;
		public Text partFlavor;
		public CUBSPartDisplayWidget displayToChange;

		public void PartSelect(PartSelectionBehavior partInfo){
			partName.text = partInfo.Title;
			partMechanics.text = partInfo.Description;
			partFlavor.text = partInfo.Flavor;

			displayToChange.Part = partInfo;

		}

		public void PartChangeRequestEventHandler(object sender, CUBSPartDisplayWidget.PartChangeRequestEventArgs e){
			displayToChange = e.partWidget;
		}

		public void OnPartFinalization(){

		}

	}
}