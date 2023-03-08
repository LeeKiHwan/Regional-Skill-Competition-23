using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private int score;
    [SerializeField] public int currentStage;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [Header("Monster")]
    public bool monsterSpawnable;
    [SerializeField] private float monsterSpawnTime;
    [SerializeField] private GameObject[] MonsterObjs;
    [SerializeField] private GameObject[] Boss;

    [Header("Meteor")]
    [SerializeField] private GameObject MeteorObj;
    [SerializeField] private float meteorSpawnTime;
    [SerializeField] private bool meteorSpawnable;
    

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
        SpawnMonster();
        SpawnMeteor();
        Score();
    }

    public void AddScore(int AdditionalScore)
    {
        score += AdditionalScore;
    }

    public void StartStage(int stage)
    {
        DeleteMonster();
        Invoke("SpawnBoss", 60);
        currentStage = stage;
    }

    public void DeleteMonster()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(obj);
        }
    }

    private void SpawnMonster()
    {
        if (monsterSpawnable)
        {
            if (monsterSpawnTime > 0) monsterSpawnTime -= Time.deltaTime;
            else
            {
                int rand = Random.Range(0, MonsterObjs.Length);

                Instantiate(MonsterObjs[rand], new Vector3(Random.Range(-4.5f, 4.5f), 6, 0), Quaternion.identity);

                switch (currentStage)
                {
                    case 1:
                        monsterSpawnTime = 4;
                        break;
                    case 2:
                        monsterSpawnTime = 3;
                        break;
                    case 3:
                        monsterSpawnTime = 2;
                        break;
                }
            }
        }
    }

    private void SpawnMeteor()
    {
        if (meteorSpawnable)
        {
            if (meteorSpawnTime > 0) meteorSpawnTime -= Time.deltaTime;
            else
            {
                Instantiate(MeteorObj, new Vector3(Random.Range(-7f, 7f), 6, 0), Quaternion.identity);

                switch (currentStage)
                {
                    case 1:
                        meteorSpawnTime = 7;
                        break;
                    case 2:
                        meteorSpawnTime = 5;
                        break;
                    case 3:
                        meteorSpawnTime = 3;
                        break;
                }
            }
        }
    }

    private void SpawnBoss()
    {
        switch (currentStage)
        {
            case 1:
                Instantiate(Boss[0], new Vector3(0, 7, 0), Quaternion.identity);
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public void KilledBoss()
    {

    }

    private void Score()
    {
        ScoreText.text = "Score : " + score.ToString();
    }
}
