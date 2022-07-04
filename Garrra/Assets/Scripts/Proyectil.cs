using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Proyectil : MonoBehaviour
{
    //tiempo de vide del proyectil.
    public float livespan = 1.0f;

    public float speed = 0.0f;

    public int damage = 1;

    public bool playerTeam = false;

    private float livedTime;
    private Vector2 direction;

    void Start()
    {
        livedTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        livedTime += Time.deltaTime;
        if (livedTime >= livespan)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    public void Reflect()
    {
        playerTeam = !playerTeam;
        transform.Rotate(new Vector3(0, 0, 180));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!playerTeam)
        {
            if (collision.gameObject.tag == "Player")
            {
                var healthbar = FindObjectOfType<HealthBar>();
                healthbar.TakeDamage(damage);
                //Debug.Log($"Aplica daño: {damage}.");

            }
        }
        else
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }
        }



        Destroy(this.gameObject);
    }
}
