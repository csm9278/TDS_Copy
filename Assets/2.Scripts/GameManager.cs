using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField]
    GameObject wallObject;
    public bool gameEnd = false;

    int level = 1;
    int maxEXP = 4;
    int curExp = 0;

    public Player player;

    [SerializeField]
    TMP_Text dayText, expText;
    [SerializeField]
    Image expFill;

    [SerializeField]
    GameObject LevelUpPanel;
    [SerializeField]
    EnemyGenerator enemyGenerator;
    [SerializeField]
    Upgrade[] upgrades;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    Button restartBtn;


    float dayTime = 20.0f;
    float curDayTime = 20.0f;
    
    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        CheckDayTime();
    }

    public void PlayerDie()
    {
        gameEnd = true;
        wallObject.gameObject.SetActive(false);
    }

    IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(2.0f);

        Time.timeScale = 0;
        gameOverPanel.gameObject.SetActive(true);
    }

    public void GetExp()
    {
        curExp++;
        expText.text = "EXP: " + curExp.ToString() + "/" + maxEXP.ToString();
        expFill.fillAmount = (float)curExp / maxEXP;

        if(curExp >= maxEXP)
        {
            LevelUp();
        }
    }
    
    void LevelUp()
    {
        Time.timeScale = 0;

        level++;
        maxEXP = (level * 4);
        curExp = 0;
        expFill.fillAmount = 0;
        LevelUpPanel.gameObject.SetActive(true);

        UpgradeType type1 = (UpgradeType)Random.Range(0, (int)UpgradeType.LastIdx);
        UpgradeType type2 = (UpgradeType)Random.Range(0, (int)UpgradeType.LastIdx);

        if(type1 == type2)
        {
            if (type2 + 1 != UpgradeType.LastIdx)
                type2++;
            else
                type2--;
        }

        upgrades[0].SetUpgrade(type1);
        upgrades[1].SetUpgrade(type2);
    }

    public void CloseUpgrade()
    {
        Time.timeScale = 1;
        LevelUpPanel.gameObject.SetActive(false);
    }

    void CheckDayTime()
    {
        if (curDayTime >= 0.0f)
            curDayTime -= Time.deltaTime;

        if (curDayTime <= 0.0f)
        {
            enemyGenerator.SetGenerateTime(level);
            dayText.text = "Day " + level.ToString();
            curDayTime = dayTime;
        }
    }
}
