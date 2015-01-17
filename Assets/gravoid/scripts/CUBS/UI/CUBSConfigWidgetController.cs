using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSConfigWidgetController : MonoBehaviour{
	
		[SerializeField]
		private CUBSPartDisplayWidget
			partDisplayButtonPrefab;
		
		public float widgetCenterDistance;
		
		void Start(){
			if(partDisplayButtonPrefab == null){
				throw new Exception("Invalid setup, partDisplayButtonPrefab cannot be null");
			}
		}

		public void OnChangeConfiguration(Ballistics.IProjectileConfiguration config){
			SetSelection(config);
		}
		
		public void SetSelection(Ballistics.IProjectileConfiguration config){
			List<CUBSPartDisplayWidget> widgets = new List<CUBSPartDisplayWidget>(GetComponentsInChildren<CUBSPartDisplayWidget>());
			SetHeight(config.Parts.Count);
			bool isWidgetLayoutChanged = false;
			//add any extra widgets we may need
			while(config.Parts.Count > widgets.Count){
				widgets.Add(ObjectPool.Spawn(partDisplayButtonPrefab));
			}
			//set the parts
			for(int idx = 0; idx<=config.Parts.Count; ++idx){
				CUBSPartDisplayWidget nextWidget = widgets[idx];
				nextWidget.Part = config.Parts[idx];
			}
			//clear unused widgets of thier current part
			for(int idx = config.Parts.Count; idx<=widgets.Count; ++idx){
				widgets[idx].Part = null;
			}
			if(isWidgetLayoutChanged){
				UpdateLayout();
			}
			SetHeight(config.Parts.Count);
		}
		
		private void UpdateLayout(){
			throw new NotImplementedException();
		}
		
		private void SetHeight(int numberOfParts){
			RectTransform rt = GetComponent<RectTransform>();
			throw new NotImplementedException("Required for completion of GRA-306");
		}

	}
}