using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 cameraOffset = new Vector3(1.21f, 5f, -4.77f);

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = cameraOffset + player.transform.position;
        }
    }
}
