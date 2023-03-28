using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;

    public List<RankInfo> ranking = new List<RankInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        for (int i = 0; i < 5; i++)
        {
            RankInfo rankInfo = new RankInfo();
            rankInfo.SetRankInfo("null", 0);
            ranking.Add(rankInfo);
        }
    }

    public bool AbleRankInsert(int score)
    {
        if (ranking[4].score > score) return false;
        else return true;
    }

    public void RankInsert(RankInfo rankInfo)
    {
        ranking.Add(rankInfo);

        for (int i = 5; i > 0;i--)
        {
            if (ranking[i].score > ranking[i-1].score)
            {
                RankInfo changeRankInfo = ranking[i - 1];
                ranking[i - 1] = ranking[i];
                ranking[i] = changeRankInfo;
            }
        }

        ranking.RemoveAt(5);
    }
}
