using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damageable : MonoBehaviour {
	
	[UnityEngine.SerializeField] private int currentLevel;
	
	[UnityEngine.SerializeField] private int minLevel;
	
	[UnityEngine.SerializeField] private int maxLevel;
	
	[UnityEngine.SerializeField] public float impactVulnerabiliy;
	
	public List<Trigger> triggers;

	public int Level {
		get {
			
			return Mathf.Clamp(currentLevel,minLevel,maxLevel);
			
		}
		set {
			
			int oldLevel = currentLevel;
			
			currentLevel = Mathf.Clamp(value,minLevel,maxLevel);
			
			Damageable.Trigger.Paramters param = new Damageable.Trigger.Paramters(this, oldLevel);
		
			foreach( Trigger nextTrigger in this.triggers){
				
				nextTrigger.Signal(param);
				
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		
		//TODO Expand on this as it is a dumb and simple algorithm
		//TODO Enhance for player ship improvements and special features
		this.Level += Mathf.FloorToInt(col.impactForceSum.sqrMagnitude * this.impactVulnerabiliy);
		
	}
	
	[System.Serializable]
	public class Trigger: System.IComparable<Trigger> {
		
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
			
		public void Signal (Paramters param) {
			
			int comparison = param.objectHealth.Level.CompareTo( param.oldHealthLevel );
			
			bool shouldTrigger = ( this.type == EType.onAnyChange && comparison != 0 )
				|| ( this.type == EType.onIncreasing && comparison>0)
				|| (this.type == EType.onDecreasing && comparison<0);
			
			if(shouldTrigger){
				
				param.objectHealth.SendMessage(eventMessage);
				
			}
			
		}

		#region IComparable implementation
		public int CompareTo (Trigger obj) {
			
			int levelComparison = this.threshold.CompareTo( obj.threshold );
			
			//if the triggers are at the same level, we must compare the triggerType enumerated values
			return levelComparison != 0 ?
				levelComparison :
				this.type.CompareTo( obj.type );
		}
		#endregion
		
	}
	
}
