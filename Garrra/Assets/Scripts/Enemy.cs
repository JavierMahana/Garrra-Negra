using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken!");

    }
    public void Recoil(float distance)
    {
        transform.position = new Vector2(transform.position.x + distance, transform.position.y + distance);
    }
}
