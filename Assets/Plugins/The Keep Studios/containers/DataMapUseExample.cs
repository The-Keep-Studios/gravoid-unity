using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DataMapUseExample : MonoBehaviour
{
	[SerializeField]
	private Map exampleMap;
	//public List<Link> compare1;
	//public List<int> compare2;

	// Use this for initialization
	void Start ()
	{
		GameObject obj = new GameObject();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	//INTERNAL CLASSES
	[System.Serializable]
	internal class Map: DataMap<string, int, Link> { }
	
	[System.Serializable]
	public class Link: Link<string,int> { 
		
		public string m_key;
		
		public int m_data;
		
		public override string getKey(){ return this.m_key; }
	
		public override void setKey(string _key){ this.m_key = _key; }
		
		public override int getData(){ return this.m_data; }
		
		public override void setData(int _data){ this.m_data = _data; }
	}
	
}
