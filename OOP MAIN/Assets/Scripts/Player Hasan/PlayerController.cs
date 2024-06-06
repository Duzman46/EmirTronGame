using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float climbSpeed;
    public float movementDirection;
    public float speed = 4f;
    public float jumpPower = 5f;
    public float radius;
    public float attackRate = 2f;
    float nextAttack = 0f;
    float nextShoot = 0f;
    float dashTime = 0.3f;
    public float attackDistance;
    public float damage = 25f;
    public float tempDamage;
    float nextChange = 0;
    float changeTime = 2f;

    private bool isFaceRight = true;
    bool isDashing = false;
    bool isGrounded;
    bool sjump = true;
    bool canClimbR = false;
    bool canDash = true;
    bool isEmirHoca = true;
    public bool isBlocking = false;
    bool invIsActive = false;

    Rigidbody2D rb;

    public Transform attackPoint;

    public LayerMask enemyLayers;
    public LayerMask layer;

    public GameObject circle, circleR, arrow, shootPointObj, cameraa, bow, sword;
    public GameObject inventory;

    Animator anim;

    //public Text currentXPText;

    public Vector2 shootDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //currentXPText.text = Experience.instance.currentExperience.ToString() + "/" + Experience.instance.expToNextLevel.ToString();
    }


    void Update()
    {
        if (GetComponent<PlayerHealth>().isDying == true)
            return;

        if (isBlocking || isDashing)
            return;
        StartCoroutine(block());
        wallClimb();
        CheckDirection();
        jump();
        surfaceCheck();
        checkAnimatoins();
        dashCheck();
        AttackKnt();
        characterChange();
        InventoryIsActive();
        arrowCheck();
    }

    private void FixedUpdate()
    {
        if (PlayerHealth.instance.isDying == true)
            return;
        if (isBlocking||isDashing)
            return;
        Movement();
    }

    public void characterChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Time.time > nextChange)
            {
                isEmirHoca = !isEmirHoca;
                nextChange = Time.time + 1f / changeTime;
            }
        }
    }

    public void dashCheck()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && !canClimbR)
        {
            Debug.Log("dashCheck giriyor");
            StartCoroutine(dash());
        }
    }

    

    public void Attack()
    {
        if (isEmirHoca)
            tempDamage = damage;
        if (!isEmirHoca)
            tempDamage = damage - 10;

        anim.SetBool("atk1", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(tempDamage);
            //currentXPText.text = Experience.instance.currentExperience.ToString() + "/" + Experience.instance.expToNextLevel.ToString();
        }
        StartCoroutine(Count());
    }

    private void AttackKnt()
    {
        if (Time.time > nextAttack && isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                if (isEmirHoca)
                    nextAttack = Time.time + 1f / attackRate;
                else
                    nextAttack = Time.time + 1f / (attackRate * 1.5f);
            }
        }
    }

    public void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        if (isEmirHoca)
        {
            anim.SetBool("isEmir", true);
            rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
            anim.SetFloat("run", Math.Abs(movementDirection * speed));

        }
        else
        {
            anim.SetBool("isEmir", false);
            rb.velocity = new Vector2(movementDirection * speed * 1.4f, rb.velocity.y);
            anim.SetFloat("run", Math.Abs(movementDirection * speed));
        }
    }

    void checkAnimatoins()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    public void CheckDirection()
    {
        if (movementDirection != 0)
        {
            if (isFaceRight && movementDirection < 0)
                Flip();
            else if (!isFaceRight && movementDirection > 0)
                Flip();
        }
    }

    public void Flip()
    {
        isFaceRight = !isFaceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //transform.Rotate(0f,180f,0f);
    }

    public void jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            sjump = true;
            canDash = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && sjump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                sjump = false;
            }
        }
    }

    public void wallClimb()
    {
        if (canClimbR)
        {
            if (Input.GetKey(KeyCode.W))
                rb.velocity = new Vector2(0, climbSpeed);
            else if (Input.GetKeyDown(KeyCode.Space))
                rb.velocity = new Vector2(2, 2);
            sjump = true;
            canDash = true;
        }
    }

    public void surfaceCheck()
    {
        isGrounded = Physics2D.OverlapCircle(circle.transform.position, radius, layer);
        canClimbR = Physics2D.OverlapCircle(circleR.transform.position, radius, layer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(circle.transform.position, radius);
        Gizmos.DrawWireSphere(circleR.transform.position, radius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }
    private IEnumerator dash()
    {
            canDash = false;
            rb.gravityScale = 0f;
            isDashing = true;
            rb.velocity = new Vector2(transform.localScale.x * 120, 0f);
            yield return new WaitForSeconds(dashTime / 2);
            isDashing = false;
            rb.gravityScale = 1f;
    }

    IEnumerator block()
    {
        if (isEmirHoca && isGrounded && Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            rb.velocity = Vector2.zero;
            anim.SetBool("isBlocking", true);
            yield return new WaitForSeconds(0.5f);
            isBlocking = false;
            anim.SetBool("isBlocking", false);
        }
    }

    /*public IEnumerator block()
    {
        isBlocking = true;
        anim.SetBool("isBlocking",true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.8f);
        isBlocking = false;
        canBlock = false;
        anim.SetBool("isBlocking", false);
        yield return new WaitForSeconds(3);
        canBlock = true;
    }*/

    void arrowCheck()
    {
        if (!isEmirHoca && Input.GetMouseButtonDown(1) && Time.time > nextShoot)
        {
            tempDamage -= 10;
            sword.SetActive(false);
            bow.SetActive(true);
            anim.SetBool("arrow", true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (!isFaceRight && (mousePosition - shootPointObj.transform.position).normalized.x > 0)
                Flip();
            else if (isFaceRight && (mousePosition - shootPointObj.transform.position).normalized.x < 0)
            {
                Flip();
            }
            Vector3 shootPoint = shootPointObj.transform.position;
            shootDirection = (mousePosition - shootPoint).normalized;
            if (transform.localScale.x > 0)
            {
                arrow.transform.localScale = new Vector3(-0.05f, 0.05f, 0.05f);
            }
            else
                arrow.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            Instantiate(arrow, shootPoint, Quaternion.identity);
            nextShoot = Time.time + 0.5f;
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("arrow", false);
        anim.SetBool("atk1", false);
        bow.SetActive(false);
        sword.SetActive(true);
    }

    public void InventoryIsActive()
    {
        if (Input.GetKeyDown(KeyCode.E) && !invIsActive)
        {
            inventory.SetActive(true);
            invIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && invIsActive)
        {
            inventory.SetActive(false);
            invIsActive = false;
        }
    }

    public void characterThrow(float speed)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + speed);
    }
}