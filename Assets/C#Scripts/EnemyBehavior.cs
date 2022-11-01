using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 norm = new Vector3(1.0f, 1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position +
                transform.right * Time.fixedDeltaTime +
                transform.up * Time.fixedDeltaTime +
                transform.forward * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("hehehe");
        }
    }
}
