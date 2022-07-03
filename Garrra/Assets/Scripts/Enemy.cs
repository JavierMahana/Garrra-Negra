using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float sightDistance = 5.0f;
    public bool isAttacking = false;
    public float movementSpeed = 1.0f;
    public bool onAttackRange = false;

    public float attackRange = 1f;

    public float attackPrepTime = 1.0f;

    public LayerMask obstacleMask;

    public Proyectil attackProyectil;
    public float proyectilSpawnOffset = 0.5f;



    [HideInInspector]
    public bool playerOnSight = false;

    public Animator animator;

    private CircleCollider2D circleCollider;

    private GameObject player;
    private CircleCollider2D playerCollider;

    private RaycastHit2D[] hitsBuffer = new RaycastHit2D[10];




    private float timerOnAttackRange = 0;

    private const float attackDistanceTreshold = 0.4f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CircleCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {
        playerOnSight = CheckPlayerIsOnSight();

        animator.SetBool("PlayerOnSight", playerOnSight);

        if (isAttacking)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                isAttacking = false;
            }
        }

        if (playerOnSight && !isAttacking)
        {
            ChasePlayer(Time.deltaTime);
            if (TryAttackPlayer(Time.deltaTime))
            {

                isAttacking = true;
                animator.SetTrigger("Attack");
            }
        }
    }

    public void LaunchAttack()
    {
        var proyectilDirection = (player.transform.position - transform.position).normalized;
        var startingPos = transform.position + (proyectilDirection * proyectilSpawnOffset);

        var angle = Vector3.SignedAngle(proyectilDirection, Vector3.right, new Vector3(0,0,-1));

        var proyectilInstance = Instantiate(attackProyectil, startingPos, Quaternion.Euler(0,0, angle));
    }

    //Esta funcion realiza la logica para calcular si se debe atacar o no.
    //devuelve true si se debe atacar, falso sino
    private bool TryAttackPlayer(float deltaTime)
    {
        var distToPlayerSqr = (player.transform.position - transform.position).sqrMagnitude;
        var attackRangeSqr = attackRange * attackRange;
        var attackDistanceTresholdSqr = attackDistanceTreshold * attackDistanceTreshold;

        if (attackRangeSqr >= distToPlayerSqr)
        {
            onAttackRange = true;
            timerOnAttackRange += deltaTime;
            if (timerOnAttackRange >= attackPrepTime)
            {
                timerOnAttackRange = 0;
                return true;
            }
        }
        else if (onAttackRange && attackRangeSqr + attackDistanceTresholdSqr >= distToPlayerSqr)
        {
            timerOnAttackRange += deltaTime;
            timerOnAttackRange += deltaTime;
            if (timerOnAttackRange >= attackPrepTime)
            {
                timerOnAttackRange = 0;
                return true;
            }
        }
        else
        {
            onAttackRange = false;
            timerOnAttackRange = 0;
        }

        return false;
    }
    private void ChasePlayer(float deltaTime)
    {
        //sigue al jugador a max velocidad, hasta que llega cerca de el y ahi trata de quedarse pegado.

        var separation = player.transform.position - transform.position;
        var distance = separation.magnitude;

        var rad = (circleCollider.radius + playerCollider.radius);
        Vector2 destinastionDir = separation/distance;

        var destinationPoint = destinastionDir * (distance - rad);

        var movementMag = deltaTime * movementSpeed;

        if (destinationPoint.sqrMagnitude < movementMag * movementMag)
        {
            movementMag = distance - rad;
        }
        transform.Translate(destinationPoint.normalized * movementMag);
    }

    private bool CheckPlayerIsOnSight()
    {
        //sqr dist para no hacer raiz cuadradas.
        bool inSightDist = (sightDistance * sightDistance) >= Vector2.SqrMagnitude(player.transform.position - transform.position);

        
        if (!inSightDist)
        {
            return false;
        }    

        int hits = Physics2D.LinecastNonAlloc(transform.position, player.transform.position, hitsBuffer, obstacleMask);
        if (hits > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void TakeDamage(int damage)
    {
        //resetea el ataque
        timerOnAttackRange = 0;

        health -= damage;
        Debug.Log("damage taken!");
    }
    public void Recoil(float distance)
    {
        transform.position = new Vector2(transform.position.x + distance, transform.position.y + distance);
    }
}
