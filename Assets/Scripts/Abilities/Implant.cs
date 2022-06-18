using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implant : MonoBehaviour
{
    [Header("Set in inspector")]
    public GameObject[] abilitiesPrefab;
    private EAbilityType _abilityType;
    private float lastTimeAbilityUsed;
    [SerializeField]
    private GameObject abilitiesAchor;
    public EAbilityType activeImplantAbility
    {
        get
        {
            return _abilityType;
        }
        set
        {
            _abilityType = value;
        }
    }
    public void UseAbility()
    {
        foreach (GameObject gameObject in abilitiesPrefab)
        {
            Ability ab = gameObject.GetComponent<Ability>();
            if (lastTimeAbilityUsed !=0 && Time.time - lastTimeAbilityUsed < ab.cooldown)
            {
                return;
            }
            if (ab.type == activeImplantAbility)
            {
                Instantiate(gameObject);
                lastTimeAbilityUsed = Time.time;
            }
        }
        
    }
    private void Awake()
    {
        lastTimeAbilityUsed = 0;
    }
    
}
