using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eWeaponType
{
    pistol,
    rifle
}




public interface ILongRangeWeapon
{
    public eWeaponType weaponType { get; set; }
    public int ammo { get; set; }
    public int clipSize { get; }
    void Fire();
}

public interface ICloseRangeWeapon
{
    public int damage { get; }
    void Hit();
}
