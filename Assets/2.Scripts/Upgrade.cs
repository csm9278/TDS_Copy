using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    ShotDamage,
    ShotSpeed,
    ShotCount,
    CritShot,
    DoubleChance,

    LastIdx,
}

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    TMP_Text upgradeName, upgradeInfo;
    [SerializeField]
    Image upgradeImage;
    [SerializeField]
    Button getUpgradeBtn;

    UpgradeType upgradeIdx = UpgradeType.ShotSpeed;

    private void Start()
    {
        getUpgradeBtn.onClick.AddListener(GetUpgrade);
    }

    public void SetUpgrade(UpgradeType type)
    {
        upgradeIdx = type;
        upgradeName.text = GlobalData.upgradeName[(int)type];
        upgradeInfo.text = GlobalData.upgradeInfo[(int)type];
        upgradeImage.sprite = GlobalData.upgradeImage[(int)type];
    }

    void GetUpgrade()
    {
        GameManager.inst.player.GetUpgrade(upgradeIdx);
        GameManager.inst.CloseUpgrade();
    }
}
