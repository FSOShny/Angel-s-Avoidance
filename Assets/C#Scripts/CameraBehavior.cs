using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 playerDist = new Vector3(0f, 0f, -3.0f);

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
