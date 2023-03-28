using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject rankingUI;
    public GameObject rankInfoText;

    public void StartGame()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void RankingUIOn()
    {
        foreach (Transform obj in rankingUI.GetComponentsInChildren<Transform>())
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
            text.text = (i + 1) + ". \"" + RankingManager.instance.ranking[i].name + "\", " + RankingManager.instance.ranking[i].score;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
