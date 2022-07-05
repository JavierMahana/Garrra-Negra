using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    public Animator animator;
    void Awake()
    {
        defaultSpeed = moveSpeed;
        slowSpeed = moveSpeed * 0.8f;
    }

    public float moveSpeed;
    public float defaultSpeed;
    public float slowSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    bool facingRight = true;


    void Start()
    {
        if (!GameManager.instance.OnStart)
        {
            gameObject.transform.position = GameManager.instance.SavePoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
        if(moveDirection.x < 0 && facingRight)
            Flip();
        if (moveDirection.x > 0 && !facingRight)
            Flip();

        //Vertical//
        if (moveDirection.y == 0)
        {
            animator.SetBool("walkup", false);
            animator.SetBool("walkdown", false);
        }
        else if(moveDirection.y > 0)
        {
            animator.SetBool("walkup", true);
        }
        else if(moveDirection.y < 0)
        {
            animator.SetBool("walkdown", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


}
