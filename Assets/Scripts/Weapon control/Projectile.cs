using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0.09f;
    public float birthTime = 0;
    public float lifetime = 4;
    public float lifeDuration;

    private void Awake()
    {
        birthTime = Time.time;
    }
    void Update()
    {
        if (lifeDuration > lifetime)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeDuration += Time.deltaTime;
    }
}
