using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckWheel : MonoBehaviour
{
    [SerializeField]
    float maxWheelSpeed = 400;
    float wheelSpeed;

    private void Start()
    {
        wheelSpeed = maxWheelSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, -wheelSpeed * Time.deltaTime);
    }

    public void ChangeWheelSpeed(int frontEnemy)
    {
        wheelSpeed = maxWheelSpeed - (frontEnemy * 100);
        if (wheelSpeed <= 0)
            wheelSpeed = 0;
    }
}
