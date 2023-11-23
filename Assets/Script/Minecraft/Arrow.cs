using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool isFlying = false;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Collider col;
    [SerializeField] private PickupZone pickupCol;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBehavior>() != null) return;
        if (other.GetComponent<Arrow>() != null) return;
        Monster _monster = other.GetComponent<Monster>();
        if (_monster != null)
        {
            _monster.Damage(5.0f, rb.velocity);
            Destroy(this.gameObject);
        } else
        {
            pickupCol.gameObject.SetActive(true);
        }
        isFlying = false;
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
        isFlying = true;
        mesh.SetActive(true);
    }

    private void Update()
    {
        if (isFlying)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}
