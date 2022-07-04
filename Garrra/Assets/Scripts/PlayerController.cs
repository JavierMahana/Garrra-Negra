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
    private float shieldCD;
    public float startShieldCD;
    
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
    private void ShieldUp()
    {
        
        if (Input.GetKeyDown(KeyCode.C) && !shield.activeInHierarchy)
        {
            shield.SetActive(true);
            Parry();
            shieldCD = startShieldCD;
        }          
        else
        {
            shieldCD -= Time.deltaTime;
            if(shieldCD <= 0)
            {
                shield.SetActive(false);
            }
        }
    }
    
    private void Parry()
    {
        parryWindow -= Time.deltaTime;

        if (parryWindow <= startParryTimer && parryWindow > 0 && shield.GetComponent<Shield>().isColliding)
        {         
                                        
                     
            
        }
        
    }

   

}
