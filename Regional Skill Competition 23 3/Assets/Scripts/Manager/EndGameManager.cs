using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject RankingUI;
    public GameObject RankText;
    public GameObject RankInsertUI;
    public Button MenuBtn;
    public InputField NameInputField;
    public Text TotalTimeText;
    public Text TotalScoreText;

    private void Awake()
    {
        OnRankingUI();

        TotalTimeText.text = "Total Time : " + (int)InGameManager.totalTime / 60 + "m " + (int)InGameManager.totalTime % 60 + "s";
        TotalScoreText.text = "Total Score : " + InGameManager.totalScore;

        if (RankingManager.instance.AbleRankInsert(InGameManager.totalScore))
        {
            OnRankInsertUI();
        }
        else
        {
            MenuBtn.gameObject.SetActive(true);
        }
    }

    public void OnRankingUI()
    {
        foreach(Transform obj in RankingUI.transform.GetComponentsInChildren<Transform>())
        {
            if (obj == RankingUI.transform) continue;
            Destroy(obj.gameObject);
        }
        for (int i = 0; i < RankingManager.instance.Ranking.Count; i++)
        {
            if (RankingManager.instance.Ranking[i].score == 0) break;
            Text text = Instantiate(RankText, RankingUI.transform).GetComponent<Text>();
            text.text = (i + 1) + ". " + "\"" + RankingManager.instance.Ranking[i].name + "\", " + RankingManager.instance.Ranking[i].score;
        }
    }

    public void OnRankInsertUI()
    {
        RankInsertUI.SetActive(true);
    }

    public void RankInsert()
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.SetRankInfo(NameInputField.text, InGameManager.totalScore);
        RankingManager.instance.RankInsert(rankInfo);
        OnRankingUI();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
