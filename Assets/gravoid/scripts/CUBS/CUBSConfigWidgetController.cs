using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS{
	public class CUBSConfigWidgetController : MonoBehaviour{

		public CUBSPartSelectionWidget selectionDisplayWidget;

		public void OnChangeConfiguration(ICUBSConfiguration config){
			selectionDisplayWidget.SetSelection(config);
		}

	}
}