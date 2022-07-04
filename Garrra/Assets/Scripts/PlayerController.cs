using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public Slider healthBar;
    public GameObject shield;
    float parryWindow;
    public float startParryTimer;
    public float shieldTimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       ShieldUp();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                UI.instance.healthbar.TakeDamage(10f);
                
                break;
            
        }
    }

    private void Parry()
    {
        parryWindow -= Time.deltaTime;

        if (parryWindow <= startParryTimer && parryWindow > 0 && shield.GetComponent<Shield>().isColliding)
        {         
           
                               
                       
            
        }
        
    }

    private void ShieldUp()
    {
        if (Input.GetKeyDown(KeyCode.C) && shieldTimer == 1.5f)
        {
            shield.SetActive(true);
            Parry();
            shieldTimer -= Time.deltaTime;

        }
        else
        {
            shieldTimer = 1.5f;
            shield.SetActive(false);
        }
        
    }

}
