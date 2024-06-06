using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_Move : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float moveSpeed;
    public float cool=1f;
    public float shootCoolDown;
    public float something;
    public bool isReload;
    public bool inFocus;
    public bool inRange;
    private float timer;
    public Enemy4_Stats stats;
    public Transform rightBound,leftBound;
    private Bullet bullet;
    public Transform target;
    public GameObject actionZone,triggerZone;
    private Vector2 targetPosition;
    public Animator an;
    void Start()
    {
        stats = GetComponentInParent<Enemy4_Stats>();
        an = GetComponent<Animator>();
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        shootCoolDown-=Time.deltaTime;
        cool -=Time.deltaTime;
        if(isReload)
        {
            return;
        shoot();
        Move();
        }
        if(!inFocus)
        {
            Move();
        }
        else
        {
            EnemyFocus();
            if(inRange)
            {
                if(!stats.isStunned)
                    Attack();
            }
        }
        if(stats.isStunned){
            Stunned();
            return;
        SelectTarget();
        }
        
        if(!InsideOfBounds()&& !inRange && !an.GetCurrentAnimatorStateInfo(0).IsName("Enemy4_Shot"))
        {
            SelectTarget();
        }
    }
    public void StopAttack()
    {
        an.SetBool("isShot", false);
    }
    public void CoolDown()
    {
    isReload = true;
    an.SetBool("isReload",true);
    StartCoroutine(ReloadCooldownCoroutine(2f));
    }

    IEnumerator ReloadCooldownCoroutine(float cooldownTime)
    {
    yield return new WaitForSeconds(cooldownTime);
    isReload = false;
    an.SetBool("isReload", false);
    shootCoolDown = 7f;
    }
    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position,leftBound.position);
        float distanceToRight = Vector2.Distance(transform.position,rightBound.position);
        if(stats.isStunned)
        {
            if(transform.localScale.x <0)
            {
                target =leftBound;
            }
            else
            {
                target =rightBound;
            }
        }
        else
        {
            if(distanceToLeft >distanceToRight)
            {
                target = leftBound;
            }
            else 
            {
                target = rightBound;
            }
            moveSpeed = 1f;
            Flip();
        }
    }
    public void Move()
    {
        
        an.SetBool("walk", true);
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("Enemy4_Shot"))
        {
            targetPosition = new Vector2(target.transform.position.x,something);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

    }
    IEnumerator StunnedCoolDownCoroutine(float StunnedCoolDown)
    {
        yield return new WaitForSeconds(StunnedCoolDown);
        moveSpeed =1f;
    }
    public void Stunned()
    {
        an.SetBool("isShot",false);
        an.SetBool("isReload",false);
        an.SetBool("walk",false);
        moveSpeed =0f;
        StartCoroutine(StunnedCoolDownCoroutine(2.5f));
    }
    public void Attack()
    {
        if(shootCoolDown >= 0)
        {
            an.SetBool("isReload",false);    
            an.SetBool("isShot", true);
            shoot();
            if(!inRange)
            {
                an.SetBool("isShot",false);
            }
        }
        else
        {
            StopAttack();
            CoolDown();
        }
    }
    private bool InsideOfBounds()
    {
        return transform.position.x>leftBound.position.x && transform.position.x < rightBound.position.x;
    }
    public void Flip()
    {
       if(!stats.isStunned)
       {
       if (transform.position.x > target.position.x)
        {
        
            transform.localScale = new Vector2(-0.3f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector2(0.3f, 0.3f);
        }
        }
    }
    public void EnemyFocus()
    {
        moveSpeed = 0f;
        an.SetBool("walk",false);
    }
    public void shoot()
    {
            if(cool<=0)
            {
            Vector2 bulletPostion = new Vector2(firePoint.position.x,0.06f);
            GameObject bullet =Instantiate(bulletPrefab,bulletPostion,Quaternion.identity);
            bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
            cool =1f;
            }
    }

}

