using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid 
{
	public class PartSelectionBehavior : MonoBehaviour
	{
		
		#region PRIVATE PROPERTIES
		[SerializeField]
		private ProjectilePartBehavior
			m_projectilePartPrefab;
		[SerializeField]
		private PartSelectionBehavior
			m_prefab;
		private const string m_poolName = "PartSelections";
		
		#endregion
		#region PUBLIC METHODS
	
		public static PartSelectionBehavior Spawn (PartSelectionBehavior _prefab)
		{
				
			Transform newParentObject = PathologicalGames.PoolManager.Pools [m_poolName].Spawn (_prefab.transform);
				
			if (newParentObject != null) {
					
				return newParentObject.GetComponent<PartSelectionBehavior> ();
					
			} else {
				
				return null;
				
			}
			
		}
	
		
		public PartSelectionBehavior GetPartType ()
		{
			return m_prefab;
		}
	
	
		public ProjectilePartBehavior GetPartPrefab ()
		{
			return m_projectilePartPrefab;
		}
	
	
		public void Despawn ()
		{
				
			PathologicalGames.PoolManager.Pools [m_poolName].Despawn (this.transform);
		}
	
		
		static public ProjectilePartBehavior GetPartPrefabFromSelection (PartSelectionBehavior _selection)
		{
			
			return _selection.GetPartPrefab ();
			
		}
		#endregion
		#region PRIVATE METHODS
		#endregion
	}
}
