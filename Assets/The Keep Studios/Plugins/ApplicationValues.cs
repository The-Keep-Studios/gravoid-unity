using UnityEngine;
using System.Collections;

namespace TheKeepStudios{
	public class ApplicationValues{

		//FIXME this value should come from an editor script
		private static string name = "Gravoid";
		
		//FIXME this value should come from an editor script
		private static string version = "0.1.0";

		public static string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		public static string Version {
			get {
				return version;
			}
			set {
				version = value;
			}
		}
	}
}

