using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekenesis : Ability
{
    public float knockback;
    public float stunTime;

    [SerializeField]
    private float _damage;
    [SerializeField]
    private float speed;
    private GameObject implantAnchor;


    private void Awake()
    {
        implantAnchor = GameObject.Find("Implant");
        transform.position = implantAnchor.transform.position;
        transform.rotation = implantAnchor.transform.rotation;
        birthTime = Time.time;
    }
    private void Update()
    {
        MoveForward();
    }
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        overallLifetime += Time.deltaTime;
        if (overallLifetime > lifetimeDuration)
        {
            Destroy(gameObject);
        }
    }
}
