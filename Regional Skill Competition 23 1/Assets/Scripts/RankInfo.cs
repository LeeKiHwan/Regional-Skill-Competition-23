using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankInfo
{
    public string name;
    public int score;

    public void SetRankInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
