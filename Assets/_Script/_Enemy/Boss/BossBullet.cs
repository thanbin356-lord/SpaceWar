using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float damage = 1;
    void OnBecameInvisible()
    {
        Debug.Log("BossBullet invisible: " + gameObject.name);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable idamageable = col.GetComponent<IDamageable>();
        if (idamageable != null && col.CompareTag("Player"))
        {
            idamageable.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
