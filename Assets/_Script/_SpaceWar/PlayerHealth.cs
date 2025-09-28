using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float HealthTime;
    float damage = 3;
    public float maxHealth;
    private float currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    AudioManager audioManager;
    [SerializeField] private GameOver gameOverManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public float Health
    {
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            if (currentHealth > 0)
            {
                animator.SetTrigger("TakeDmg");
            }
            if (currentHealth <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return currentHealth;
        }
    }
    public void Defeated()
    {
        animator.Play("Spaceship_Explore");
        Destroyer();
    }
    float IDamageable.Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public void Heal(float amount)
    {
        Health = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        gameObject.tag = "Untagged";
        HealthTime = Time.time + 10f;
        audioManager.PlaySFX(audioManager.gethit);
    }

    public void EnableShield()
    {
        animator.Play("Spaceship_Shield");
    }
    public void BackToIdle()
    {
        gameObject.tag = "Player";
        animator.Play("SpaceShip_Idle");
    }
    public void Destroyer()
    {
        gameObject.tag = "Untagged";
        audioManager.PlaySFX(audioManager.death);
        gameOverManager.ShowGameOverScreen();
    }
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        col = GetComponent<Collider2D>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable idamageable = col.GetComponent<IDamageable>();
        if (idamageable != null && col.tag == "Enemy")
        {
            idamageable.OnHit(damage);
            audioManager.PlaySFX(audioManager.tourch);
        }
    }
    void Update()
    {
        if (gameObject.tag == "Untagged" && Time.time >= HealthTime)
        {
            animator.Play("Spaceship_ShieldEnd");
        }
    }
}
