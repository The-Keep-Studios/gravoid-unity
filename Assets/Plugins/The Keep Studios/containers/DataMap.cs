using UnityEngine;
using System.Collections.Generic;

//I'm going to be the first to admit this is a bad name. If there are any suggestions, tell me. -Ian S.
public class DataMap<Key,Data,Link> where Link : Link<Key,Data>, new()
{

	public Data valueWhenKeyIsMissing;

	//Should never be accessed directly in code, but needs to be public for serialization
	[SerializeField]
	private List<Link> keyValuePairs;
	
	[SerializeField]//doesn't work, but in case LATER this is implemented, this is useful
	private Dictionary<Key, Link> m_dict;


	protected Dictionary<Key, Link> stitchedDict {

		get {
			
			if (this.keyValuePairs == null || this.m_dict == null || this.keyValuePairs.Count != this.m_dict.Count) {
				
				restitchDict ();
				
			}
			
			return this.m_dict;
			
		}

		set { this.m_dict = value; }
	}
	
		
	public DataMap(){
		
		keyValuePairs = new List<Link>();
		
		
	}
	
	
	public DataMap( List<Link> _serializedSource ){
		
		
		keyValuePairs = _serializedSource;
		
	}


	public Data getData (Key key)
	{
		
		if (this.stitchedDict.ContainsKey (key)) {
			
			return this.stitchedDict[key].getData();
			
		} else {
			
			return this.valueWhenKeyIsMissing;
			
		}
		
	}


	public void setData (Key _key, Data _data)
	{
		
		if ( !this.stitchedDict.ContainsKey(_key) ) {
			
			Link dataMap = new Link ();
			
			dataMap.setKey(_key);
			
			dataMap.setData(_data);
			
			this.addMap (dataMap);
			
			this.restitchDict ();
			
		} else {
			
			this.stitchedDict[_key].setData(_data);
			
		}
		
	}


	protected void restitchDict ()
	{
		
		if (this.m_dict == null) {
			
			this.m_dict = new Dictionary<Key, Link> ();
			
		}
		
		this.m_dict.Clear ();
		
		this.validateMaps ();
		
		foreach (Link nextMap in this.keyValuePairs) {
			
			if( this.m_dict.ContainsKey(nextMap.getKey()) ){
				
				this.keyValuePairs.Remove(nextMap); //the key is a duplicate, so we remove it completely
				
			}
			else{
				
				this.m_dict[nextMap.getKey()] = nextMap;
				
			}
			
			
		}
		
	}


	protected void addMap (Link _dataMap)
	{
		
		this.validateMaps ();
		
		this.keyValuePairs.Add (_dataMap);
		
	}


	protected void validateMaps ()
	{
		
		if (this.keyValuePairs == null) {
			
			this.keyValuePairs = new List<Link> ();
			
		}
		
	}

	//INTERNAL CLASSES

	
}


public abstract class Link<Key,Data>
{
	
	public abstract Key getKey();
	
	public abstract void setKey(Key _key);
	
	public abstract Data getData();
	
	public abstract void setData(Data _data);
	
}