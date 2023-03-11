using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject Player;
    [SerializeField] private int score;
    [SerializeField] public int currentStage;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI StageText;
    [SerializeField] private float stageTime;
    [SerializeField] private float totalTime;
    [SerializeField] private int getScore;
    [SerializeField] private TextMeshProUGUI GetScoreText;
    [SerializeField] private TextMeshProUGUI StageTimeText;
    [SerializeField] private TextMeshProUGUI TotalTimeText;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject BossHpSlider;
    [SerializeField] private TMP_InputField NameInputField;
    [SerializeField] private GameObject InsertRankingUI;
    [SerializeField] private GameObject RankingUI;
    [SerializeField] private GameObject MenuUI;
    public GameObject StartBtn;
    public GameObject EndBtn;
    public GameObject RankingBtn;
    public TextMeshProUGUI GameOverText;

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
        Timer();
    }

    public void PlayerDied()
    {
        StartCoroutine(OnMenuUI());
    }

    public void AddScore(int AdditionalScore)
    {
        score += AdditionalScore;
        getScore += AdditionalScore;
    }

    public void StartStage(int stage)
    {
        currentStage = stage;
        if (stage == 1)
        {
            InGameUI.SetActive(true);
            Instantiate(Player, transform.position, Quaternion.identity);
            totalTime = 0;
            score = 0;
        }
        stageTime = 0;
        getScore = 0;
        DeleteMonster();
        monsterSpawnable = true;
        meteorSpawnable = true;
        Invoke("SpawnBoss", 1);
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
                        meteorSpawnTime = 3;
                        break;
                    case 2:
                        meteorSpawnTime = 2;
                        break;
                    case 3:
                        meteorSpawnTime = 1;
                        break;
                }
            }
        }
    }

    private void SpawnBoss()
    {
        BossHpSlider.SetActive(true);
        switch (currentStage)
        {
            case 1:
                Instantiate(Boss[0], new Vector3(0, 7, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(Boss[1], new Vector3(0, 8, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(Boss[2], new Vector3(0, 15, 0), Quaternion.identity);
                break;
        }
        monsterSpawnable = false;
        meteorSpawnable = false;
    }

    public void KilledBoss()
    {
        switch (currentStage)
        {
            case 1:
                StartCoroutine(ClearStage());
                break;
            case 2:
                StartCoroutine(ClearStage());
                break;
            case 3:
                Clear();
                break;
        }
    }

    IEnumerator ClearStage()
    {
        StageText.text = "Cleared Stage : " + currentStage.ToString();
        GetScoreText.text = "Get Score : " + getScore.ToString();
        StageTimeText.text = "Stage Clear Time : " + ((int)stageTime / 60) + ":" + ((int)stageTime % 60);
        StageText.gameObject.SetActive(true);
        GetScoreText.gameObject.SetActive(true);
        StageTimeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        StageText.gameObject.SetActive(false);
        GetScoreText.gameObject.SetActive(false);
        StageTimeText.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillManager>().SetSkillTime();

        getScore = 0;
        stageTime = 0;

        if (currentStage == 1) StartStage(2);
        else if (currentStage == 2) StartStage(3);

        yield break;
    }

    private void Score()
    {
        ScoreText.text = "Score : " + score.ToString();
    }

    private void Timer()
    {
        if (currentStage != 0)
        {
            totalTime += Time.deltaTime;
            stageTime += Time.deltaTime;
        }

        TotalTimeText.text = "Total Time : " + ((int)totalTime / 60) + ":" + ((int)totalTime % 60);
    }

    private void Clear()
    {
        currentStage = 0;
        monsterSpawnable = false;
        meteorSpawnable = false;

        Destroy(GameObject.FindGameObjectWithTag("Player"));
        InGameUI.SetActive(false);

        if (RankingManager.Instance.IsAbleInsertRank(score)) OnInsertRankUI();
        else
        {
            StartBtn.SetActive(true);
            EndBtn.SetActive(true);
            RankingBtn.SetActive(true);
        }
    }

    private void OnInsertRankUI()
    {
        ShowRankingUI();
        InsertRankingUI.SetActive(true);
    }

    public void InsertRankInfo()
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.SetRankInfo(NameInputField.text, score);

        RankingManager.Instance.InsertRankInfo(rankInfo);
    }

    public void ShowRankingUI()
    {
        RankingUI.SetActive(true);
        RankingUI.GetComponent<RankingUI>().ShowRankig();
    }

    IEnumerator OnMenuUI()
    {
        if (GameObject.FindGameObjectWithTag("Monster"))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
            {
                Destroy(obj);
            }
        }

        InGameUI.SetActive(false);
        GameOverText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);


        GameOverText.gameObject.SetActive(false);

        StartBtn.SetActive(true);
        EndBtn.SetActive(true);
        RankingBtn.SetActive(true);

        yield break;
    }
}
