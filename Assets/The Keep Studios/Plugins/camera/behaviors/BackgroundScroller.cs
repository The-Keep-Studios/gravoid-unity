using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	
	public float scrollRate;
	public Transform scrollFollower;
	private Vector3 lastPosition;

	// Use this for initialization
	virtual protected void Start (){
		lastPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		
		//calculate our tile scroll change		
		Scroll((scrollFollower.position - lastPosition) * scrollRate * -1);

		//store the position for the next update
		lastPosition = scrollFollower.position;
		
	}
	
	/// <summary>
	/// Scroll the by the specified scrollChange.
	/// </summary>
	/// <param name='scrollChange'>
	/// Scroll change amount, in world units.
	/// </param>
	virtual protected void Scroll(Vector2 scrollChange){
		
		this.transform.position = new Vector3(scrollChange.x, scrollChange.y);
		
	}
	
}
