using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthBar;
    Animator anim;
    bool isImmune;
    public bool isDying=false;

    public float immunityTime;
    public float defense = 10;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", currentHealth);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(death());
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&& !isImmune)
        {
            StartCoroutine(Immunity());
        }
    }

    public void TakeDamage(float a)
    {
        if(PlayerController.Instance.isBlocking)
            currentHealth -= a * ((defense + 40) / 100);
        else
            currentHealth -= a * (defense / 100);
        StartCoroutine(Immunity());
    }

    public IEnumerator death()
    {
        isDying = true;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        Respawn();
    }

    IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }

    public void Respawn()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y") && PlayerPrefs.HasKey("z"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            transform.position = Vector3.zero;
        }

        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        anim.SetBool("isDead", false);
        isDying = false;
    }
}
