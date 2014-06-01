using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PauseMenu : MonoBehaviour
{
	
	public GUISkin skin;
	private float gldepth = -0.5f;
	private float startTime = 0.1f;
	public Material mat;
	private float savedTimeScale;
	//private SepiaToneEffect pauseFilter;
	public GameObject start;
	public string url = "unity.html";
	
	public enum Page
	{
		None,
		Main,
		Options
	}
	
	private Page currentPage;
	private float[] fpsarray;
	private float fps;
	private int toolbarInt = 0;
	private string[]  toolbarstrings = {"Audio","Graphics"};
	
	void Start ()
	{
		fpsarray = new float[Screen.width];
		Time.timeScale = 1;
		//pauseFilter = Camera.main.GetComponent<SepiaToneEffect> ();
		PauseGame ();
	}
	
	static bool IsDashboard ()
	{
		return Application.platform == RuntimePlatform.OSXDashboardPlayer;
	}
	
	static bool IsBrowser ()
	{
		return (Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer);
	}
	
	void LateUpdate (){
		if (Input.GetKeyDown ("escape")) {
			switch (currentPage) {
			case Page.None: 
				PauseGame (); 
				break;
				
			case Page.Main: 
				if (!IsBeginning ()) 
					UnPauseGame (); 
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}
	
	void OnGUI ()
	{
		if (skin != null) {
			GUI.skin = skin;
		}
		if (IsGamePaused ()) {
			switch (currentPage) {
			case Page.Main:
				MainPauseMenu ();
				break;
			}
		}   
	}
	
	void ShowBackButton ()
	{
		if (GUI.Button (new Rect (20, Screen.height - 50, 50, 20), "Back")) {
			currentPage = Page.Main;
		}
	}
	
	void QualityControl ()
	{
		GUILayout.BeginHorizontal ();
		int originalLevel = QualitySettings.GetQualityLevel();
		int newLevel = GUILayout.Toolbar( originalLevel, QualitySettings.names);
		if( originalLevel != newLevel){
			QualitySettings.SetQualityLevel(newLevel);
		}
		GUILayout.EndHorizontal ();
	}
	
	void VolumeControl ()
	{
		GUILayout.Label ("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider (AudioListener.volume, 0, 1);
	}
	
	void BeginPage (int width, int height)
	{
		GUILayout.BeginArea (new Rect ((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage ()
	{
		GUILayout.EndArea ();
		if (currentPage != Page.Main) {
			ShowBackButton ();
		}
	}
	
	bool IsBeginning ()
	{
		return (Time.time < startTime);
	}
	
	void MainPauseMenu ()
	{
		BeginPage (200, 200);
		if (GUILayout.Button (IsBeginning () ? "Play" : "Continue")) {
			UnPauseGame ();
			
		}
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
		}
		EndPage ();
	}
	
	void PauseGame ()
	{
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		//if (pauseFilter) 
		//	pauseFilter.enabled = true;
		currentPage = Page.Main;
	}
	
	void UnPauseGame ()
	{
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		//if (pauseFilter) 
		//	pauseFilter.enabled = false;
		
		currentPage = Page.None;
		
		if (IsBeginning () && start != null) {
			start.SetActive(true);
		}
	}
	
	bool IsGamePaused ()
	{
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause (bool pause)
	{
		if (IsGamePaused ()) {
			AudioListener.pause = true;
		}
	}

}