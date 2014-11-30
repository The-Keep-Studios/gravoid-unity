using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS{
	public class CUBSPartSelectionWidget : MonoBehaviour{

		public CUBSPartDisplayWidget headWidget;

		public CUBSBodyPartsWidget bodyWidget;

		public CUBSPartDisplayWidget tailWidget;

		public void SetSelection(ICUBSConfiguration config){
			headWidget.setPart(config.getHeadComponent());
			bodyWidget.setParts(config.getBodyComponents());
			tailWidget.setPart(config.getTailComponent());
		}

	}

}
