using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	//Top Banner
	float topBannerH;
	float topBannerW;

	//Buttons
	float buttonSizeH;
	float buttonSizeW;
	float buttonPos1;
	float buttonPos2;
	float buttonPos3;
	float buttonPos4;
	float buttonPos5;

	//Bottom Banner
	float bottomBannerH;
	float bottomBannerW;
	float bottomBannerPos;
	string exampleVar1;
	public GUISkin customSkin1;
	public GUISkin customSkin2;
	public GUISkin customSkin3;
	
	float lastScreenHeight;
	float lastScreenWidth;

	void  Awake (){
		UpdateMenuDimensions ();
	}

	void  OnGUI (){
		UpdateMenuDimensions ();
		Draw ();
	}
	
	void UpdateMenuDimensions (){
		if(lastScreenHeight != Screen.height || lastScreenWidth != Screen.width){
			lastScreenHeight = Screen.height;
			lastScreenWidth = Screen.width;
			topBannerH = Screen.height / 4;
			topBannerW = Screen.width;
			buttonSizeH = Screen.height / 10;
			buttonSizeW = Screen.width;
			buttonPos1 = topBannerH;
			buttonPos2 = topBannerH + buttonSizeH;
			buttonPos3 = topBannerH + buttonSizeH * 2;
			buttonPos4 = topBannerH + buttonSizeH * 3;
			buttonPos5 = topBannerH + buttonSizeH * 4;
			bottomBannerH = Screen.height / 4;
			bottomBannerW = Screen.width;
			bottomBannerPos = topBannerH + buttonSizeH * 5;
		}
	}

	void Draw (){
		GUI.skin = customSkin1;
		//Title Banner
		GUI.Box (new Rect (0, 0, topBannerW, topBannerH), exampleVar1);
		GUI.skin = customSkin2;
		//Button 1
		if (GUI.Button (new Rect (0, buttonPos1, buttonSizeW, buttonSizeH), "New Game Button")) {
			Debug.Log ("Clicked the button New Game Button");
		}
		//Button 2
		if (GUI.Button (new Rect (0, buttonPos2, buttonSizeW, buttonSizeH), "Load Game Button")) {
			Debug.Log ("Clicked the button Load Game Button");
		}
		//Button 3
		if (GUI.Button (new Rect (0, buttonPos3, buttonSizeW, buttonSizeH), "Options Button")) {
			Debug.Log ("Clicked the button Options Button");
		}
		//Button 4
		if (GUI.Button (new Rect (0, buttonPos4, buttonSizeW, buttonSizeH), "Credits Button")) {
			Debug.Log ("Clicked the button Credits Buttons");
		}
		//Button 5
		if (GUI.Button (new Rect (0, buttonPos5, buttonSizeW, buttonSizeH), "Exit Game Button")) {
			Debug.Log ("Clicked the button Exit Game Button");
		}
		GUI.skin = customSkin3;
		//Bottom Banner
		GUI.Box (new Rect (0, bottomBannerPos, bottomBannerW, bottomBannerH), "we can place news, links\nwhatever we want here.");
	}
}