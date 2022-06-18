using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPowerUpType
{
    ammo,
    health
}
public class PowerUp : MonoBehaviour
{
    public EPowerUpType type;
    
    private void Update()
    {
        RotateArondSelf();
    }

    private void RotateArondSelf()
    {
        transform.rotation *= Quaternion.Euler(0, 1, 0);
    }
    
}
