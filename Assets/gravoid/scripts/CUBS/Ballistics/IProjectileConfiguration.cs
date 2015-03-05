using System;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public interface IProjectileConfiguration{

		PartSelectionBehavior Head{ get; set; }
		
		List<PartSelectionBehavior> Body{ get; set; }
		
		PartSelectionBehavior Tail{ get; set; }

		List<PartSelectionBehavior> Parts{ get; set; }
		
		int Count{ get; }

		//TODO make better representation of cost than a string
		string Cost{ get; }

		//TODO make better representation of reload time than a string
		string ReloadTime{ get; }

		void Copy(IProjectileConfiguration configToCopy);

		void Add(PartSelectionBehavior part);

	}
}

