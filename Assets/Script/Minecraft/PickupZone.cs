using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    public ItemData data;
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior _player = other.GetComponent<PlayerBehavior>();
        if (_player != null)
        {
            _player.inventory.AddItem(data, 1);
            Destroy(transform.parent.gameObject);
        }
    }
}
