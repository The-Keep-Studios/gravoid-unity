using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TheKeepStudios.Gravoid.CUBS.Ballistics;

// This behavior will require a component prefab and thus generate the look of the corresponding Ballistic Component Button in the CUBS window 
// TODO Also make this script update the name of the Game Object its self (ex. Mass Component Button)

namespace TheKeepStudios.Gravoid.CUBS{
	public class BallisticComponentButton : MonoBehaviour{
		[SerializeField]
		private PartSelectionBehavior
			partInfo;
		public Image partIcon;
		public Text buttonLabel;

		public PartSelectionBehavior PartInfo{
			get{
				return partInfo;
			}
			set{
				partInfo = value;
				buttonLabel.text = PartInfo.Title;
				partIcon.sprite = PartInfo.Icon;
			}
		}

		void Start(){

			PartInfo = partInfo;
		}

	}





}


