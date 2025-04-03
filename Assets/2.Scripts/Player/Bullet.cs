using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MemoryPoolObject
{
    [SerializeField]
    float bulletSpeed = 6.0f;
    bool isDamaged = false;

    TrailRenderer trail;
    float lifeTime = 4.0f;

    LayerMask enemyLayer;

    int critPercent = 0;
    int damage = 5;

    public override void InitObject()
    {
        enemyLayer = LayerMask.GetMask("Enemy0", "Enemy1", "Enemy2");
        trail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void Update()
    {
        CheckLifeTime();
    }

    void MoveBullet()
    {
        this.transform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);
    }

    public override void ResetObject()
    {
        isDamaged = false;
        lifeTime = 4.0f;
        trail.Clear();
    }

    public void SetBullet(int dmg, int critUpgrade)
    {
        critPercent = critUpgrade * 10;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer & (1 << collision.gameObject.layer)) != 0)
        {
            if (isDamaged)
                return;

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                isDamaged = true;
                int checkCrit = Random.Range(0, 101);
                if (critPercent > checkCrit)
                    damage *= 2;
                enemy.TakeDamage(damage);
                ObjectReturn();
            }
        }
    }

    void CheckLifeTime()
    {
        if (lifeTime >= 0.0f)
            lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
            ObjectReturn();
    }
}
