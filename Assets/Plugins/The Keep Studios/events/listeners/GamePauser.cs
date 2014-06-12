using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners
{
	public class GamePauser : EventListener
	{
		
		public enum PauserBehavior
		{
			Toggle,
			Pause,
			Unpause
		}

		private static float savedTimeScale;
		private static bool paused = false;
		public PauserBehavior behavior;
		
		override public EventHandler OnEvent {
			get {
				return RequestPauseChange;
			}
		}

		private void RequestPauseChange (object requestSource, EventArgs e)
		{
			switch (behavior) {
			case PauserBehavior.Toggle:
				TogglePause ();
				break;
			case PauserBehavior.Pause:
				Pause ();
				break;
			case PauserBehavior.Unpause:
				Unpause ();
				break;
			default:
				throw new InvalidOperationException ("");
			}
		}
		
		private void TogglePause ()
		{
			if (paused) {
				this.Unpause ();
			} else {
				this.Pause ();
			}
		}
		
		private void Pause ()
		{
			if (!paused) {
				savedTimeScale = Time.timeScale;
				Time.timeScale = 0;
				AudioListener.pause = true;
				paused = true;
			} else {
				Debug.LogWarning ("Attempted to pause when the current state is already paused.", this);
			}
		}
		
		private void Unpause ()
		{
			if (paused) {
				Time.timeScale = savedTimeScale;
				AudioListener.pause = false;
				paused = false;
			} else {
				Debug.LogWarning ("Attempted to unpause when the current state is not paused.", this);
			}
		}
	}
}