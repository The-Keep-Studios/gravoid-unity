using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSConfigWidgetController : MonoBehaviour{
	
		[SerializeField]
		CUBSPartDisplayWidget
			partDisplayButtonPrefab;
			
		float widgetHeight;
		
		[SerializeField]
		float
			borderHeight;
		
		[SerializeField]
		RectTransform
			widgetContainerTranform;
		
		[SerializeField]
		RectTransform
			widgetPrefabTranform;
		
		void Awake(){
			if(partDisplayButtonPrefab == null){
				Debug.LogWarning("Invalid setup, partDisplayButtonPrefab cannot be null");
			} else{
				widgetPrefabTranform = partDisplayButtonPrefab.transform as RectTransform;
				widgetHeight = widgetPrefabTranform.sizeDelta.y;
			}
			List<CUBSPartDisplayWidget> widgets = new List<CUBSPartDisplayWidget>(GetComponentsInChildren<CUBSPartDisplayWidget>());
			SetHeight(widgets.Count);
			UpdateLayout(widgets);
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
			SetHeight(config.Parts.Count);
			if(isWidgetLayoutChanged){
				UpdateLayout(widgets);
			}
		}
		
		private void UpdateLayout(List<CUBSPartDisplayWidget> widgets){
			for(int idx = 0; idx <= widgets.Count; ++idx){
				CUBSPartDisplayWidget nextWidget = widgets[idx];
				RectTransform rt = nextWidget.transform as RectTransform;
				rt.SetParent(widgetContainerTranform);
				float inset = (idx * widgetHeight) + borderHeight;
				rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, inset, widgetHeight);
			}
		}
		
		private void SetHeight(int numberOfParts){
			Vector2 sizeDelta = widgetContainerTranform.sizeDelta;
			//our RectTransform must fit each widget vertically in the 
			sizeDelta.y = (widgetHeight * numberOfParts) + (borderHeight * 2);
			widgetContainerTranform.sizeDelta = sizeDelta;
		}

	}
}