using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Transform spawnPos;
    [SerializeField]
    float maxGenerateTime = 0.5f;
    float generateTime = 0.5f;
    [SerializeField]
    GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(generateTime >= 0.0f)
        {
            generateTime -= Time.deltaTime;
            if(generateTime < 0.0f)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyObj = MemoryPoolManager.instance.GetObject("Enemy");
        //GameObject enemyObj = Instantiate(enemyPrefab);

        int posRand = Random.Range(0, 3);

        Vector3 pos = spawnPos.position;
        pos.y -= posRand * 0.25f;
        enemyObj.transform.position = pos;

        if(enemyObj.TryGetComponent(out Enemy enemy))
            enemy.SetOrder(posRand);

        generateTime = maxGenerateTime;
    }
}
