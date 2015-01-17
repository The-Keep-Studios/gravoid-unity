using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSPartDisplayWidget : UnityEngine.UI.Button{

		[SerializeField]
		private PartSelectionBehavior
			currentPart;
		[SerializeField]
		private UnityEngine.UI.Text
			partNameText;
		[SerializeField]
		private UnityEngine.UI.Image
			partIcon;
		[SerializeField]
		private int
			currentPosition;
		
		public PartSelectionBehavior Part{
			get{ return currentPart;} 
			set{
				if(currentPart == null){
					currentPart = null;
					if(partIcon != null){
						partIcon.sprite = null;
					}
					if(partNameText != null){
						partNameText.text = null;
					}
					this.gameObject.SetActive(false);
				} else{
					currentPart = value;
					if(partIcon != null){
						partIcon.sprite = currentPart.Icon;
					}
					if(partNameText != null){
						partNameText.text = currentPart.name;
					}
					this.gameObject.SetActive(true);
				}
			}
		}

		void Start(){
			partIcon = (partIcon == null) ? (GetComponentInChildren<UnityEngine.UI.Image>()) : (partIcon);
			partNameText = (partNameText == null) ? (GetComponentInChildren<UnityEngine.UI.Text>()) : (partNameText);
			Part = Part; //initializes the current part according to it's current value
		}
		
		public void ActivateConfigurationSelector(){
			throw new NotImplementedException("Required for completion of GRA-335");
		}
		
	}
}
