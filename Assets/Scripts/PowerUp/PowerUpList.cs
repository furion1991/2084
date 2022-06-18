using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpList : MonoBehaviour
{
    public static Dictionary<EPowerUpType, PowerUpDefinition> POWERUPDEFINITIONS;
    public PowerUpDefinition[] puDefinitions;

    private void Awake()
    {
        POWERUPDEFINITIONS = new Dictionary<EPowerUpType, PowerUpDefinition>();

        foreach (var definition in puDefinitions)
        {
            POWERUPDEFINITIONS[definition.powerUpType] = definition;
        }
    }
}

[System.Serializable]
public class PowerUpDefinition
{
    public EPowerUpType powerUpType;
    public GameObject appearance;
}
