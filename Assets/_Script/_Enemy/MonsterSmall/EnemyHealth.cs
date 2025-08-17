using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    float damage = 1;
    public float maxHealth;
    private float currentHealth;
    private Animator animator;
    private Rigidbody2D rb;

    public float Health
    {
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
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
        animator.SetTrigger("Explore");
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    float IDamageable.Health { get; set; }
    public void Heal(float amount)
    {
        Health = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        animator.SetTrigger("TakeDmg");
    }
    public void Destroyer()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable idamageable = col.GetComponent<IDamageable>();
        if (idamageable != null && col.tag == "Player")
        {
            idamageable.OnHit(damage);
        }
    }
    void Update()
    {

    }
}
