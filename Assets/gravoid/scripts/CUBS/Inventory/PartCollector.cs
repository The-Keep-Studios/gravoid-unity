using UnityEngine;
using System.Collections;

namespace TheKeepStudios.Gravoid.CUBS.Inventory{
	public class PartCollector : MonoBehaviour{
		public InventoryBehavior inventory;


		//FIXME this needs find CollectableParts (via collider as trigger) and store it in this inventory
		void OnTriggerEnter(Collider other){

			CollectablePart collected = other.gameObject.GetComponent<CollectablePart>(); //attempt to rerieve a collectable part from this

			if(collected && collected.part && inventory){

				if(!inventory.Insert(collected.part)){

					Debug.LogWarning("Unable to insert the collected part into our inventory!");

				} else{

					Debug.Log("Collected Resource");
					
					collected.OnCollected();

				}
			}
		}


	}

}
