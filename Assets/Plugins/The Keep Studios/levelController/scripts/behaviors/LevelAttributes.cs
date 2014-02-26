using UnityEngine;
using System.Collections;

public class LevelAttributes : MonoBehaviour{
	
	public Vector3 zoneDimensions;
	
	// Size of the level
	//public Rect bounds;

	// Sea Green For the Win!
	private Color sceneViewDisplayColor = new Color (0.20f, 0.74f, 0.27f, 0.50f);
	
	static private LevelAttributes instance;

	public static LevelAttributes  GetInstance ()
	{
		if (!LevelAttributes.instance) {
			LevelAttributes.instance = (LevelAttributes)FindObjectOfType(typeof(LevelAttributes));
			if (!LevelAttributes.instance) {
				Debug.LogError ("There needs to be one active LevelAttributes script on a GameObject in your scene.");
			}
		}
		return instance;
	}

	public void  OnDisable ()
	{
		instance = null;
	}

	public void  OnDrawGizmos ()
	{
		Gizmos.color = sceneViewDisplayColor;
		/*
		Vector3 lowerLeft = new Vector3 (bounds.xMin, bounds.yMax, 0);
		Vector3 upperLeft = new Vector3 (bounds.xMin, bounds.yMin, 0);
		Vector3 lowerRight = new Vector3 (bounds.xMax, bounds.yMax, 0);
		Vector3 upperRight = new Vector3 (bounds.xMax, bounds.yMin, 0);
		Gizmos.DrawLine (lowerLeft, upperLeft);
		Gizmos.DrawLine (upperLeft, upperRight);
		Gizmos.DrawLine (upperRight, lowerRight);
		Gizmos.DrawLine (lowerRight, lowerLeft);
		*/
	}

	public void  Start ()
	{/*
		GameObject createdBoundaries = new GameObject ("Created Boundaries");
		createdBoundaries.transform.parent = transform;
		
		Vector3 size = new Vector3 (colliderThickness, bounds.height + colliderThickness * 2.0f + fallOutBuffer, colliderThickness);
		Vector3 center = new Vector3 (bounds.xMin - colliderThickness * 0.5f, bounds.y + bounds.height * 0.5f - fallOutBuffer * 0.5f, 0.0f);
		this.makeBoundry("Left Boundary", createdBoundaries.transform, size, center);
		
		size = new Vector3 (colliderThickness, bounds.height + colliderThickness * 2.0f + fallOutBuffer, colliderThickness);
		center = new Vector3 (bounds.xMax + colliderThickness * 0.5f, bounds.y + bounds.height * 0.5f - fallOutBuffer * 0.5f, 0.0f);
		this.makeBoundry("Right Boundary", createdBoundaries.transform, size, center);
		
		size = new Vector3 (bounds.width + colliderThickness * 2.0f, colliderThickness, colliderThickness);
		center = new Vector3 (bounds.x + bounds.width * 0.5f, bounds.yMax + colliderThickness * 0.5f, 0.0f);
		this.makeBoundry("Top Boundary", createdBoundaries.transform, size, center);
		
		size = new Vector3 (bounds.width + colliderThickness * 2.0f, colliderThickness, colliderThickness);
		center = new Vector3 (bounds.x + bounds.width * 0.5f, bounds.yMin - colliderThickness * 0.5f - fallOutBuffer, 0.0f);
		this.makeBoundry("Bottom Boundary", createdBoundaries.transform, size, center);
		*/
	}
	
	
	private void makeBoundry(string _name, Transform _parent, Vector3 _size, Vector3 _width){
		GameObject boundary = new GameObject (_name);
		boundary.transform.parent = _parent;
		BoxCollider boxCollider = boundary.AddComponent<BoxCollider>();
		boxCollider.size = _size;
		boxCollider.center = _width;

	}

}