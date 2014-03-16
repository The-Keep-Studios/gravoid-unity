using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid
{
//SIMPLE PROTOTYPE IMPLEMENTATION
	public class ComponentSelectorBehavior : MonoBehaviour
	{

	#region PUBLIC PROPERTIES
		public EComponentSelectionPreset selectionType;
	#endregion
	#region PRIVATE PROPERTIES
		[SerializeField]
		private ChoiceMap
			selectionOptionToTypeMaps = new ChoiceMap ();
	#endregion

	#region PUBLIC METHODS
		// Use this for initialization
		void Start ()
		{
			this.selectionOptionToTypeMaps.valueWhenKeyIsMissing = new List<PartSelectionBehavior> ();
		}


		// Update is called once per frame
		void Update ()
		{
		
		}
	
		virtual public List<PartSelectionBehavior> GetCurrentSelection ()
		{
		
			return this.selectionOptionToTypeMaps.getData (this.selectionType);
		
		}
	#endregion
	
	#region INTERNAL CLASSES
		[System.Serializable]
		internal class ChoiceMap: DataMap<EComponentSelectionPreset, List<PartSelectionBehavior>, Link>
		{
		}
	
		[System.Serializable]
		public class Link: Link<EComponentSelectionPreset,List<PartSelectionBehavior> >
		{
		
			public EComponentSelectionPreset m_presetType;
			public List<PartSelectionBehavior> m_presetContents;
		
			public override EComponentSelectionPreset getKey ()
			{
				return this.m_presetType;
			}
	
			public override void setKey (EComponentSelectionPreset _key)
			{
				this.m_presetType = _key;
			}
		
			public override List<PartSelectionBehavior> getData ()
			{
				return this.m_presetContents;
			}
		
			public override void setData (List<PartSelectionBehavior> _data)
			{
				this.m_presetContents = _data;
			}
	
		}
	#endregion
	
	#region ENUMERATORS
		public enum EComponentSelectionPreset
		{
		
			ball
		,
			cube
		,
			ballCubeBall
		
		}
		#endregion
	}
}