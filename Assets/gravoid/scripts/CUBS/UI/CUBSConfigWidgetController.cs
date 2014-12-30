using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSConfigWidgetController : MonoBehaviour{

		public CUBSPartSelectionWidget selectionDisplayWidget;

		public void OnChangeConfiguration(Ballistics.IProjectileConfiguration config){
			selectionDisplayWidget.SetSelection(config);
		}

	}
}