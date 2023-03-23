using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Player Status UI")]
    public Slider HpSlider;
    public Text HpText;
    public Slider FuelSlider;
    public Text FuelText;

    [Header("Player Skill UI")]
    public Image BombSkillImg;
    public Image HpUpSkillImg;
    public Text WaringText;
    public bool waringTextOn;

    [Header("Game Info UI")]
    public Text ScoreText;
    public Text TimeText;
    public Text StageInfoText;
    public Slider BossHpSlider;
    public Text GameOverText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        Hp();
        Fuel();

        BombSkill();
        HpUpSkill();

        GameInfoTextShow();

        setBossHpSlider();
    }

    public void Hp()
    {
        HpSlider.value = (float)GameManager.instance.PlayerStatus.hp / 100f;
        HpText.text = GameManager.instance.PlayerStatus.hp + " / 100";
    }
    public void Fuel()
    {
        FuelSlider.value = GameManager.instance.PlayerStatus.fuel / 100;
        FuelText.text = (int)GameManager.instance.PlayerStatus.fuel + " / 100";
    }

    public void BombSkill()
    {
        BombSkillImg.fillAmount = GameManager.instance.PlayerStatus.bombCurTime / GameManager.instance.PlayerStatus.bombCoolTime;
    }
    public void HpUpSkill()
    {
        HpUpSkillImg.fillAmount = GameManager.instance.PlayerStatus.HpUpCurTime / GameManager.instance.PlayerStatus.HpUpCoolTime;
    }
    public IEnumerator WaringTextShow(string text)
    {
        if (waringTextOn) yield break;

        waringTextOn = true;
        WaringText.text = text;
        yield return new WaitForSeconds(2);
        WaringText.text = "";
        waringTextOn = false;

        yield break;
    }

    public void GameInfoTextShow()
    {
        ScoreText.text = "Score : " + GameManager.totalScore.ToString();
        TimeText.text = "TotalTime : " + (int)GameManager.totalTime / 60 + "m " + (int)GameManager.totalTime % 60 + "s";
    }

    public IEnumerator StageInfoTextShow()
    {
        StageInfoText.text = "ClearStage : " + GameManager.instance.currentStage + "\n" + "Get Score : " + GameManager.instance.stageScore + "\n" + "Stage Time : " + (int)GameManager.instance.stageTime / 60 + "m " + (int)GameManager.instance.stageTime % 60 + "s";
        yield return new WaitForSeconds(3);
        StageInfoText.text = "";

        GameManager.instance.StartStage(GameManager.instance.currentStage + 1);

        yield break;
    }

    public void setBossHpSlider()
    {
        if (GameManager.instance.BossClass != null)
        {
            BossHpSlider.value = (float)GameManager.instance.BossClass.hp / (float)GameManager.instance.BossClass.maxHp;
        }
    }

    public void BossHpSliderOnOff(bool onOrOff)
    {
        BossHpSlider.gameObject.SetActive(onOrOff);
    }

    public IEnumerator GameOverTextOn(string text)
    {
        GameOverText.text = text;
        yield return new WaitForSeconds(3);

        GameManager.instance.EndGame();

        yield break;
    }
}