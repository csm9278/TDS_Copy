using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Truck : MonoBehaviour
{
    public static Truck inst;

    [SerializeField]
    TruckWheel[] truckWheels;

    [SerializeField]
    Transform frontEnemyCheckPoint;
    LayerMask enemyLayer;

    List<Box> boxList = new List<Box>();
    Player player;

    [SerializeField]
    Transform firstTr;

    public int frontEnemyNum = 0;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy0", "Enemy1", "Enemy2");
        Box[] boxes = GetComponentsInChildren<Box>();
        player = GetComponentInChildren<Player>();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].transform.localPosition = new Vector2(0, (1.5f * i));
            boxList.Add(boxes[i]);
        }

    }
    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(frontEnemyCheckPoint.position, Vector2.right, 1.5f, enemyLayer);
        frontEnemyNum = hits.Length;
        if(frontEnemyNum > 0)
            for(int i = 0; i < truckWheels.Length; i++)
                truckWheels[i].ChangeWheelSpeed(frontEnemyNum);
    }

    public void BrokenBox(Box box)
    {
        boxList.Remove(box);
        SoundManager.instance.PlayEffSound(SoundManager.instance.soundClip[(int)SoundsType.BrokenBox], 0.25f);

        Destroy(box.gameObject);

        for(int i = 0; i < boxList.Count; i++)
            boxList[i].transform.DOLocalMove(new Vector2(0, (1.5f * i)), 0.3f);

        player.transform.DOLocalMove(new Vector2(0, (1.5f * boxList.Count)), 0.3f);
    }
}
