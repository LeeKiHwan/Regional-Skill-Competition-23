using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject RankingUI;
    public GameObject RankInfoUI;

    public void StartGame()
    {
        SceneManager.LoadScene("InGameScene");
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
