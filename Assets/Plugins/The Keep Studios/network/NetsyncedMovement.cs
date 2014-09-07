using UnityEngine;
using System.Collections;

namespace TheKeepStudios.network
{
		public class NetsyncedMovement : MonoBehaviour
		{

				private delegate void NetStreamHandler (BitStream stream,NetworkMessageInfo info);

				NetStreamHandler writeRidgidbodyPhysics;
				NetStreamHandler readRidgidbodyPhysics;
				NetStreamHandler write2dRidgidbodyPhysics;
				NetStreamHandler read2dRidgidbodyPhysics;
				NetStreamHandler writeTransform;
				NetStreamHandler readTransform;
	
				void Start ()
				{
						writeRidgidbodyPhysics = new NetStreamHandler (writeRidgidbodyPhysics);
						readRidgidbodyPhysics = new NetStreamHandler (ReadRidgidbodyPhysics);
						write2dRidgidbodyPhysics = new NetStreamHandler (Write2dRidgidbodyPhysics);
						read2dRidgidbodyPhysics = new NetStreamHandler (Read2dRidgidbodyPhysics);
						writeTransform = new NetStreamHandler (WriteTransform);
						readTransform = new NetStreamHandler (ReadTransform);
				}
				
				void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
				{
						NetStreamHandler handler = GetStreamHandler (stream.isWriting);
						handler (stream, info);
				}
	
				/// <summary>
				/// Gets the stream handler for the current state of the game object.
				/// </summary>
				/// <returns>The stream handler.</returns>
				/// <param name="isStreamWriting">
				///     If set to <c>true</c> returns a stream writing handler, 
				///     else it returns a stream reader behavior.
				/// </param>
				private NetStreamHandler GetStreamHandler (bool isStreamWriting)
				{
						if (this.rigidbody != null) {
								return isStreamWriting ? writeRidgidbodyPhysics : readRidgidbodyPhysics;
						} else if (this.rigidbody2D != null) {
								return isStreamWriting ? write2dRidgidbodyPhysics : read2dRidgidbodyPhysics;
						} else {
								return isStreamWriting ? writeTransform : readTransform;
						}
				}
				
				void WriteRidgidbodyPhysics (BitStream stream, NetworkMessageInfo info)
				{
						writeTransform (stream, info);
						Vector3 vel = rigidbody.velocity;
						Vector3 angVel = rigidbody.angularVelocity;
						stream.Serialize (ref vel);
						stream.Serialize (ref angVel);
				}
		
				void ReadRidgidbodyPhysics (BitStream stream, NetworkMessageInfo info)
				{
						readTransform (stream, info);
						Vector3 vel = Vector3.zero;
						Vector3 angVel = Vector3.zero;
						stream.Serialize (ref vel);
						stream.Serialize (ref angVel);
						rigidbody.velocity = vel;
						rigidbody.angularVelocity = angVel;
				}
		
				void Write2dRidgidbodyPhysics (BitStream stream, NetworkMessageInfo info)
				{
						writeTransform (stream, info);
						Vector3 vel = rigidbody2D.velocity;
						float angVel = rigidbody2D.angularVelocity;
						stream.Serialize (ref vel);
						stream.Serialize (ref angVel);
				}
		
				void Read2dRidgidbodyPhysics (BitStream stream, NetworkMessageInfo info)
				{
						readTransform (stream, info);
						Vector3 vel = Vector3.zero;
						float angVel = 0.0f;
						stream.Serialize (ref vel);
						stream.Serialize (ref angVel);
						rigidbody2D.velocity = vel;
						rigidbody2D.angularVelocity = angVel;
				}
		
				void WriteTransform (BitStream stream, NetworkMessageInfo info)
				{
						Vector3 pos = transform.position;
						stream.Serialize (ref pos);
						Quaternion rot = transform.rotation;
						stream.Serialize (ref rot);
				}
		
				void ReadTransform (BitStream stream, NetworkMessageInfo info)
				{
						Vector3 posRec = Vector3.zero;
						stream.Serialize (ref posRec);
						transform.position = posRec;
						Quaternion rot = Quaternion.identity;
						stream.Serialize (ref rot);
						transform.rotation = rot;
				}
		}
}
