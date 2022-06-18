using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WeaponType
{
    none,
    pistol,
    rifle,
    shotgun
}

public class Weapon : MonoBehaviour
{
    public WeaponType _type;
    public GameObject projectileAnchor;
    public WeaponDefinition weaponDefinition;
    public float lastShotMade;
    public string weaponOwner;
    public int ammo;
    public void Fire()
    {
        if (ammo <= 0)
        {
            ammo = 0;
            return;
        }
        if (projectileAnchor != null)
        {
            if (_type == WeaponType.pistol)
            {
              if (weaponOwner == "Enemy" && Time.time - lastShotMade < WeaponStash.WEAPONS[_type].delayBetweenShots)
                {
                    return;
                }
                GameObject p = Instantiate(weaponDefinition.projectilePrefab);
                if (weaponOwner == "Enemy")
                {
                    p.tag = "EnemyProjectile";
                    p.layer = 9;
                }
                p.transform.position = projectileAnchor.transform.position;
                p.transform.rotation = projectileAnchor.transform.rotation;
                ammo--;
                lastShotMade = Time.time;
            }
        }
    }
    public void SetType(WeaponType weaponType)
    {
        foreach (var item in WeaponStash.WEAPONS)
        {
            if (item.Key == weaponType)
            {
                _type = weaponType;
            }
        }
    }







}
