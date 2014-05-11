using UnityEngine;
using System.Collections;
namespace TheKeepStudios.menu.widgets{
	public abstract class Widget  : events.EventProducer
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

