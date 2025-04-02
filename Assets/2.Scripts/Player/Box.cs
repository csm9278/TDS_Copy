using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Character
{
    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DieEffect()
    {
        Truck.inst.BrokenBox(this);
    }
}
