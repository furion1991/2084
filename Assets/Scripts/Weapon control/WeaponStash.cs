using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStash : MonoBehaviour
{
    public static Dictionary<WeaponType, WeaponDefinition> WEAPONS;
    public WeaponDefinition[] definitons;

    public WeaponDefinition GetWeaponDefinition(WeaponType weaponType)
    {
        return WEAPONS[weaponType];
    }

    private void Awake()
    {

        WEAPONS = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition definition in definitons)
        {
            WEAPONS[definition.type] = definition;
        }

    }
}


[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public GameObject projectilePrefab;
    public float damageOnHit = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}

