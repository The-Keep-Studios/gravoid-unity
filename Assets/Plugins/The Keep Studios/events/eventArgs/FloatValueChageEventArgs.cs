using System;

namespace TheKeepStudios.events.eventArgs{
	public class FloatValueChageEventArgs : System.EventArgs
	{
		public readonly float oldValue;
		public readonly float newValue;
		public FloatValueChageEventArgs (float oldValue, float newValue)
		{
			this.oldValue = oldValue;
			this.newValue = newValue;
		}
	}
}

