using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public bool isStatic;
    public bool isWalker;
    public bool isWalkingR;
    public bool isPatroller;

    
    public Transform wallCheck,groundCheck,gapCheck;
    public bool wallDetected,groundDetected,gapDetected;
    public float detectionRadius;
    public LayerMask whatIsGround;
    public Transform pointA,pointB;
    public bool moveToA,moveToB;

    Rigidbody2D rb2;
    Animator animation;
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animation =GetComponent<Animator>();
        moveToA = true;
    }

    // Update is called once per frame
    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position,detectionRadius,whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position,detectionRadius,whatIsGround);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position,detectionRadius,whatIsGround);

        if((gapDetected || wallDetected) && groundDetected )
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        if(isStatic)
        {
            animation.SetBool("Idle",true);
            rb2.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if(isWalker)
        {
            animation.SetBool("Idle",false);
            animation.SetBool("walk",true);
            rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
            if(!isWalkingR)
            {
                rb2.velocity= new Vector2(-speed*Time.deltaTime, rb2.velocity.y);
            }
            else
            {
               rb2.velocity= new Vector2(speed*Time.deltaTime, rb2.velocity.y); 
            }
        }
        if(isPatroller)
        {
            animation.SetBool("walk",true);
            animation.SetBool("Idle",false);
            if(moveToA)
            {
                rb2.velocity = new Vector2(-speed*Time.deltaTime, rb2.velocity.y);
                if(Vector2.Distance(transform.position,pointA.position)<0.2f)
                {
                    Flip();
                    moveToA = false;
                    moveToB = true;
                }
            }
            if(moveToB)
            {
                rb2.velocity = new Vector2(speed*Time.deltaTime, rb2.velocity.y);
                if(Vector2.Distance(transform.position,pointB.position)<0.2f)
                {
                    Flip();
                    moveToA = true;
                    moveToB = false;
                }
            }
        }
    }
    private void Flip()
    {
        isWalkingR = !isWalkingR;
        Vector3 theScale = transform.localScale;
        theScale.x *=-1;
        transform.localScale = theScale; 
    }
}
