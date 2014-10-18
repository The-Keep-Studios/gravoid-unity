using UnityEngine;
using System.Collections.Generic;

//Need to make it 1 particle system which creates all 4 engine particles. Group them into Left and Right so that turning can be accomplished.
public class ThrusterSpecialEffect : MonoBehaviour{

	private float r1;

	private float r2;

	public int density;

	private ParticleSystem.Particle[] points;

	public int length;

	public float ThrustRatio{
		get{ return r1; }
		set { r1 = Mathf.Clamp(value, 0, 1); }
	}

	public float TurnRatio{
		get { return r2; }
		set { r2 = Mathf.Clamp(value, 0, 1); }
	}

	public float ThrottleRatio{
		get { return (ThrustRatio + TurnRatio) / 2; }
	}

	public bool IsActive{
		get { return !Mathf.Approximately(ThrustRatio + TurnRatio, 0); }
	}
	
	// Use this for initialization
	void Start(){
		ThrustRatio = 0;
		TurnRatio = 0;
		CreatePoints();
	}
	
	void Reset(){
		
		length = 2;
		
	}
	
	void Update(){
		
		CreatePoints();
		
		particleSystem.SetParticles(points, points.Length);

		bool soundUpdateSuccessful = IsActive ? PlaySound() : StopSound();

		if(!soundUpdateSuccessful){

			Debug.LogWarning("Unable to update the thruster sound effect", this);

		}

	}

	private void CreatePoints(){
		
		points = new ParticleSystem.Particle[density];

		float currentLength = (-1) * this.length * ((r1 + r2) / 2.0f);
		
		float increment = currentLength / density; //Particles start at 0 and end at 2

		for(int i = 0; i < density; i++){
			
			float y = i * increment;
			
			points[i].position = new Vector3(0f, y, 0f);
			
			points[i].color = new Color(0f, 1f, 0f);
			
			points[i].size = 1f;
			
		}
		
	}
	
	bool PlaySound(){
		if(this.audio){
			if(!this.audio.isPlaying){
				this.audio.Play();
			}
			this.audio.volume = ThrottleRatio;
			return true;
		} else{
			return false;
		}
	}

	bool StopSound(){
		if(this.audio && this.audio.isPlaying){
			this.audio.Stop();
		}
		return true;
	}
}
