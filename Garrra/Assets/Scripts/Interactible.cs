using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    Transform transformObj;
    Collider2D collider2d;
    SpriteRenderer sprite;

    float defaultRotation;
    float corner;

    [SerializeField]
    bool isDoor;
    [SerializeField]
    GameObject objectInteractible;

    int cooldown = 0;
    [SerializeField]
    int cooldownDuration = 300;



    void Awake()
    {
        if (objectInteractible)
        {
            collider2d = objectInteractible.GetComponent<Collider2D>();
            sprite = objectInteractible.GetComponent<SpriteRenderer>();
            transformObj = objectInteractible.GetComponent<Transform>();
            defaultRotation = transform.rotation.z;
            corner = transform.position.x;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown--;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
           Input.GetAxisRaw("Interact")!=0 )
        {
            if (isDoor && cooldown <= 0)
            {
                cooldown = cooldownDuration;
                Vector3 moveVect = new Vector3(transformObj.localScale.x * 0.5f, -transformObj.localScale.y * 2, 0);

                //collider2d.enabled = (collider2d.enabled) ? false : true;
                if (collider2d.enabled)
                {
                    collider2d.enabled = false;
                    sprite.color = Color.grey;
                    transformObj.rotation = Quaternion.Euler(0, 0, defaultRotation + 90);
                    transformObj.position += moveVect;
                }
                else
                {
                    collider2d.enabled = true;
                    sprite.color = Color.white;
                    transformObj.rotation = Quaternion.Euler(0, 0, defaultRotation);
                    transformObj.position -= moveVect;
                }
            }
        }   
    }
}
