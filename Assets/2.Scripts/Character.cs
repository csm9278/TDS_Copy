using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Character : MemoryPoolObject
{
    [Header("--- Character ---")]
    [SerializeField]
    float hp = 100;
    float curHp;

    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Transform damageTextTr = null;
    

    public virtual void TakeDamage(int value)
    {
        if (!hpSlider.gameObject.activeSelf)
            hpSlider.gameObject.SetActive(true);
        ShowDamageText(value);
        curHp -= value;

        if (curHp <= 0)
        {
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

    void ShowDamageText(int value)
    {
        if (damageTextTr == null)
            return;

        GameObject damageText = MemoryPoolManager.instance.GetObject("DamageText");

        if(damageText != null)
        if (damageText.TryGetComponent(out DamageText dmgtext))
            dmgtext.SetDamageText(value, damageTextTr);
    }

    public virtual void DieEffect()
    {

    }
}
