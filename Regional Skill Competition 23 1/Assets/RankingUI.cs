using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingUI : MonoBehaviour
{
    TMP_Text[] child;

    private void Start()
    {
    }

    public void ShowRankig()
    {
        child = GetComponentsInChildren<TMP_Text>();

        for (int i = 0; i < 5; i++)
        {
            if (RankingManager.Instance.Ranking.Count < i + 1) return;

            child[i].text = (i + 1).ToString() + ". " + "\"" + RankingManager.Instance.Ranking[i].name + "\"" + ", " + RankingManager.Instance.Ranking[i].score;
        }
    }
}
