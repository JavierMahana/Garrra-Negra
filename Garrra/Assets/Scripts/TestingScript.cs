using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public bool KillOnTouch;

    Collider2D collider2d;
    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && KillOnTouch) Destroy(collision.gameObject);
    }
}
