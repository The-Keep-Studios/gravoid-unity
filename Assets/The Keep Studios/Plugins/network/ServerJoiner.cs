using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network{

	public class ServerJoiner : MonoBehaviour{

		private HostData hostToJoin;

		public HostData HostToJoin{
			get{
				return hostToJoin;
			}
			set{
				hostToJoin = value;
			}
		}

		// Use this for initialization
		void Start(){
	
		}
	
		// Update is called once per frame
		void Update(){
	
		}

		public void JoinServer(){
			Network.Connect(hostToJoin);
		}
	}
}