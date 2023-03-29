using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class EndGameManager : MonoBehaviour
{
    [Header("Ranking")]
    public GameObject rankingUI;
    public GameObject rankInfoText;

    [Header("Rank Insert")]
    public GameObject rankInsertUI;
    public InputField nameInputField;
    public Button rankInsertBtn;

    [Header("My GameInfo")]
    public Text scoreText;
    public Text timeText;

    public Button goMenuBtn;


    private void Awake()
    {
        RankingUIOn();

        scoreText.text = "Total Score : " + InGameManager.totalScore;
        timeText.text = "Total Time : " + (int)InGameManager.totalTime / 60 + "m " + (int)InGameManager.totalTime % 60 + "s";

        if (RankingManager.instance.AbleRankInsert(InGameManager.totalScore))
        {
            RankInsertUIOn();
        }
        else
        {
            goMenuBtn.gameObject.SetActive(true);
        }
    }

    public void RankingUIOn()
    {
        foreach(Transform obj in rankingUI.GetComponentsInChildren<Transform>())
        {
            if (obj == rankingUI.transform)
            {
                continue;
            }
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            if (RankingManager.instance.ranking[i].score == 0) return;

            Text text = Instantiate(rankInfoText, rankingUI.transform).GetComponent<Text>();
            text.text = (i + 1) + ". \"" +RankingManager.instance.ranking[i].name + "\", " + RankingManager.instance.ranking[i].score;
        }
    }

    public void RankInsertUIOn()
    {
        rankInsertUI.SetActive(true);
    }

    public void RankInsert()
    {
        if (!Regex.IsMatch(nameInputField.text, @"^[a-zA-Z]+$") || nameInputField.text.Length != 3) return;

        RankInfo rankInfo = new RankInfo();
        rankInfo.SetRankInfo(nameInputField.text, InGameManager.totalScore);

        RankingManager.instance.RankInsert(rankInfo);
        RankingUIOn();

        goMenuBtn.gameObject.SetActive(true);
        rankInsertUI.SetActive(false);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
