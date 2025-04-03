using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageText : MemoryPoolObject
{
    public TMPro.TMP_Text dmgText;

    public void SetDamageText(int dmgValue, Transform spawnTr)
    {
        this.transform.position = spawnTr.position;

        if (dmgValue != 0)
            dmgText.text = dmgValue.ToString();

        dmgText.color = Color.white;
        StartCoroutine(DamageTextCo());
    }

    public IEnumerator DamageTextCo()
    {
        Vector3 origin = this.transform.localPosition;
        Vector3 center = this.transform.localPosition;
        center.y += 150f;
        center.x += Random.Range(-100.0f, 100.0f);
        Vector3 target = center;
        target.y -= 500.0f;
        target.x *= 1.3f;
        float step = 0;

        dmgText.transform.DOScale(1.3f, .3f).OnComplete(() =>
        {
            dmgText.transform.DOScale(0.5f, 0.7f);
            dmgText.DOFade(0.0f, 0.7f);
        });

        while (step < 1)
        {
            step += Time.deltaTime;
            this.transform.localPosition = BesierCurve(origin, center, target, step);

            yield return new WaitForEndOfFrame();
        }


        ObjectReturn();
    }

    Vector3 BesierCurve(Vector3 originPos, Vector3 center, Vector3 targetPos, float lerp)
    {
        Vector3 v1 = Vector3.Lerp(originPos, center, lerp);
        Vector3 h1 = Vector3.Lerp(center, targetPos, lerp);

        Vector3 besier = Vector3.Lerp(v1, h1, lerp);

        return besier;
    }
}
