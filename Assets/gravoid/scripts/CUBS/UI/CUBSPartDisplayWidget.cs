using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSPartDisplayWidget : MonoBehaviour{
	
		[Serializable]
		public class PartChangeRequestEventArgs{
			CUBSPartDisplayWidget partWidget;
		}
	
		[Serializable]
		public delegate void PartChangeRequestEventHandler(object sender,PartChangeRequestEventArgs e);

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
			
		[SerializeField]
		private event PartChangeRequestEventHandler
			pcrEvent;
			
		private PartChangeRequestEventArgs args = new PartChangeRequestEventArgs();
		
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
		
		public virtual void OnPartChangeRequest(){
			if(pcrEvent != null){
				pcrEvent(this, args);
			}
		}
		
		virtual public void RegisterPartChangeRequestListener(PartChangeRequestEventHandler onEvent){
			pcrEvent += onEvent;
		}
		
		virtual public void DeregisterPartChangeRequestListener(PartChangeRequestEventHandler onEvent){
			pcrEvent -= onEvent;
		}
		
	}
	
}
