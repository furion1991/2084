using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EAbilityType
{
    telekenesis,
    shield
}

public class Ability : MonoBehaviour
{
    public EAbilityType type;
    public float cooldown;
    public float damage;
    public float lifetimeDuration;
    public float birthTime;
    public float overallLifetime;
}
