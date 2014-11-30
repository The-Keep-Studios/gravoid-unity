using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS{
	public class CUBSPartDisplayWidget : MonoBehaviour{

		UnityEngine.UI.Text text;
		UnityEngine.UI.Image image;

		void Start(){
			//Warning: Assumes one and only one of each of these components in our children and that they will remain constant
			text = text != null ? text : gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
			image = image != null ? image : gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
		}

		public void setPart(PartSelectionBehavior partSelectionBehavior){
			// TODO get name of part in a more planned fashion (this is just the name of the object I believe
			text.text = partSelectionBehavior.name;
			image.sprite = partSelectionBehavior.Icon;
		}

	}
}
