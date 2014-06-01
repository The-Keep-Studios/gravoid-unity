using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.listeners {
	
	public class QualityLevelChanger: events.SelfRegisteringEventListener{
		
		override public EventHandler OnEvent {
			get {
				return ChangeQualityLevel;
			}
		}
		
		private void ChangeQualityLevel(object source, EventArgs e){
			//not pretty, but this is such a MINOR performance hit compared to changing the quality level that I'm not worrying about it right now.
			events.IntValueChageEventArgs myArgs = (events.IntValueChageEventArgs) e;
			QualitySettings.SetQualityLevel(myArgs.newValue);
		}
	}
}

