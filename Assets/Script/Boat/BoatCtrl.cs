using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCtrl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float acceleration;
    public float rotationSpeed;
    public float maxSpeed;
    public float jumpForce;

    private void FixedUpdate()
    {
        Vector2 _speed = new Vector2(rb.velocity.x, rb.velocity.z);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (speed < maxSpeed)
            {
                speed += acceleration;
            }
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
        else
        {
            if (speed > 0) speed *= 0.9f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(Vector3.one * -rotationSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(Vector3.one * rotationSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
