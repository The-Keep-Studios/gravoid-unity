// 
// SpecialEffects.cs
//  
// Author:
//       Ian T. Small <ian.small@thekeepstudios.com>
// 
// Copyright (c) 2013 The Keep Studios LLC
using System;
using UnityEngine;
using System.Collections.Generic;

/* Storage for special effects GameObjects */
[System.Serializable]
public class SpecialEffects : MonoBehaviour
{
    // There are four possible special effects that can be assigned.
    public List<ThrusterSpecialEffect> positiveThrustEffect;
    public List<ThrusterSpecialEffect> negativeThrustEffect;
    public List<ThrusterSpecialEffect> positiveTurnEffect;
    public List<ThrusterSpecialEffect> negativeTurnEffect;

    // How loud should collision sounds be? This is used in the OnCollisionEnter () function.
    public float collisionVolume;
}

