using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] _spRenderer;
    [SerializeField]
    bool isRider = false;
    [SerializeField]
    Transform rayTr;

    Rigidbody2D rb;
    [SerializeField]
    float speed = 2.0f;

    LayerMask layer;
    Vector2 moveVec = Vector2.left;
    bool isJump = false;

    float groundY;

    [SerializeField]
    bool checkVelocity = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVec.y = rb.velocity.y;
        StartCoroutine(CheckGroundY());
    }

    private void FixedUpdate()
    {
        if (checkVelocity)
        {
            Debug.Log(rb.velocity);
        }
        CheckFront();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MoveBack();
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
        rb.velocity = new Vector2(moveVec.x * speed, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) < 0.05f)
            if(this.transform.position.y > groundY)
            MoveLowObj();
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
        RaycastHit2D[] hit = Physics2D.RaycastAll(rayTr.position, Vector3.left, .5f, layer);
        Debug.DrawRay(rayTr.position, Vector3.left * .5f, Color.red);
        if (hit.Length > 1)
            Jump();
    }

    void CheckGround()
    {
        if (this.transform.position.y > groundY)    //공중에 있을 시
        {

        }
        else
            isJump = false;
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.1)
            return;

        isJump = true;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 7.0f, ForceMode2D.Impulse);
    }

    void MoveBack()
    {
        Vector2 movePos = this.transform.position;
        movePos.x -= 1.0f;
        rb.MovePosition(movePos);
    }

    void MoveLowObj()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(rayTr.position, Vector3.left, .5f, layer);
        Debug.DrawRay(rayTr.position, Vector3.left * .5f, Color.red);
        if (hit.Length > 1)
            Jump();
    }
}
