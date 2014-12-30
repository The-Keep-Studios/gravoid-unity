using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSPartSelectionWidget : MonoBehaviour{

		public CUBSPartDisplayWidget headWidget;

		public CUBSBodyPartsWidget bodyWidget;

		public CUBSPartDisplayWidget tailWidget;

		public void SetSelection(Ballistics.IProjectileConfiguration config){
			headWidget.setPart(config.Head);
			bodyWidget.setParts(config.Body);
			tailWidget.setPart(config.Tail);
		}

	}

}
