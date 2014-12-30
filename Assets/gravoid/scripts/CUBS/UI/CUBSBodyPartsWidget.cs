using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSBodyPartsWidget : MonoBehaviour{

		public void setParts(System.Collections.Generic.List<PartSelectionBehavior> list){
			//HACK this should dynamically expand and contract the set of body parts
			if(list.Count != 1){
				throw new System.NotImplementedException("CUBSBodyPartsWidget.setParts does not yet support mody sizes other than 1.");
			} else{
				CUBSPartDisplayWidget display = gameObject.GetComponentInChildren<CUBSPartDisplayWidget>();
				display.setPart(list[0]);
			}
		}

	}
}