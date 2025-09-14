using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    float damage = 1;
    public float maxHealth;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[BossHealth] Initialized with HP={currentHealth}");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable idamageable = col.GetComponent<IDamageable>();
        if (idamageable != null && col.tag == "Player")
        {
            idamageable.OnHit(damage);
        }
    }
}
