using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public string levelToLoadAfterPlayingIsFinished;

	public bool IsDonePlaying {
		get {
			//no playing logic yet, so we are ALWAY done playing
			return true;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(IsDonePlaying){
			CloseSplashScreen();
		}
	}

	private void CloseSplashScreen(){
		Application.LoadLevel(levelToLoadAfterPlayingIsFinished);
		Destroy(this); //just in case remove the splash screen behavior completely
	}


	
}
