using UnityEngine;
using System.Collections;

public class CollisionSound : MonoBehaviour {

	[SerializeField]
	 private AudioSource
		sound;

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2)
			{ 
			GetComponent<AudioSource>().Play();
			}
		
	}
}