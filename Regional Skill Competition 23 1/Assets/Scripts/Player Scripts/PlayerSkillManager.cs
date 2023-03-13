using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;

    [Header("Bomb Skill")]
    [SerializeField] private GameObject BombSkillObj;
    [SerializeField] private int bombSkillTime;
    [SerializeField] private float bombSkillCoolTime;
    [SerializeField] private float bombSkillCurTime;

    [Header("Bomb Skill UI")]
    [SerializeField] private Image BombSkillCoolTimeImage;
    [SerializeField] private TextMeshProUGUI BombSkillCount;

    [Header("HpUp Skill")]
    [SerializeField] private float hpUpValue;
    [SerializeField] private int hpUpSkillTime;
    [SerializeField] private float hpUpSkillCoolTime;
    [SerializeField] private float hpUpSkillCurTime;

    [Header("HpUp Skill UI")]
    [SerializeField] private Image HpUpCoolTimeImage;
    [SerializeField] private TextMeshProUGUI HpUpSkillCount;

    

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerStatus>();

        if (GameObject.Find("Tutorial Manager")) return;
        BombSkillCoolTimeImage = GameObject.Find("BombSkill CoolTime").GetComponent<Image>();
        BombSkillCount = GameObject.Find("BombSkill Count Text").GetComponent<TextMeshProUGUI>();

        HpUpCoolTimeImage = GameObject.Find("HpUpSkill CoolTime").GetComponent<Image>();
        HpUpSkillCount = GameObject.Find("HpUpSkill Count Text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UseBombSkill();
        UseHpUpSkill();
        BombSkillUI();
        HpUpUI();
    }

    private void UseBombSkill()
    {
        if (bombSkillCurTime > 0) bombSkillCurTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K) && bombSkillCurTime <= 0 && bombSkillTime > 0)
        {
            Instantiate(BombSkillObj, new Vector2(0,0), Quaternion.identity);
            bombSkillTime--;
            bombSkillCurTime = bombSkillCoolTime;
        }
    }

    private void UseHpUpSkill()
    {
        if (hpUpSkillCurTime > 0) hpUpSkillCurTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L) && hpUpSkillCurTime <= 0 && hpUpSkillTime > 0)
        {
            player.HpUp(hpUpValue);
            hpUpSkillTime--;
            hpUpSkillCurTime = hpUpSkillCoolTime;
        }
    }

    private void BombSkillUI()
    {
        BombSkillCoolTimeImage.fillAmount = bombSkillCurTime / bombSkillCoolTime;
        BombSkillCount.text = bombSkillTime.ToString();
    }

    private void HpUpUI()
    {
        HpUpCoolTimeImage.fillAmount = hpUpSkillCurTime / hpUpSkillCoolTime;
        HpUpSkillCount.text = hpUpSkillTime.ToString();
    }

    public void SetSkillTime()
    {
        hpUpSkillTime = 3;
        bombSkillTime = 3;
    }
}
