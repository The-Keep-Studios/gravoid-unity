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

		public PartSelectionBehavior CurrentlyDisplayedPart{
			get{
				return currentlyDisplayedPart;
			}
			set{
				currentlyDisplayedPart = value;
				partName.text = currentlyDisplayedPart.Title;
				partMechanics.text = currentlyDisplayedPart.Description;
				partFlavor.text = currentlyDisplayedPart.Flavor;
			}
		}

		public void ChangeDisplayedPart(PartSelectionBehavior partInformationToDisplay){
			CurrentlyDisplayedPart = partInformationToDisplay;
		}
	}
}