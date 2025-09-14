using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float xLimit = 6f; // giới hạn di chuyển trái/phải
    public float yTarget = 3f; // vị trí y mà boss sẽ dừng lại (từ trên bay xuống)
    public float centerMoveSpeed = 10f;
    private bool reachedTarget = false;
    private int moveDirection = 1; // 1 = phải, -1 = trái
    private Animator animator;
    private bool isMovingToCenter = false;

    [Header("Summon Settings")]
    public GameObject minionPrefab;
    public Transform[] summonPoints;
    public float summonCooldown = 5f;

    private bool isPhase2 = false;
    private bool isTransitioning = false;
    private bool waitingForTransition = false;
    private float nextSummonTime = 0f;

    [Header("Summon Settings")]
    BossHealth bossHealth;


    [Header("Animator Controllers")]
    public RuntimeAnimatorController phase1Controller;
    public RuntimeAnimatorController phase2Controller;
    private Collider2D col;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        HandleMovement();

        if (!isPhase2 && !isTransitioning && bossHealth.CurrentHealth <= bossHealth.maxHealth / 2)
        {
            isTransitioning = true;
            isMovingToCenter = true; // bắt đầu di chuyển về giữa màn
            Debug.Log($"[BossHealth] HP={bossHealth.CurrentHealth}, bắt đầu di chuyển về giữa màn");
        }

        if (isPhase2 && Time.time >= nextSummonTime)
        {
            SummonMinions();
            nextSummonTime = Time.time + summonCooldown;
        }
    }
    public void OnPhase2TransitionEnd()
    {
        animator.runtimeAnimatorController = phase2Controller; // đổi sang controller Phase 2
        isPhase2 = true;
        isTransitioning = false;
        waitingForTransition = false;
        col.enabled = true;
        Debug.Log("Boss đã chuyển sang Phase 2!");
    }

    void HandleMovement()
    {
        if (!reachedTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x, yTarget),
                moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - yTarget) < 0.05f)
            {
                reachedTarget = true;
                col.enabled = true;
            }
        }
        else if (isMovingToCenter)
        {
            // Di chuyển về giữa màn với tốc độ nhanh
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(0f, transform.position.y),
                centerMoveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x) < 0.05f)
            {
                isMovingToCenter = false;
                waitingForTransition = true;
                animator.SetTrigger("Transition");
                Debug.Log("Boss đã về giữa màn, bắt đầu di chuyển ngang Phase 2");
            }
        }
        else if (waitingForTransition)
        {
            // đợi animation kết thúc → boss đứng yên, không di chuyển
        }
        else
        {
            transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
            if (transform.position.x > xLimit) moveDirection = -1;
            else if (transform.position.x < -xLimit) moveDirection = 1;
        }
    }

    void SummonMinions()
    {
        Debug.Log("Boss triệu hồi quái nhỏ!");
        foreach (Transform point in summonPoints)
        {
            Instantiate(minionPrefab, point.position, Quaternion.identity);
        }
    }
}
