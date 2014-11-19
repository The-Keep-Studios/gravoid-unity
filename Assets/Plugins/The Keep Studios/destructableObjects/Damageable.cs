using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damageable : MonoBehaviour{
	
	[UnityEngine.SerializeField]
	private int
		currentLevel;
	
	[UnityEngine.SerializeField]
	private int
		minLevel;
	
	[UnityEngine.SerializeField]
	private int
		maxLevel;
	
	[UnityEngine.SerializeField]
	public float
		impactVulnerabiliy;
	
	public List<Trigger> triggers;

	public int Level{
		get{
			
			return Mathf.Clamp(currentLevel, minLevel, maxLevel);
			
		}
		set{
			
			int oldLevel = currentLevel;
			
			currentLevel = Mathf.Clamp(value, minLevel, maxLevel);
			
			Damageable.Trigger.Paramters param = new Damageable.Trigger.Paramters(this, oldLevel);
		
			foreach(Trigger nextTrigger in this.triggers){
				
				nextTrigger.Signal(param);
				
			}
		}
	}
	
	void OnCollisionEnter(Collision col){

		float recievedForce = (col.rigidbody.mass * col.relativeVelocity.magnitude);

		this.Level += Mathf.FloorToInt(recievedForce * this.impactVulnerabiliy);
		
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		if(stream.isWriting){
			stream.Serialize(ref minLevel);
			stream.Serialize(ref maxLevel);
			stream.Serialize(ref impactVulnerabiliy);
			stream.Serialize(ref currentLevel);
		} else{
			stream.Serialize(ref minLevel);
			stream.Serialize(ref maxLevel);
			stream.Serialize(ref impactVulnerabiliy);

			//make sure to always update the level through the property
			int newCurrentLevel = 0;
			stream.Serialize(ref newCurrentLevel);
			this.Level = newCurrentLevel; 
		}
	}
	
	[System.Serializable]
	public class Trigger: System.IComparable<Trigger>{
		
		public class Paramters{
			
			public Damageable objectHealth;
			
			public int oldHealthLevel;
	
			public Paramters(Damageable objectHealth, int oldHealthLevel){
				this.objectHealth = objectHealth;
				this.oldHealthLevel = oldHealthLevel;
			}
	
		}
		
		public enum EType{
			
			onIncreasing,
			onAnyChange,
			onDecreasing,
			
		}
		
		public int threshold = 0;
		
		public EType type = EType.onAnyChange;
		
		public string eventMessage = "";
			
		public void Signal(Paramters param){

			int newLevel = param.objectHealth.Level;

			int oldLevel = param.oldHealthLevel;
			
			//compare the levels to each other (this finds directionality of the change
			int changeDirection = newLevel.CompareTo(newLevel);

			bool triggerTypeMatchesChangeDirection =
				(this.type == EType.onAnyChange)
				|| (this.type == EType.onIncreasing && changeDirection > 0)
				|| (this.type == EType.onDecreasing && changeDirection < 0);

			//check if we have crossed the threshold (shortcut this by first checking that the direction counts as crossing the threshold)
			bool thresholdCrossed = triggerTypeMatchesChangeDirection 
				&& threshold.CompareTo(newLevel) != threshold.CompareTo(oldLevel);
			
			if(thresholdCrossed){
				param.objectHealth.SendMessage(eventMessage);
			}
			
		}

		#region IComparable implementation
		public int CompareTo(Trigger obj){
			
			int levelComparison = this.threshold.CompareTo(obj.threshold);
			
			//if the triggers are at the same level, we must compare the triggerType enumerated values
			return levelComparison != 0 ?
				levelComparison :
				this.type.CompareTo(obj.type);
		}
		#endregion
		
	}
	
}
