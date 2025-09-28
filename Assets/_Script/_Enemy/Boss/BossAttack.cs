using System;
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Phase Controllers Bullet ")]
    public GameObject EnemyBulletGo;
    public GameObject EnemyBulletGo2;
    public Transform firePoint;
    public int bulletCount = 16;
    public float bulletSpeed = 5f;
    public float fireRate = 0.3f;
    BossHealth bossHealth;
    private bool isPhase2 = false;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        Invoke(nameof(StartFire), 7f);
        bossHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>();
    }
    void Update()
    {
        if (!isPhase2 && bossHealth.CurrentHealth <= bossHealth.maxHealth / 2)
        {
            bulletCount = 20;
            fireRate = 0.05f;
            bulletSpeed = 15f;
            isPhase2 = true;
            StopAllCoroutines();
            Invoke(nameof(StartFire), 5f);
        }
    }
    void StartFire()
    {
        StartCoroutine(AttackPatterns());
    }

    IEnumerator AttackPatterns()
    {
        while (bossHealth.CurrentHealth > 0) // loop vô tận
        {
            int patternIndex = UnityEngine.Random.Range(0, 5);

            switch (patternIndex)
            {
                case 0:
                    yield return StartCoroutine(CircleShot());
                    yield return new WaitForSeconds(4f);
                    break;

                case 1:
                    yield return StartCoroutine(SpiralShot());
                    yield return new WaitForSeconds(4f);
                    break;

                case 2:
                    yield return StartCoroutine(FanShot());
                    yield return new WaitForSeconds(4f);
                    break;

                case 3:
                    yield return StartCoroutine(AimedShot());
                    yield return new WaitForSeconds(4f);
                    break;

                case 4:
                    yield return StartCoroutine(RainShot());
                    yield return new WaitForSeconds(5f);
                    break;

                default:
                    yield return null;
                    break;
            }
        }
    }

    // ---------------- PATTERNS ----------------

    // 1. Circle shot (bắn vòng cung toàn màn)
    IEnumerator CircleShot()
    {
        float angleStep = 360f / bulletCount;

        for (int wave = 0; wave < 6; wave++)
        {
            float angle = 0f;

            for (int i = 0; i < bulletCount; i++)
            {
                float bulDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 bulDir = new Vector2(bulDirX, bulDirY).normalized;
                audioManager.PlaySFX(audioManager.bossshooting);
                SpawnBullet(bulDir);
                angle += angleStep;
            }

            yield return new WaitForSeconds(fireRate); // nghỉ giữa các vòng
        }
    }

    // 2. Spiral shot (xoắn ốc)
    IEnumerator SpiralShot()
    {
        float angle = 0f;
        for (int i = 0; i < 50; i++)
        {
            float bulDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulDir = new Vector2(bulDirX, bulDirY).normalized;
            audioManager.PlaySFX(audioManager.bossshooting);
            SpawnBullet(bulDir);
            angle += 15f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // 3. Fan shot (bắn hình quạt)
    IEnumerator FanShot()
    {
        for (int wave = 0; wave < 6; wave++)
        {
            for (int j = -9; j <= 0; j++)
            {
                float angle = j * 20f;
                float bulDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 bulDir = new Vector2(bulDirX, bulDirY).normalized;
                audioManager.PlaySFX(audioManager.bossshooting);
                SpawnBullet(bulDir);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // 4. Aimed shot (bắn vào player)
    IEnumerator AimedShot()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) yield break;

        for (int i = 0; i < 15; i++)
        {
            audioManager.PlaySFX(audioManager.bossshooting);
            Vector2 dir = (player.transform.position - firePoint.position).normalized;
            SpawnBullet(dir);
            yield return new WaitForSeconds(0.2f);
        }
    }

    // 5. Rain shot (mưa đạn từ trên xuống)
    IEnumerator RainShot()
    {
        for (int i = 0; i < 30; i++)
        {
            float xPos = UnityEngine.Random.Range(-8f, 8f);
            Vector3 spawnPos = new Vector3(xPos, firePoint.position.y, 0);

            Vector2 dir = Vector2.down;
            GameObject prefab = bossHealth.CurrentHealth <= bossHealth.maxHealth / 2
                ? EnemyBulletGo2
                : EnemyBulletGo;
            audioManager.PlaySFX(audioManager.bossshooting);
            GameObject b = Instantiate(prefab, spawnPos, Quaternion.identity);
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.linearVelocity = dir * bulletSpeed;
            float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            b.transform.rotation = Quaternion.Euler(0, 0, angleDeg + 90f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // ---------------- UTILITY ----------------
    void SpawnBullet(Vector2 dir)
    {
        GameObject prefab = bossHealth.CurrentHealth <= bossHealth.maxHealth / 2
            ? EnemyBulletGo2
            : EnemyBulletGo;

        GameObject b = Instantiate(prefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * bulletSpeed;
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0, 0, angleDeg + 90f);
    }

}
