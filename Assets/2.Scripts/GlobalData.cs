using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    public static string[] upgradeName = { "공격력 업", "공격속도 업", "공격량 증가", "크리티컬 샷", "더블 찬스" };
    public static string[] upgradeInfo = { "공격력을 1 올립니다.", "공격속도를 10% 높입니다.", "총알을 1개 더 발사합니다.", "크리티컬 확률을 10% 올립니다.", "일정 확률로 2배의 공격을 가합니다" };
    public static Sprite[] upgradeImage = Resources.LoadAll<Sprite>("Images");
}
