using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isColliding = false;
    public GameObject enemyCollision;
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemyProjectile":
                isColliding = true;
                enemyCollision = collision.gameObject;                
                break;            
            default:
                isColliding = false;
                break;
        }

    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

}
