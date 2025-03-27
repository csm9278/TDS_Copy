using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] _spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 3.0f);
    }

    public void SetOrder(int order)
    {
        for (int i = 0; i < _spRenderer.Length; i++)
            _spRenderer[i].sortingOrder = order;
    }
}
