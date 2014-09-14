using UnityEngine;
using System.Collections;

namespace TheKeepStudios{

	public class SceneLoader : MonoBehaviour{
		
		public void LoadScene(string sceneToLoad){
			Application.LoadLevel(sceneToLoad);
		}
	}
}
