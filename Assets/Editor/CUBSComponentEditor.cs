using UnityEngine;
using System.Collections;
using UnityEditor;
// using TheKeepStudios.Gravoid.CUBS.Ballistics;

[CustomEditor(typeof(TheKeepStudios.Gravoid.CUBS.Ballistics.PartSelectionBehavior))]
public class CUBSComponentEditor : Editor {
	public override void OnInspectorGUI()
	{
		TheKeepStudios.Gravoid.CUBS.Ballistics.PartSelectionBehavior myPartSelectionBehavior = (TheKeepStudios.Gravoid.CUBS.Ballistics.PartSelectionBehavior)target;

		myPartSelectionBehavior.Icon = EditorGUILayout.ObjectField("Icon Sprite", myPartSelectionBehavior.Icon, typeof(Sprite), false) as Sprite;
		myPartSelectionBehavior.Title = EditorGUILayout.TextField("Object Name", myPartSelectionBehavior.Title);
		myPartSelectionBehavior.Description = EditorGUILayout.TextField("Object Description", myPartSelectionBehavior.Description);
		myPartSelectionBehavior.Flavor = EditorGUILayout.TextField("Object Flavor Text", myPartSelectionBehavior.Flavor);

		EditorGUILayout.HelpBox("The three text fields will be what is shown in the CUBS widget when the part is selected", MessageType.Info);

	}


}
