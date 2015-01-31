using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSConfigurationFlyoutWidget : MonoBehaviour{

		[SerializeField]
		private Text
			partName;
			
		[SerializeField]
		private  Text
			partMechanics;
			
		[SerializeField]
		private  Text
			partFlavor;
			
		[SerializeField]
		private  PartSelectionBehavior
			currentlyDisplayedPart;

		public void ChangeDisplayedPart(PartSelectionBehavior partInformationToDisplay){
			partName.text = partInformationToDisplay.Title;
			partMechanics.text = partInformationToDisplay.Description;
			partFlavor.text = partInformationToDisplay.Flavor;
			currentlyDisplayedPart = partInformationToDisplay;
		}
	}
}