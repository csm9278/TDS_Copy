using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("--- Attack ---")]
    [SerializeField]
    float attackTime = 0.5f;
    float curAttackTime = 0.5f;
    bool isAutoAttack = false;
    bool autoTargeting = false;
    [SerializeField]
    Transform attackPos;
    [SerializeField]
    GameObject gunObject;
    [SerializeField]
    Transform autoAttackRayPoint;
    LayerMask enemylayer;

    int attackNum = 5;
    Animation _animation;

    //Upgrade
    int shotSpeed = 0;
    int shotDamage = 0;
    int shotCount = 0;
    int crit = 0;
    int doubleChange = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        _animation = GetComponentInChildren<Animation>();
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
        isAutoAttack = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        LookWeapon(mousePos);
    }

    void AutoAttack()
    {
        isAutoAttack = true;
        RaycastHit2D hit = Physics2D.Raycast(autoAttackRayPoint.position, Vector2.right, 15.0f, enemylayer);

        if (hit)
            LookWeapon(hit.collider.transform.position);
        else
            autoTargeting = false;
    }

    void CheckBulletTime()
    {
        if (curAttackTime >= 0.0f)
            curAttackTime -= Time.deltaTime * (1.0f +(shotSpeed * 0.1f));

        if(curAttackTime <= 0.0f)
        {
            if(!isAutoAttack)
            {
                StartCoroutine(AttackCo());
                curAttackTime = attackTime;
            }
            else
            {
                if(autoTargeting)
                {
                    StartCoroutine(AttackCo());
                    curAttackTime = attackTime;
                }
            }

        }
    }

    IEnumerator AttackCo()
    {
        SoundManager.instance.PlayEffSound(SoundManager.instance.soundClip[(int)SoundsType.GunShot], 0.25f);
        _animation.Play();

        int shotNum = attackNum + shotCount;

        int checkDouble = Random.Range(0, 101);
        if (doubleChange * 5 > checkDouble)
            shotCount *= 2;

        for (int i = 0; i < shotNum; i++)
        {
            GameObject bobj = MemoryPoolManager.instance.GetObject("Bullet");
            bobj.transform.position = attackPos.position;
            Vector3 rot = gunObject.transform.eulerAngles;

            if (bobj.TryGetComponent(out Bullet bull))
                bull.SetBullet(5 + shotDamage, crit);

            rot.z += Random.Range(28.0f, 38.0f);
            bobj.transform.eulerAngles = rot;
            yield return new WaitForFixedUpdate();
        }
    }

    void LookWeapon(Vector3 targetPoint)
    {
        autoTargeting = true;
        Vector2 dir = (targetPoint - this.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 33.0f;
        gunObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void DieEffect()
    {
        GameManager.inst.PlayerDie();
    }

    public void GetUpgrade(UpgradeType type)
    {
        switch(type)
        {
            case UpgradeType.ShotSpeed:
                shotSpeed++;
                break;

            case UpgradeType.ShotDamage:
                shotDamage++;
                break;

            case UpgradeType.ShotCount:
                shotCount++;
                break;

            case UpgradeType.CritShot:
                crit++;
                break;

            case UpgradeType.DoubleChance:
                doubleChange++;
                break;
        }
    }
}
