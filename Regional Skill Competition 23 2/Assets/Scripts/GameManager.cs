using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public GameObject PlayerObj;
    public PlayerStatus PlayerStatus;

    [Header("Game Status")]
    public int totalScore;
    public float totalTime;
    public int currentStage;
    public int stageScore;
    public float stageTime;

    [Header("Monster")]
    public GameObject[] MonsterObj;
    public bool monsterSpawnable;
    public float[] monsterSpawnCoolTime;
    public float monsterSpawnCurTime;

    [Header("Meteor")]
    public GameObject MeteorObj;
    public bool meteorSpawnable;
    public float[] meteorSpawnCoolTime;
    public float meteorSpawnCurCoolTime;

    [Header("Boss")]
    public GameObject[] BossObj;
    public Boss BossClass;

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

    private void Update()
    {
        SpawnMonster();
        SpawnMeteor();
        GameTimer();

        if (Input.GetKeyDown(KeyCode.P)) StartGame();
    }

    public void AddScore(int additionalScore)
    {
        totalScore += additionalScore;
        stageScore += additionalScore;
    }

    public void SpawnMonster()
    {
        if (monsterSpawnCurTime > 0) monsterSpawnCurTime -= Time.deltaTime;

        if (monsterSpawnable && monsterSpawnCurTime <= 0)
        {
            int rand = Random.Range(0, 3);
            float randPos = Random.Range(-3.5f, 3.5f);

            Instantiate(MonsterObj[rand], new Vector3(randPos, 5.5f, 0), Quaternion.identity);

            monsterSpawnCurTime = monsterSpawnCoolTime[currentStage - 1];
        }
    }

    public void SpawnMeteor()
    {
        if (meteorSpawnCurCoolTime > 0) meteorSpawnCurCoolTime -= Time.deltaTime;

        if (meteorSpawnable && meteorSpawnCurCoolTime <= 0)
        {
            int rand = Random.Range(0, 3);
            float randPos = Random.Range(-3.5f, 3.5f);

            Instantiate(MeteorObj, new Vector3(randPos, 5.5f, 0), Quaternion.identity);

            meteorSpawnCurCoolTime = meteorSpawnCoolTime[currentStage - 1];
        }
    }

    public void GameTimer()
    {
        if (currentStage != 0)
        {
            totalTime += Time.deltaTime;
            stageTime += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGameScene");
        Instantiate(PlayerObj, new Vector3(0, 0, 0), Quaternion.identity);
        StartStage(1);
    }

    public void StartStage(int startStage)
    {
        currentStage = startStage;

        stageScore = 0;
        stageTime = 0;
        
        monsterSpawnable = true;
        meteorSpawnable = true;

        Invoke("SpawnBoss", 60);
    }

    public void ClearStage(int clearStage)
    {
        Debug.Log("Clear Stage");
        if (clearStage == 3)
        {
            EndGame();
            return;
        }
        StartCoroutine(UIManager.instance.StageInfoTextShow());
    }

    public void SpawnBoss()
    {
        monsterSpawnable = false;
        meteorSpawnable = false;

        BossClass = Instantiate(BossObj[currentStage - 1], new Vector3(0, 7, 0), Quaternion.identity).GetComponent<Boss>();
        UIManager.instance.BossHpSliderOnOff(true);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RankInsertUIOn()
    {

    }

    public void PlayerDie()
    {
        EndGame();
    }


}
