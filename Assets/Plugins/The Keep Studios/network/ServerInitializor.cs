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

		public string GameName{
			get{
				return gameName;
			}
			set{
				gameName = value;
			}
		}
		 
		void Awake(){

			if(initializeOnAwake){
				InitializeServer();
			}

		}
		
		public void InitializeServer(string serverName){
			gameName = serverName;
			InitializeServer();
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
				RegisterWithMasterServer();
			}
			Network.InitializeServer(_allowedConnections, _listeningPort, _useNAT);
		}

		public void RegisterWithMasterServer(){
			MasterServer.RegisterHost(this.gameType, this.gameName);
		}
	}
}

