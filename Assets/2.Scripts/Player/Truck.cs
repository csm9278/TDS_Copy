using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Truck : MonoBehaviour
{
    public static Truck inst;

    List<Box> boxList = new List<Box>();
    Player player;

    [SerializeField]
    Transform firstTr;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Box[] boxes = GetComponentsInChildren<Box>();
        player = GetComponentInChildren<Player>();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.localPosition = new Vector2(0, (1.5f * i));
            boxList.Add(boxes[i]);
        }
    }

    public void BrokenBox(Box box)
    {
        boxList.Remove(box);
        Destroy(box.gameObject);

        for(int i = 0; i < boxList.Count; i++)
            boxList[i].transform.DOLocalMove(new Vector2(0, (1.5f * i)), 0.3f);

        player.transform.DOLocalMove(new Vector2(0, (1.5f * boxList.Count)), 0.3f);
    }

}
