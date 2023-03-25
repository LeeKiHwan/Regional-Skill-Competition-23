using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;

    public List<RankInfo> Ranking = new List<RankInfo>();

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
            Ranking.Add(rankInfo);
        }
    }

    public void RankInsert(RankInfo insertRankInfo)
    {
        Ranking.Add(insertRankInfo);

        for (int i = 5; i > 0; i--)
        {
            if (Ranking[i].score > Ranking[i - 1].score)
            {
                RankInfo changeRank = Ranking[i - 1];
                Ranking[i - 1] = Ranking[i];
                Ranking[i] = changeRank;
            }
        }

        Ranking.RemoveAt(5);
    }

    public bool AbleRankInsert(int score)
    {
        if (Ranking[4].score < score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
