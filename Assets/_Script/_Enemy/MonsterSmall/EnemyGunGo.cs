using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunGo : MonoBehaviour
{
     AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public GameObject EnemyBulletGo;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartFire), 1f);
    }

    void StartFire()
    {
        StartCoroutine(FireEnemyBullet());
    }
    IEnumerator FireEnemyBullet()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        while (true)
        {
            if (playerShip != null)
            {
                int Randomshot = Random.Range(0, 2);
                switch (Randomshot)
                {
                    case 0: break;
                    case 1:
                        GameObject bullet = (GameObject)Instantiate(EnemyBulletGo);
                        bullet.transform.position = transform.position;
                        Vector2 direction = playerShip.transform.position - bullet.transform.position;
                        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
                        audioManager.PlaySFX(audioManager.bossshooting);
                        break;

                }
            }
            yield return new WaitForSeconds(4f);
        }
    }
}
