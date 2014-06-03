using UnityEngine;
using System;
using System.Collections;

namespace TheKeepStudios.menu.widgets{
	public class GraphicalQualitySelectionBox : ToolBox {

		void Start(){
			this.options = QualitySettings.names;
			RegisterListener(ChangeQualityLevel);
		}

		override public void Draw (){
			this.currentSelected = QualitySettings.GetQualityLevel();
			base.Draw();
		}
		
		private void ChangeQualityLevel(object source, EventArgs e){
			//not pretty, but this is such a MINOR performance hit compared to changing the quality level that I'm not worrying about it right now.
			events.eventArgs.IntValueChageEventArgs myArgs = (events.eventArgs.IntValueChageEventArgs) e;
			QualitySettings.SetQualityLevel(myArgs.newValue);
		}
	}
}

