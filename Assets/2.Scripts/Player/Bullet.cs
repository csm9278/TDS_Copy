using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MemoryPoolObject
{
    [SerializeField]
    float bulletSpeed = 4.0f;

    LayerMask enemyLayer;

    public override void InitObject()
    {
        enemyLayer = LayerMask.GetMask("Enemy0", "Enemy1", "Enemy2");
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        this.transform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer & (1 << collision.gameObject.layer)) != 0)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(5);
                ObjectReturn();
            }
        }

    }
}
