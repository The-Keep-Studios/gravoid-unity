using System;

namespace TheKeepStudios.events.eventArgs{
	public class IntValueChageEventArgs : System.EventArgs {
		public readonly int oldValue;
		public readonly int newValue;
		public IntValueChageEventArgs (int oldValue, int newValue)
		{
			this.oldValue = oldValue;
			this.newValue = newValue;
		}
	}
}

