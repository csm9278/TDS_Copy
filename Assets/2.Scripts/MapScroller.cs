using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [SerializeField]
    float maxScrollSpeed = 10;
    float scrollSpeed;

    [SerializeField]
    GameObject[] objects;
    [SerializeField]
    float maxValue, minValue;
    [SerializeField]
    float scrollValue = .3f;
    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = maxScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        scrollSpeed = maxScrollSpeed - (2.5f * Truck.inst.frontEnemyNum);
        if (scrollSpeed <= 0)
            scrollSpeed = 0;

        for(int i = 0; i < objects.Length; i++)
        {
            objects[i].gameObject.transform.Translate(Vector2.left * scrollSpeed * scrollValue * Time.deltaTime);

            if (objects[i].transform.position.x < minValue)
                objects[i].transform.position = new Vector2(maxValue, 0);
        }
    }
}
