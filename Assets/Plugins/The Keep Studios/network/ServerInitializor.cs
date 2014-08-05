using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners {
	public class ServerInitializor : MonoBehaviour
	{
		public bool initializeOnAwake;
		
		public int allowedConnections;
		
		public int listeningPort;
		
		public bool useNAT;

		void Awake(){

			InitializeServer();

		}
		
		public void InitializeServer(){
			Network.InitializeServer ( allowedConnections, listeningPort, useNAT);
		}
		
		public void InitializeServer(object requestSource,  EventArgs e){ 
			InitializeServer( allowedConnections, listeningPort, useNAT);
		}
		
		public void InitializeServer(int _allowedConnections, int _listeningPort, bool _useNAT){ 
			Debug.Log("Initializing Server Single Player" );
			Network.InitializeServer ( _allowedConnections, _listeningPort, _useNAT);
		}
	}
}

