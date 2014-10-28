using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheKeepStudios{
	
	public class Spawned : MonoBehaviour, IOriginPoolAwareSpawnable{
		
		public string originSpawnPoolName;

		public string OriginSpawnPoolName{
			get{
				return originSpawnPoolName;
			}
			set{
				originSpawnPoolName = value;
			}
		}
		
		public void Despawn(){
			NetworkView nview = this.networkView;
			if((Network.isClient || Network.isServer) && nview != null){
				if(nview.isMine){ //only the owner can cause despawning
					nview.RPC("_Despawn", RPCMode.AllBuffered);
				}
			} else{
				_Despawn();
			}
			
		}
		
		private void _Despawn(){
			PathologicalGames.SpawnPool pool = 
				PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName)
				? PathologicalGames.PoolManager.Pools[originSpawnPoolName]
				: null;
			if(pool != null && pool.IsSpawned(this.transform)){
				pool.Despawn(this.transform);
			} else{
				Debug.LogWarning("Object spawned outside of pool; unable to despawn, so destroying object instead.", this.gameObject);
				Destroy(this.gameObject);
			}
		}

		public void OnSpawned(Component originSpawnPool){
			this.OriginSpawnPoolName = originSpawnPool.name;
		}
	}
}