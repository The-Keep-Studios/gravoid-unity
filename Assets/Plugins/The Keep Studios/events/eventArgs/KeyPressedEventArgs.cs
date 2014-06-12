using System;
namespace TheKeepStudios.events.eventArgs{
	public class KeyPressedEventArgs : System.EventArgs	{
		public readonly string keyName;
		public KeyPressedEventArgs (string keyName){
			this.keyName = keyName;
		}
	}
}

