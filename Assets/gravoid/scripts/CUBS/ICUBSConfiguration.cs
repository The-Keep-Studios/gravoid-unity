using System;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS{

	public interface ICUBSConfiguration{

		PartSelectionBehavior getHeadComponent();
		
		List<PartSelectionBehavior> getBodyComponents();
		
		PartSelectionBehavior getTailComponent();

		List<PartSelectionBehavior> getPartSelectionList();

		//TODO make better representation of cost than a string
		string GetCost();

		//TODO make better representation of reload time than a string
		string GetReloadTime();
	}
}

