using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameManager : MonoBehaviour
{
    [Header("Game Info")]
    public static int dif;

    [Header("Status UI")]
    public int hp;
    public Image hpImg;
    public TextMeshProUGUI hpText;

    [Header("Skill UI")]
    public float skillCurTime;
    public Image bgImg;
    public int skillTime;
    public TMP_Text skillTimeText;

    [Header("Score UI")]
    public int score;
    public TMP_Text scoreText;
    public Transform addPos;
    public GameObject addScoreText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(10);
        hpImg.fillAmount = hp / 100f;
        hpText.text = hp + "/100";

        SkillDown();
        scoreText.text = "Score : " + score;

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(AddScoreText(100));
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    public void SkillDown()
    {
        bgImg.fillAmount = skillCurTime / 10f;
        skillTimeText.text = skillTime + " / 3";

        if (skillCurTime > 0 && skillTime > 0) skillCurTime -=Time.deltaTime;
        else if (skillCurTime <= 0 && skillTime > 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                skillCurTime = 10;
                skillTime--;
            }
        }
    }

    public IEnumerator AddScoreText(int addScore)
    {
        GameObject textObj = Instantiate(addScoreText, addPos);
        TMP_Text text = textObj.GetComponent<TMP_Text>();
        text.text = "+" + addScore;
        score += addScore;

        float destroyTime = 1;
        while(destroyTime > 0)
        {
            destroyTime -= Time.deltaTime;
            text.color = new Color(1, 1, 1, destroyTime / 1);
            yield return null;
        }

        Destroy(textObj);

        yield break;
    }
}