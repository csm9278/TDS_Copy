using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("--- Attack ---")]
    [SerializeField]
    float attackTime = 0.5f;
    float curAttackTime = 0.5f;
    [SerializeField]
    Transform attackPos;
    [SerializeField]
    GameObject gunObject;
    [SerializeField]
    Transform autoAttackRayPoint;
    LayerMask enemylayer;

    int attackNum = 5;


    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        enemylayer = LayerMask.GetMask("Enemy0", "Enemy1", "Enemy2");
        curAttackTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBulletTime();
        if (Input.GetMouseButton(0))
            CheckMousePoint();
        else
            AutoAttack();
    }

    void CheckMousePoint()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        LookWeapon(mousePos);
    }

    void AutoAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(autoAttackRayPoint.position, Vector2.right, 15.0f, enemylayer);

        if(hit)
            LookWeapon(hit.collider.transform.position);
    }

    void CheckBulletTime()
    {
        if (curAttackTime >= 0.0f)
            curAttackTime -= Time.deltaTime;

        if(curAttackTime <= 0.0f)
        {
            StartCoroutine(AttackCo());

            curAttackTime = attackTime;
        }
    }

    IEnumerator AttackCo()
    {
        for (int i = 0; i < attackNum; i++)
        {
            GameObject bobj = MemoryPoolManager.instance.GetObject("Bullet");
            bobj.transform.position = attackPos.position;
            Vector3 rot = gunObject.transform.eulerAngles;

            rot.z += Random.Range(28.0f, 38.0f);
            bobj.transform.eulerAngles = rot;
            yield return new WaitForFixedUpdate();
        }
    }

    void LookWeapon(Vector3 targetPoint)
    {
        Vector2 dir = (targetPoint - this.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 33.0f;
        gunObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
