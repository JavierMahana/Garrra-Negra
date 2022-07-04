using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCD;
    public float startAttackCD;
    public Animator animator;

    public Transform attackPos;
    public LayerMask Enemies;
    public float attackRange;
    public int damage;
    private int combo;
    public bool attacking;



    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(attackCD <= 0)
        {
            if (Input.GetKey(KeyCode.X) && !attacking)
            {
                attacking = true;
                animator.SetTrigger(""+combo);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Enemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].CompareTag("Barrel")) enemiesToDamage[i].gameObject.GetComponent<Destructible>().Drop();
                    else 
                    {
                        Enemy enemy = enemiesToDamage[i].GetComponent<Enemy>();

                        if(enemy)
                        {
                            enemy.TakeDamage(damage);
                            enemy.Recoil(0.5f);
                        }
                        //enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        //enemiesToDamage[i].GetComponent<Enemy>().Recoil(1);
                        if (combo == 2)
                        {
                            enemy.Recoil(1f);
                            //enemiesToDamage[i].GetComponent<Enemy>().Recoil(2f);
                        }
                    } 
                }
            }
            attackCD = startAttackCD;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }

    public void startCombo()
    {
        attacking = false;
        if (combo < 3)
        {
            combo++;
        }
    }
    public void finishCombo()
    {
        attacking = false;
        combo = 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
