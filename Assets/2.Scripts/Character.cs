using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Character : MemoryPoolObject
{
    [Header("--- Character ---")]
    [SerializeField]
    float hp = 100;
    float curHp;

    [SerializeField]
    Slider hpSlider;

    public virtual void TakeDamage(int value)
    {
        if (!hpSlider.gameObject.activeSelf)
            hpSlider.gameObject.SetActive(true);
        curHp -= value;

        if (curHp <= 0)
        {
            Debug.Log(curHp);
            curHp = 0;
            DieEffect();
        }

        hpSlider.value = curHp / hp;
    }

    public virtual void InitCharacter()
    {
        curHp = hp;
    }

    public virtual void ResetCharacter()
    {
        curHp = hp;
        hpSlider.gameObject.SetActive(false);
    }

    public virtual void DieEffect()
    {

    }
}
