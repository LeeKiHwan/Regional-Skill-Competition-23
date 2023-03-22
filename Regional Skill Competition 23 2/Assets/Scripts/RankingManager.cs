using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;
    public List<RankInfo> ranks = new List<RankInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        for (int i = 0; i < 5; i++)
        {
            RankInfo rankInfo = new RankInfo();
            rankInfo.SetRankInfo("null", 0);

            ranks.Add(rankInfo);
        }
    }

    public void AddRankInfo(string name, int score)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.SetRankInfo(name, score);
        ranks.Add(rankInfo);

        for (int i = ranks.Count - 1; i > 0; i--)
        {
            if (ranks[i].score > ranks[i - 1].score)
            {
                RankInfo changeRankInfo = ranks[i - 1];
                ranks[i - 1] = ranks[i];
                ranks[i] = changeRankInfo;
            }
        }

        ranks.RemoveAt(5);
    }

    public bool RankInsertAble(int score)
    {
        if (score > ranks[4].score)
        {
            return true;
        }
        return false;
    }
}
