using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1;
    public float speed;
    Vector2 _direction;
    bool isReady;
    void Awake()
    {
        isReady = false;
    }
    void Start()
    {

    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        isReady = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            Vector2 position = transform.position;
            position += _direction * speed * Time.deltaTime;
            transform.position = position;
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if ((transform.position.x < min.x) || (transform.position.x > max.x)
            || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            } 
        }
    }
    public void OnTriggerEnter2D(Collider2D col){
        IDamageable idamageable = col.GetComponent<IDamageable>();
        if (idamageable != null && col.tag == "Player")
        {
            idamageable.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
