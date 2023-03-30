using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    [Header("Player Info")]
    public GameObject playerObj;
    public Player playerClass;

    [Header("Game Info")]
    public int curStage;
    public static int totalScore;
    public static float totalTime;
    public static int difficulty;

    [Header("Spawn Info")]
    public GameObject[] monsterObjs;
    public bool monsterSpawnable;
    public float monsterSpawnCurTime;
    public float[] monsterSpawnRate;
    public Boss bossClass;
    public GameObject[] bossObjs;
    public bool bossSpawnable;
    public float bossSpawnCurTime;
    public float bossSpawnRate;

    [Header("Stage Info")]
    public int stageScore;
    public float stageTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        StartGame();
    }

    private void Update()
    {
        Timer();
        MonsterSpawn();
        BossSpawn();
        CheatKey();
    }

    public void StartGame()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerClass = playerObj.GetComponent<Player>();

        for (int i = 0; i < monsterSpawnRate.Length; i++)
        {
            monsterSpawnRate[i] = 10 - (i * 2 + difficulty * 2);
        }

        totalScore = 0;
        totalTime = 0;

        StartCoroutine(StartStage(1));
    }

    public IEnumerator StartStage(int stage)
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(obj);
        }
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("MonsterBullet"))
        {
            Destroy(obj);
        }

        playerClass.ResetStatus();


        stageScore = 0;
        stageTime = 0;

        monsterSpawnable = true;

        bossSpawnCurTime = bossSpawnRate;
        bossSpawnable = true;

        curStage = stage;

        yield break;
    }

    public void AddScore(int addScore)
    {
        totalScore += addScore;
        stageScore += addScore;
    }
    public void Timer()
    {
        totalTime += Time.deltaTime;
        stageTime += Time.deltaTime;
    }

    public void MonsterSpawn()
    {
        if (monsterSpawnable)
        {
            if (monsterSpawnCurTime > 0) monsterSpawnCurTime -= Time.deltaTime;
            else
            {
                int rand = Random.Range(0, 7);
                switch (rand)
                {
                    case 0:
                        StartCoroutine(AssaultSpawn());
                        break;
                    case 1:
                        TankSpawn();
                        break;
                    case 2:
                        StartCoroutine(SpeedSpawn());
                        break;
                    case 3:
                        BigSpawn();
                        break;
                    case 4:
                        StartCoroutine(MeteorShower());
                        break;
                    case 5:
                        AssualtSpeedSpawn();
                        break;
                    case 6:
                        StartCoroutine(BigSpeedSpawn());
                        break;
                }
                monsterSpawnCurTime = monsterSpawnRate[curStage - 1];
            }
        }
    }
    public IEnumerator AssaultSpawn()
    {
        Instantiate(monsterObjs[0], new Vector3(0, 5, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(monsterObjs[0], new Vector3(1, 5, 0), Quaternion.identity);
        Instantiate(monsterObjs[0], new Vector3(-1, 5, 0), Quaternion.identity);


        yield break;
    }
    public void TankSpawn()
    {
        Instantiate(monsterObjs[1], new Vector3(-2, 5, 0), Quaternion.identity);
        Instantiate(monsterObjs[1], new Vector3(2, 5, 0), Quaternion.identity);
    }
    public IEnumerator SpeedSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(monsterObjs[2], new Vector3(-3, 5, 0), Quaternion.identity);
            Instantiate(monsterObjs[2], new Vector3(3, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        yield break;
    }
    public void BigSpawn()
    {
        Instantiate(monsterObjs[3], new Vector3(0, 6.5f, 0), Quaternion.identity);
    }
    public IEnumerator MeteorShower()
    {
        for (int i = 0; i < 7;i++)
        {
            Instantiate(monsterObjs[4], new Vector3(Random.Range(-3, 3), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }

        yield break;
    }
    public IEnumerator AssualtSpeedSpawn()
    {
        Instantiate(monsterObjs[0], new Vector3(1, 5, 0), Quaternion.identity);
        Instantiate(monsterObjs[0], new Vector3(-1, 5, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(monsterObjs[2], new Vector3(-3, 5, 0), Quaternion.identity);
        Instantiate(monsterObjs[2], new Vector3(3, 5, 0), Quaternion.identity);

        yield break;
    }
    public IEnumerator BigSpeedSpawn()
    {
        Instantiate(monsterObjs[3], new Vector3(-2, 6.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(monsterObjs[2], new Vector3(0, 5, 0), Quaternion.identity);
        Instantiate(monsterObjs[2], new Vector3(2, 5, 0), Quaternion.identity);

        yield break;
    }


    public void BossSpawn()
    {
        if (bossSpawnable)
        {
            if (bossSpawnCurTime > 0) bossSpawnCurTime -= Time.deltaTime;
            else
            {
                monsterSpawnable = false;
                switch (curStage)
                {
                    case 1:
                        bossClass = Instantiate(bossObjs[curStage - 1], new Vector3(0, 6.3f, 0), Quaternion.identity).GetComponent<Boss>();
                        break;
                    case 2:
                        bossClass = Instantiate(bossObjs[curStage - 1], new Vector3(0, 6, 0), Quaternion.identity).GetComponent<Boss>();
                        break;
                    case 3:
                        bossClass = Instantiate(bossObjs[curStage - 1], new Vector3(0, 9.5f, 0), Quaternion.identity).GetComponent<Boss>();
                        break;
                }
                InGameUIManager.instance.OnBossHpSlider(true);
                bossSpawnable = false;
            }
        }
    }

    public void BossClear()
    {
        InGameUIManager.instance.OnBossHpSlider(false);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) Destroy(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(obj);
        StartCoroutine(InGameUIManager.instance.SetStageInfoText());
    }

    public void PlayerDie(string text)
    {
        StartCoroutine(InGameUIManager.instance.PlayerDieText(text));
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
                if (obj.GetComponent<Boss>()) obj.GetComponent<Boss>().Die("");
                if (obj.GetComponent<Monster>()) obj.GetComponent<Monster>().Die("");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < 4; i++)
            {
                playerClass.BulletUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            playerClass.ResetStatus();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            playerClass.hp = 100;
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            playerClass.fuel = 100;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            if (curStage < 3) StartCoroutine(StartStage(curStage + 1));
            else if (curStage == 3) StartCoroutine(StartStage(1));
            InGameUIManager.instance.OnBossHpSlider(false);
        }
    }
}
