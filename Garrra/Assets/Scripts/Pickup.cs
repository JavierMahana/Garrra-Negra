using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ShipController shipController = collision.GetComponent<ShipController>();
            if (shipController)
            {
                shipController.PickupItem(gameObject);
            }
            else if (gameObject.CompareTag("Heart"))
            {
                UI.instance.healthbar.Heal(10f);
            }
            Destroy(gameObject);
        }
     
    }
}
