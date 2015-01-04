using UnityEngine;
using System.Collections;

// This behavior will contain all of the sounds associated with the menu and control playing them.

public class MenuSounds : MonoBehaviour {

	[SerializeField]
	private Map soundMap;

	//public List<Link> compare1;
	//public List<int> compare2;1
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	//INTERNAL CLASSES
	[System.Serializable]
	internal class Map: DataMap<string, AudioClip, Link> { }
	
	[System.Serializable]
	public class Link: Link<string,AudioClip> { 
		
		public string m_key;
		
		public AudioClip m_data;
		
		public override string getKey(){ return this.m_key; }
		
		public override void setKey(string _key){ this.m_key = _key; }
		
		public override AudioClip getData(){ return this.m_data; }
		
		public override void setData(AudioClip _data){ this.m_data = _data; }
	}

	public void PlaySound(string soundName)
	{
		AudioSource radio;
		AudioClip sound = soundMap.getData(soundName);
		radio = GetComponent<AudioSource>();
		radio.clip = sound;
		radio.Play ();
	}


}


