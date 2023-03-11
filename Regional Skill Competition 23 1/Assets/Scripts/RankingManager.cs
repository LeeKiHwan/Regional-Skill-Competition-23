using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private static RankingManager instance;
    private int rankingCount = 5;
    public List<RankInfo> Ranking = new List<RankInfo>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("instance error");
            Destroy(gameObject);
        }
    }
    public static RankingManager Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < rankingCount; i++)
        {
            RankInfo empty = new RankInfo();
            empty.SetRankInfo("Empty", 0);
            Ranking.Add(empty);
        }
    }

    public void InsertRankInfo(RankInfo rankInfo)
    {
        for (int i = 0; i < Ranking.Count; i++)
        {
            if (Ranking[i].score <= rankInfo.score)
            {
                Ranking.Insert(i, rankInfo); break;
            }
        }
        Ranking.RemoveAt(5);
    }

    public bool IsAbleInsertRank(int score)
    {
        if (Ranking.Count < 5) return true;

        if (Ranking[4].score < score) return true;

        return false;
    }

}
