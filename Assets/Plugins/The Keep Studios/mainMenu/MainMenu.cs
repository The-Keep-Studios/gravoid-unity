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
	GUISkin customSkin1;
	GUISkin customSkin2;
	GUISkin customSkin3;

	void  Awake ()
	{
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

	void  OnGUI ()
	{
		GUI.skin = customSkin1;
		//Title Banner
		GUI.Box (new Rect (0, 0, topBannerW, topBannerH), exampleVar1);
	
		GUI.skin = customSkin2;
		//Button 1
		if (GUI.Button (new Rect (0, buttonPos1, buttonSizeW, buttonSizeH), "Math - Traditional")) {
			Debug.Log ("Clicked the button Math - Traditional");
		}
		//Button 2
		if (GUI.Button (new Rect (0, buttonPos2, buttonSizeW, buttonSizeH), "Math - Integrated")) {
			Debug.Log ("Clicked the button Math - Integrated");
		}
		//Button 3
		if (GUI.Button (new Rect (0, buttonPos3, buttonSizeW, buttonSizeH), "Language Arts")) {
			Debug.Log ("Clicked the button Language Arts");
		}
		//Button 4
		if (GUI.Button (new Rect (0, buttonPos4, buttonSizeW, buttonSizeH), "History/Social Studies")) {
			Debug.Log ("Clicked the button History/Social Studiess");
		}
		//Button 5
		if (GUI.Button (new Rect (0, buttonPos5, buttonSizeW, buttonSizeH), "Science & Technical Subjects")) {
			Debug.Log ("Clicked the button Science & Technical Subjects");
		}
	
		GUI.skin = customSkin3;
		//Bottom Banner
		GUI.Box (new Rect (0, bottomBannerPos, bottomBannerW, bottomBannerH), "we can place advertisements, links\nwhatever we want here.");
	}
}