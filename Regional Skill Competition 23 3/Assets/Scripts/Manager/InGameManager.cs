using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    [Header("Player Info")]
    public GameObject PlayerObj;
    public Player PlayerClass;

    [Header("Game Info")]
    public static int totalScore;
    public static float totalTime;

    [Header("Stage Info")]
    public int stageScore;
    public float stageTime;
    public int currentStage;

    [Header("Spawn Info")]
    public bool monsterSpawnable;
    public GameObject[] MonsterObjs;
    public float[] monsterSpawnRate;
    public float monsterSpawnCurTime;

    [Header("BossInfo")]
    public GameObject[] BossObjs;
    public Boss BossClass;
    public float bossSpawnRate;
    public float bossSpawnCurTime;
    public bool bossSpawnable;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        SpawnMonster();
        Timer();
        CheatKey();
        SpawnBoss();
    }

    public void AddScore(int score)
    {
        totalScore += score;
        stageScore += score;
    }
    public void Timer()
    {
        if (currentStage > 0)
        {
            totalTime += Time.deltaTime;
            stageTime += Time.deltaTime;
        }
    }

    public void SpawnMonster()
    {
        if (monsterSpawnable)
        {
            if (monsterSpawnCurTime > 0) monsterSpawnCurTime -= Time.deltaTime;
            else
            {
                Vector3 randPos = new Vector3(Random.Range(-4f, 4f), 6, 0);
                int rand = Random.Range(0, 4);
                Instantiate(MonsterObjs[rand], randPos, Quaternion.identity);
                monsterSpawnCurTime = monsterSpawnRate[currentStage - 1];
            }
        }
    }

    public IEnumerator StartGame()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerClass = PlayerObj.GetComponent<Player>();
        totalScore = 0;
        totalTime = 0;
        yield return new WaitForSeconds(2);

        StartStage(1);

        yield break;
    }

    public void StartStage(int stage)
    {
        currentStage = stage;

        if (stage == 4)
        {
            EndGame();
            return;
        }

        monsterSpawnable = true;
        bossSpawnable = true;
        bossSpawnCurTime = bossSpawnRate;

        PlayerClass.SkillReset();

        stageScore = 0;
        stageTime = 0;
    }

    public void SpawnBoss()
    {
        if (bossSpawnable)
        {
            if (bossSpawnCurTime > 0) bossSpawnCurTime -= Time.deltaTime;
            else
            {
                monsterSpawnable = false;
                BossClass = Instantiate(BossObjs[currentStage - 1], new Vector3(0, 6.5f, 0), Quaternion.identity).GetComponent<Boss>();
                InGameUIManager.instance.OnBossUI(true);
                bossSpawnable=false;
            }
        }
    }

    public void BossClear()
    {
        InGameUIManager.instance.OnBossUI(false);
        StartCoroutine(InGameUIManager.instance.StageInfoTextShow());
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
            {
                if (obj.GetComponent<Monster>()) Destroy(obj);
                if (obj.GetComponent<Boss>())
                {
                    Destroy(obj);
                    BossClear();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerClass.BulletLevelUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerClass.SkillReset();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerClass.HpUp(100);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerClass.FuelUp();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            monsterSpawnable = false;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(obj);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) Destroy(obj);
            InGameUIManager.instance.OnBossUI(false);

            switch (currentStage)
            {
                case 1:
                    StartStage(2);
                    break;
                case 2:
                    StartStage(3);
                    break;
                case 3:
                    StartStage(1);
                    break;
            }
        }
    }
}
