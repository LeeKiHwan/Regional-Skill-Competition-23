using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [Header("Game UI")]
    public Text stageInfoText;
    public Text totalScoreText;
    public Text totalTimeText;

    [Header("Player UI")]
    public Slider playerHpSlider;
    public Slider playerFuelSlider;
    public Text playerHpText;
    public Text playerFuelText;
    public Text warningText;
    public Text playerBombText;
    public Text playerHpUpText;
    public bool warningOn;
    public Image playerBombImg;
    public Image playerHpUpImg;

    [Header("Boss UI")]
    public Slider bossHpSlider;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (InGameManager.instance.playerClass)
        {
            SetPlayerUI();
        }
        SetGameUI();
        SetBossHpSlider();
    }

    public void SetPlayerUI()
    {
        playerHpSlider.value = (float)InGameManager.instance.playerClass.hp / 100;
        playerFuelSlider.value = InGameManager.instance.playerClass.fuel / 100;
        playerHpText.text = InGameManager.instance.playerClass.hp + " / 100";
        playerFuelText.text = (int)InGameManager.instance.playerClass.fuel + " / 100";

        playerBombImg.fillAmount = InGameManager.instance.playerClass.bombCurTime / InGameManager.instance.playerClass.bombRate;
        playerHpUpImg.fillAmount = InGameManager.instance.playerClass.hpUpCurTime / InGameManager.instance.playerClass.hpUpRate;
        playerBombText.text = InGameManager.instance.playerClass.bombTime.ToString();
        playerHpUpText.text = InGameManager.instance.playerClass.hpUpTime.ToString();

        if (InGameManager.instance.playerObj && (InGameManager.instance.playerObj.transform.position.x <= -1.2f && InGameManager.instance.playerObj.transform.position.y <= -4.3f))
        {
            foreach(Image img in playerHpSlider.GetComponentsInChildren<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 0.1f);
            foreach (Image img in playerFuelSlider.GetComponentsInChildren<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 0.1f);
        }
        else
        {
            foreach (Image img in playerHpSlider.GetComponentsInChildren<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
            foreach (Image img in playerFuelSlider.GetComponentsInChildren<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }

        if (InGameManager.instance.playerObj && (InGameManager.instance.playerObj.transform.position.x >= 2.1f && InGameManager.instance.playerObj.transform.position.y <= -4.3f))
        {
            foreach (Image img in playerBombImg.GetComponentsInParent<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 0.1f);
            foreach (Image img in playerHpUpImg.GetComponentsInParent<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 0.1f);
        }
        else
        {
            foreach (Image img in playerBombImg.GetComponentsInParent<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
            foreach (Image img in playerHpUpImg.GetComponentsInParent<Image>()) img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }
    }

    public void SetGameUI()
    {
        totalScoreText.text = "Total Score : " + InGameManager.totalScore;
        totalTimeText.text = "Total Time : " + (int)InGameManager.totalTime / 60 + "m " + (int)InGameManager.totalTime % 60 + "s";
    }

    public IEnumerator WarningTextOn(string text)
    {
        if (warningOn) yield break;

        warningOn = true;
        warningText.text = text;
        yield return new WaitForSeconds(2);
        warningOn = false;
        warningText.text = "";

        yield break;
    }

    public IEnumerator SetStageInfoText()
    {
        stageInfoText.text = "Clear Stage : " + InGameManager.instance.curStage + "\nStage Score : " + InGameManager.instance.stageScore + "\nStage Time : " + (int)InGameManager.instance.stageTime / 60 + "m " + (int)InGameManager.instance.stageTime % 60 + "s";

        yield return new WaitForSeconds(2);

        if (InGameManager.instance.curStage < 3)
        {
            stageInfoText.text = "Next Stage : " + (InGameManager.instance.curStage + 1);
            yield return new WaitForSeconds(1);
            stageInfoText.text = "";
            StartCoroutine(InGameManager.instance.StartStage(InGameManager.instance.curStage + 1));

            yield break;
        }
        else if (InGameManager.instance.curStage == 3)
        {
            stageInfoText.text = "";
            InGameManager.instance.EndGame();
        }

    }

    public IEnumerator PlayerDieText(string text)
    {
        stageInfoText.text = text;
        yield return new WaitForSeconds(1);
        stageInfoText.text = "";
        InGameManager.instance.EndGame();

        yield break;
    }

    public void OnBossHpSlider(bool onOff)
    {
        bossHpSlider.gameObject.SetActive(onOff);
    }

    public void SetBossHpSlider()
    {
        if (InGameManager.instance.bossClass && bossHpSlider.gameObject)
        {
            bossHpSlider.value = (float)InGameManager.instance.bossClass.hp / (float)InGameManager.instance.bossClass.maxHp;
        }
    }
}
