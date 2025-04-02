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
    TMP_Text damageText;


    [SerializeField]
    Slider hpSlider;

    public virtual void TakeDamage(int value)
    {
        if (!hpSlider.gameObject.activeSelf)
            hpSlider.gameObject.SetActive(true);
        curHp -= value;
        if (damageText != null && !damageText.gameObject.activeSelf)
            StartCoroutine(ShowDamageText());

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

    IEnumerator ShowDamageText()
    {
        damageText.gameObject.SetActive(true);

        Vector3 originPos = damageText.transform.position;
        Vector3 targetPos = originPos;
        targetPos.y += 0.2f;
        yield return damageText.transform.DOMove(targetPos, 0.2f).WaitForCompletion();
        yield return damageText.DOColor(Color.clear, 0.15f).WaitForCompletion();

        damageText.color = Color.white;
        damageText.transform.position = originPos;
        damageText.gameObject.SetActive(false);
    }
    public virtual void DieEffect()
    {

    }
}
