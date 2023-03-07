using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private int score;
    [SerializeField] private int currentStage;
    [SerializeField] private bool monsterSpawnable;

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

    public static GameManager Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }

    private void Update()
    {
        
    }

    public void AddScore(int AdditionalScore)
    {
        score += AdditionalScore;
    }
}
