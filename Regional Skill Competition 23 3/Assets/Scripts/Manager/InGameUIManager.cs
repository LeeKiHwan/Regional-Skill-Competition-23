using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [Header("Player UI")]
    public Slider PlayerHpSlider;
    public Text PlayerHpText;
    public Slider PlayerFuelSlider;
    public Text PlayerFuelText;
    public Image BombImg;
    public Image HpUpImg;
    public Text BombTimeText;
    public Text HpUpTimeText;
    public Text WaringText;
    public bool waringTextOn;

    [Header("GameInfo UI")]
    public Text ScoreText;
    public Text TimeText;
    public Text StageInfoText;

    [Header("BossUI")]
    public Slider BossHpSlider;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (InGameManager.instance.PlayerClass)
        {
            SetPlayerUI();
            SetGameInfoUI();
        }
        if (InGameManager.instance.BossClass)
        {
            SetBossUI();
        }
    }

    public void SetPlayerUI()
    {
        PlayerHpSlider.value = (float)InGameManager.instance.PlayerClass.hp / 100;
        PlayerFuelSlider.value = InGameManager.instance.PlayerClass.fuel / 100;
        PlayerHpText.text = InGameManager.instance.PlayerClass.hp + " / 100";
        PlayerFuelText.text = (int)InGameManager.instance.PlayerClass.fuel + " / 100";

        BombImg.fillAmount = InGameManager.instance.PlayerClass.bombCurTime / InGameManager.instance.PlayerClass.bombCoolTime;
        HpUpImg.fillAmount = InGameManager.instance.PlayerClass.hpUpCurTime / InGameManager.instance.PlayerClass.hpUpCoolTime;

        BombTimeText.text = InGameManager.instance.PlayerClass.bombTime.ToString();
        HpUpTimeText.text = InGameManager.instance.PlayerClass.hpUpTime.ToString();
    }

    public void SetGameInfoUI()
    {
        ScoreText.text = "Total Score : " + InGameManager.totalScore;
        TimeText.text = "Total Time : " + (int)InGameManager.totalTime / 60 + "m " + (int)InGameManager.totalTime % 60 + "s";
    }

    public IEnumerator StageInfoTextShow()
    {
        StageInfoText.text = "Clear Stage : " + InGameManager.instance.currentStage + "\nStage Time : " + (int)InGameManager.instance.stageTime / 60 + "m " + (int)InGameManager.instance.stageTime % 60 + "s" + "\nStage Score : " + InGameManager.instance.stageScore;
        yield return new WaitForSeconds(2);
        StageInfoText.text = "";

        InGameManager.instance.StartStage(InGameManager.instance.currentStage + 1);

        yield break;
    }

    public void OnBossUI(bool set)
    {
        BossHpSlider.gameObject.SetActive(set);
    }

    public void SetBossUI()
    {
        BossHpSlider.value = (float)InGameManager.instance.BossClass.hp / (float)InGameManager.instance.BossClass.maxHp;
    }

    public IEnumerator OnWaringText(string text)
    {
        if (waringTextOn) yield break;

        waringTextOn = true;
        WaringText.text = text;
        yield return new WaitForSeconds(2);
        WaringText.text = "";
        waringTextOn = false;

        yield break;
    }
}
