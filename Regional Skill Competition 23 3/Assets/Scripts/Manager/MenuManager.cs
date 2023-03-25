using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject RankingUI;
    public GameObject RankText;

    public void StartGame()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void RankingUIOn()
    {
        foreach (Transform obj in RankingUI.transform.GetComponentsInChildren<Transform>())
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

    public void Quit()
    {
        Application.Quit();
    }
}
