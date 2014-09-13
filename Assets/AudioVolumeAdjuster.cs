using UnityEngine;
using System.Collections;

public class AudioVolumeAdjuster : MonoBehaviour{

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
	
	}

	public void ChangeMasterAudioVolumn(float newLevel){
		AudioListener.volume = newLevel;
	}
}
