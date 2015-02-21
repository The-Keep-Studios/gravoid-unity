using UnityEngine;
using System.Collections;
using TheKeepStudios.events.producers;

namespace TheKeepStudios.menu.widgets{
	public abstract class Widget  : EventProducer
	{
		[UnityEngine.SerializeField]
		private string label;
		
		public string Label {
			get { return label; }
			set { label = value; }
		}
		abstract  public void Draw ();
	}
}

