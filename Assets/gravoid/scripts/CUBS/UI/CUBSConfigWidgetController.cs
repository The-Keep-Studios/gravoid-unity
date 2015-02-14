using UnityEngine;
using System.Collections.Generic;
using System;
using TheKeepStudios.Gravoid.CUBS.Ballistics;


namespace TheKeepStudios.Gravoid.CUBS.UI{
	public class CUBSConfigWidgetController : ConfigurationSelectorBehavior{
	
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
		
		[SerializeField]
		CUBSConfigurationFlyoutWidget
			configurationAltertionWidget;
		
		[SerializeField]
		private  CUBSPartDisplayWidget
			partToChange;

		virtual public Ballistics.IProjectileConfiguration Configuration{
			get{
				return base.Configuration;
			}
			set{
				base.Configuration = value;
				SetSelection(Configuration);
			}
		}

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
		
		public void ChangeCurrentPart(){
			PartSelectionBehavior newPartSelection = null;
			if(newPartSelection == null){
				Debug.LogWarning("No part selection specified. Assuming the configurationAltertionWidget's CurrentlyDisplayedPart was intended.");
				partToChange.Part = configurationAltertionWidget.CurrentlyDisplayedPart;
			} else{
				partToChange.Part = newPartSelection;
			}
			//TODO Create a new Ballistics.IProjectileConfiguration
			//TODO Send the new Ballistics.IProjectileConfiguration to the ConfigurationSelectorBehavior
			configurationAltertionWidget.gameObject.SetActive(false);
		}

		public void OnChangeConfiguration(Ballistics.IProjectileConfiguration config){
			Configuration = config;
		}
		
		private void SetSelection(Ballistics.IProjectileConfiguration config){
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
			for(int idx = 0; idx < widgets.Count; ++idx){
				CUBSPartDisplayWidget nextWidget = widgets[idx];
				RectTransform rt = nextWidget.transform as RectTransform;
				rt.SetParent(widgetContainerTranform);
				float inset = (idx * widgetHeight) + borderHeight;
				rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, inset, widgetHeight);
				nextWidget.RegisterPartChangeRequestListener(this.PartChangeRequestEventHandler);
			}
		}
		
		private void SetHeight(int numberOfParts){
			Vector2 sizeDelta = widgetContainerTranform.sizeDelta;
			//our RectTransform must fit each widget vertically in the 
			sizeDelta.y = (widgetHeight * numberOfParts) + (borderHeight * 2);
			widgetContainerTranform.sizeDelta = sizeDelta;
		}
		
		private void PartChangeRequestEventHandler(object sender, CUBSPartDisplayWidget.PartChangeRequestEventArgs e){
			configurationAltertionWidget.gameObject.SetActive(true);
			partToChange = e.partWidget;
		}

	}
}