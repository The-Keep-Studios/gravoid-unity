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
			if(nview != null){
				if(nview.isMine){ //only the owner can cause despawning
					nview.RPC("_Despawn", RPCMode.AllBuffered);
				}
			} else{
				_Despawn();
			}
			
		}
		
		private void _Despawn(){
			if(PathologicalGames.PoolManager.Pools.ContainsKey(originSpawnPoolName)){
				PathologicalGames.PoolManager.Pools[originSpawnPoolName].Despawn(this.transform);
			} else{
				Debug.LogWarning("Object spawned with pool; unable to despawn, so destroying object instead.", this.gameObject);
				Destroy(this.gameObject);
			}
		}
	}
}