using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheKeepStudios.Gravoid.CUBS.UI{

	public class CUBSConfigCostWidget : MonoBehaviour{

		[SerializeField]
		private string
			costLabel;

		[SerializeField]
		private string
			reloadTimeLabel;

		[SerializeField]
		private UnityEngine.UI.Text
			text1;

		[SerializeField]
		private UnityEngine.UI.Text
			text2;
		
		[SerializeField]
		private Ballistics.IProjectileConfiguration
			currentConfig;

		public string CostLabel{
			get{
				return costLabel;
			}
			set{
				costLabel = value;
			}
		}

		public string ReloadTimeLabel{
			get{
				return reloadTimeLabel;
			}
			set{
				reloadTimeLabel = value;
			}
		}
		
		public Ballistics.IProjectileConfiguration CurrentConfig{
			get{
				return currentConfig;
			}
			set{
				currentConfig = value;
				UpdateConfigDisplay();
			}
		}

		void Start(){
			//Warning: Assumes exactly two UnityEngine.UI.Text components in children
			text1 = text1 != null ? text1 : gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
			text2 = text2 != null ? text2 : gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
		}

		public void OnChangeConfiguration(Ballistics.IProjectileConfiguration config){
			CurrentConfig = config;
		}

		private void UpdateConfigDisplay(){
			text1.text = this.CostLabel + CurrentConfig.Cost;
			text2.text = this.ReloadTimeLabel + CurrentConfig.ReloadTime; 
		}

	}

}
