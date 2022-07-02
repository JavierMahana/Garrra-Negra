using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShipController shipController = collision.GetComponent<ShipController>();
        if (shipController)
        {
            shipController.PickupItem(gameObject);
            Destroy(gameObject);

        }
    }
}
