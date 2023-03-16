using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public GameObject PlayerObj;
    public PlayerStatus PlayerStatus;

    [Header("Game Status")]
    public int score;
    public int stageScore;

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
    }

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
    }
}
