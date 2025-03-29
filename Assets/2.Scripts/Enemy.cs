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
    float speed = 150.0f;
    LayerMask layer;
    Vector3 moveVec = Vector3.left;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(speed - 20f, speed + 20f);
    }

    private void FixedUpdate()
    {
        Move();
        CheckFront();
    }

    // Update is called once per frame
    void Update()
    {
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
        rb.velocity = moveVec * Time.fixedDeltaTime * speed;
    }

    void CheckFront()
    {
        //if (!isRider)
        //    return;

        RaycastHit2D[] hit = Physics2D.RaycastAll(rayTr.position, Vector3.left, .5f, layer);
        Debug.DrawRay(rayTr.position, Vector3.left * .5f, Color.red);
        if(hit.Length > 1)
        {
            for (int i = 0; i < hit.Length; i++)
                Debug.Log(hit[i].collider.gameObject.name);
            rb.AddForce(Vector3.up * 100.0f);
        }
    }
}
