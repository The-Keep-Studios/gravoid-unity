using UnityEngine;
using System.Collections;

abstract public class Zoom : MonoBehaviour
{
	
	public float zoomFactoMin = 0.0f;
	public float zoomFactoMax = 0.0f;
	public string zoomButtonName;
	public string zoomAxisName;
	public float zoomRate;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		ChangeZoom ();
		
	}
	
	void Reset ()
	{
		
		zoomFactoMin = 0.0f;
		
		zoomFactoMax = 0.0f;
		
	}
	
	void ChangeZoom ()
	{
		
		float zoomAxisChange = Input.GetAxisRaw (zoomAxisName) * this.zoomRate;

		//clamp the target zoom
		float zoomChange = Mathf.Clamp (zoomAxisChange, -1 * this.zoomRate, this.zoomRate);

		ChangeZoom (zoomChange);
		
	}
	
	public abstract void ChangeZoom (float zoomChange);
	
}
