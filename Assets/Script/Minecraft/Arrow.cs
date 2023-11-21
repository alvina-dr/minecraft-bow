using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool isShot = false;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private PickupZone pickupCol;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBehavior>() != null) return;
        if (other.GetComponent<Arrow>() != null) return;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void ShootArrow(Vector3 shootParameters)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = true;
        transform.parent = null;
        rb.AddForce(shootParameters, ForceMode.Impulse);
        isShot = true;
    }

    private void Update()
    {
        if (isShot)
        {
            //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            //transform.Rotate(-.normalized);
        }
    }
}
