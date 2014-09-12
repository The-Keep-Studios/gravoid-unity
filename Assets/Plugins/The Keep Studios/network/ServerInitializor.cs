using UnityEngine;
using System.Collections;
using System;

namespace TheKeepStudios.events.listeners{

	public class ServerInitializor : MonoBehaviour{
		public bool initializeOnAwake;
		
		public int allowedConnections;
		
		public int listeningPort;
		
		public bool useNAT;

		public string gameType;
		
		public string gameName;

		public bool registerWithMasterServerOnInitialize;
		 
		void Awake(){

			if(initializeOnAwake){
				InitializeServer();
			}

		}
		
		public void InitializeServer(){
			Network.InitializeServer(allowedConnections, listeningPort, useNAT);
		}
		
		public void InitializeServer(object requestSource, EventArgs e){ 
			InitializeServer(allowedConnections, listeningPort, useNAT);
		}
		
		public void InitializeServer(int _allowedConnections, int _listeningPort, bool _useNAT){ 
			Debug.Log("Initializing Server Single Player");
			if(registerWithMasterServerOnInitialize){
				MasterServer.RegisterHost(this.gameType, this.gameName);
			}
			Network.InitializeServer(_allowedConnections, _listeningPort, _useNAT);
		}
	}
}

