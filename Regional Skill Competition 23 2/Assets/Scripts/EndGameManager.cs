using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject RankingUI;
    public GameObject RankInfoUI;
    public GameObject RankInsertUI;
    public GameObject GoMenuBtn;
    public InputField NameInputField;
    public Text ScoreText;
    public Text TimeText;

    private void Awake()
    {
        ScoreText.text = "Score : " + GameManager.totalScore;
        TimeText.text = "Time : " + (int)GameManager.totalTime / 60 + "m " + (int)GameManager.totalTime % 60 + "s";
        if (RankingManager.instance.RankInsertAble(GameManager.totalScore))
        {
            RankingUIOn();
            RankInsertUIOn();
        }
        else
        {
            GoMenuBtn.SetActive(true);
            RankInsertUIOn();
        }
    }

    public void RankingUIOn()
    {
        foreach (Transform obj in RankingUI.GetComponentsInChildren<Transform>())
        {
            if (RankingUI.transform == obj) continue;
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            if (RankingManager.instance.ranks[i].score > 0)
            {
                Text text = Instantiate(RankInfoUI, RankingUI.transform).GetComponent<Text>();
                text.text = (i + 1).ToString() + ". \"" + RankingManager.instance.ranks[i].name + "\", " + RankingManager.instance.ranks[i].score;
            }
        }
    }

    public void RankInsertUIOn()
    {
        RankInsertUI.SetActive(true);
    }

    public void RankInsert()
    {
        RankingManager.instance.AddRankInfo(NameInputField.text, GameManager.totalScore);

        RankingUIOn();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
