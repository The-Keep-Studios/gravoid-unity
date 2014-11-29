using System;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS{

	public interface ICUBSConfiguration{

		PartSelectionBehavior getHeadComponent();
		
		List<PartSelectionBehavior> getBodyComponents();
		
		PartSelectionBehavior getTailComponent();

		List<PartSelectionBehavior> getPartSelectionList();

	}
}

