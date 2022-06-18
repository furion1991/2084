using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, ILongRangeWeapon
{
    private int _ammo;
    private eWeaponType _weaponType = eWeaponType.pistol;
    private GameObject projectileAnchor;
    public int ammo { get; set; }
    public int clipSize { get; }
    public eWeaponType weaponType 
    {
        get { return _weaponType; } 
        set { _weaponType = value; } 
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(Resources.Load("projectile")) as GameObject;
        projectile.transform.position = projectileAnchor.transform.position;
        projectile.transform.rotation = projectileAnchor.transform.rotation;
    }

    private void Awake()
    {
        projectileAnchor = GameObject.Find("ProjectileAnchor");
    }
}
