using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Move : MonoBehaviour
{
    private Enemy3 enemy;
    public float speed;
    private bool wallDetected,groundDetected,gapDetected;
    public Transform wallCheck,groundCheck,gapCheck;
    public Transform pointA,pointB;
    public float detectionRadius = 0.09f;
    public LayerMask whatIsGround;
    public bool isStatic,isWalker,isWalkingR,isPatroller;
    public bool moveToA,moveToB;
    private Rigidbody2D rb2;
    private Animator an;
    void Start()
    {
        
        rb2 = GetComponent<Rigidbody2D>();
        an =GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy3>();
        moveToA= true;
    }
    void Update()
    {
        Check();
        if((gapDetected || wallDetected) && groundDetected )
        {
            isWalker = true;
            isPatroller = false;
            Flip();
        }
    }
    private void FixedUpdate()
    {
        if(isStatic)
        {
            an.SetBool("Idle",true);
            rb2.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if(isWalker)
        {
            an.SetBool("Idle",false);    
            an.SetBool("Move",true);
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
            an.SetBool("Idle",false);
            an.SetBool("Move",true);
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
    public void Flip()
    {
        isWalkingR = !isWalkingR;
        Vector3 theScale = transform.localScale;
        theScale.x *=-1;
        transform.localScale = theScale; 
    }
    private void Check()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position,detectionRadius,whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position,detectionRadius,whatIsGround);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position,detectionRadius,whatIsGround);

    }
}
