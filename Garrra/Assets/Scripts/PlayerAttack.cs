using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCD;
    public float startAttackCD;
    public Animator animator;

    public Transform attackPos;
    public LayerMask Enemies;
    public float attackRange;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(attackCD <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                animator.SetTrigger("Swing");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Enemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            attackCD = startAttackCD;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
