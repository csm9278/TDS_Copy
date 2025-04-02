using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    SpriteRenderer[] _spRenderer;
    [SerializeField]
    Transform frontRayTr,backRayTr, topRayTr, bottomRayTr;

    Rigidbody2D rb;
    [SerializeField]
    float speed = 2.0f;

    LayerMask layer;
    LayerMask WallLayer;
    LayerMask AttackLayer;
    Vector2 moveVec = Vector2.left;
    float checkJumpTimer = 0.1f;

    bool isMove = true;
    bool isBack = false;
    bool checkAttack = false;
    float groundY;

    Animator _animator;
    Character attackTarget;

    [SerializeField]
    bool checkVelocity = false;

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        WallLayer = LayerMask.GetMask("Wall");
        AttackLayer = LayerMask.GetMask("Attackable");
        moveVec.y = rb.velocity.y;
        StartCoroutine(CheckGroundY());
    }

    private void FixedUpdate()
    {
        CheckFront();
        Move();
    }

    public void SetOrder(int order)
    {
        for (int i = 0; i < _spRenderer.Length; i++)
            _spRenderer[i].sortingOrder += order * 10;

        string layerName = "Enemy" + order.ToString();
        gameObject.layer = LayerMask.NameToLayer(layerName);
        layer = LayerMask.GetMask(layerName);
    }

    void Move()
    {
        if (!isMove)
            return;

        rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);
    }

    IEnumerator CheckGroundY()
    {
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            if (rb.velocity.y == 0)
            {
                groundY = this.transform.position.y;
                yield break;
            }


            yield return new WaitForFixedUpdate();
        }
    }

    void CheckFront()
    {
        //앞에 좀비가 있을 시
        RaycastHit2D hit = Physics2D.BoxCast(frontRayTr.position, new Vector2(0.1f, .8f), 0, Vector3.left, 0, layer);
        if (hit)
        {
            isMove = false;
            //뒤, 위에 좀비가 없을 시 점프
            RaycastHit2D backHit = Physics2D.BoxCast(backRayTr.position, new Vector2(0.5f, .8f), 0, Vector3.right, 0.1f, layer);
            RaycastHit2D topHit = Physics2D.BoxCast(topRayTr.position, new Vector2(0.6f, 0.1f), 0, Vector2.up, 0.3f, layer);
            if (!backHit && !topHit)
            {
                CheckJumpTimer();
            }
        }
        else
            isMove = true;

        //앞에 플레이어가 있을 시
        if (isBack)
            return;
        RaycastHit2D hitBox = Physics2D.Raycast(frontRayTr.position, Vector3.left, .1f, WallLayer);
        if (hitBox)
        {
            if (attackTarget == null)
            {
                RaycastHit2D hitTarget = Physics2D.Raycast(frontRayTr.position, Vector3.left, 0.5f, AttackLayer);
                if(hitTarget)
                    if(hitTarget.collider.TryGetComponent(out Character target))
                        attackTarget = target;
            }

            isMove = false;
            checkAttack = true;
            _animator.SetBool("IsAttacking", true);
            CheckGround();
        }
        else
        {
            attackTarget = null;
            checkAttack = false;
            _animator.SetBool("IsAttacking", false);
        }
    }

    void CheckGround()
    {
        if (this.transform.position.y > groundY && checkAttack)    //공중에 있을 시
        {
            //RaycastHit2D hit = Physics2D.BoxCast(bottomRayTr.position, new Vector2(0.6f, 0.1f), 0, Vector2.down, 0.3f, layer);
            RaycastHit2D hit = Physics2D.Raycast(bottomRayTr.position, Vector2.down, 0.1f, layer);
            if (hit)
            {
                if (hit.collider.TryGetComponent(out Enemy enemy))
                    enemy.MoveBack();
            }
                
        }
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.1f)
            return;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 7.0f, ForceMode2D.Impulse);

        float maxJumpVelocity = 4.0f;
        if (rb.velocity.y >= maxJumpVelocity)
            rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);
        checkJumpTimer = 0.1f;
    }

    void MoveBack()
    {
        if (isBack)
            return;

        isBack = true;
        StartCoroutine(moveBackCO());
    }

    IEnumerator moveBackCO()
    {
        isMove = true;
        moveVec.x = .8f;
        rb.mass = 10.0f;

        while(true)
        {
            RaycastHit2D hit = Physics2D.Raycast(frontRayTr.position, Vector3.left, 10f, layer);
            if (hit)
                break;
            yield return new WaitForFixedUpdate();
        }

        isBack = false;
        rb.mass = 1;
        moveVec.x = -1;
    }

    void CheckJumpTimer()
    {
        if(checkJumpTimer >= 0.0f)
            checkJumpTimer -= Time.deltaTime;

        if (checkJumpTimer <= 0.0f)
            Jump();
    }

    void OnAttack()
    {
        if (attackTarget != null)
            attackTarget.TakeDamage(2);
    }

    public override void DieEffect()
    {
        ObjectReturn();
    }

    public override void ResetObject()
    {
        ResetCharacter();
    }
}
