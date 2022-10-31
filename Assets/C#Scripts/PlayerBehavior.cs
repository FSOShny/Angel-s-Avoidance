using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 1f;

    private Rigidbody rb;
    private float hInput;
    private float vInput;
    private bool depthSwitch = true;
    private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            depthSwitch = !depthSwitch;
        }

        if (Input.GetMouseButtonDown(0))
        {
            newAngle = transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            newAngle.y += (Input.mousePosition.x - lastMousePosition.x) * rotateSpeed;
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotateSpeed;
            transform.localEulerAngles = newAngle;
            lastMousePosition = Input.mousePosition;
        }
    }

    private void FixedUpdate()
    {
        if (depthSwitch)
        {
            NormalMove(0, 1);
        }
        else
        {
            NormalMove(1, 0);
        }
    }

    public void NormalMove(int Y, int Z)
    {
        rb.MovePosition(transform.position +
                transform.right * hInput * Time.fixedDeltaTime +
                transform.up * vInput * Time.fixedDeltaTime * Y +
                transform.forward * vInput * Time.fixedDeltaTime * Z);
    }
}
