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

    [Header("Skill SFX")]
    public AudioClip BombSound;
    public AudioClip HpUpSound;
    public AudioClip CoolTimeSound;
    public TextMeshProUGUI CoolTimeText;
    

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerStatus>();

        if (GameObject.Find("Tutorial Manager")) return;
        BombSkillCoolTimeImage = GameObject.Find("BombSkill CoolTime").GetComponent<Image>();
        BombSkillCount = GameObject.Find("BombSkill Count Text").GetComponent<TextMeshProUGUI>();

        HpUpCoolTimeImage = GameObject.Find("HpUpSkill CoolTime").GetComponent<Image>();
        HpUpSkillCount = GameObject.Find("HpUpSkill Count Text").GetComponent<TextMeshProUGUI>();

        CoolTimeText = GameObject.Find("CoolTime Text").GetComponent<TextMeshProUGUI>();
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
            SoundManager.Instance.SFXPlay("BombSound", BombSound);
            bombSkillTime--;
            bombSkillCurTime = bombSkillCoolTime;
        }
        else if (Input.GetKeyDown(KeyCode.K) && bombSkillCurTime > 0 && bombSkillTime > 0)
        {
            StartCoroutine(CoolTime("스킬이 준비중입니다!"));
        }
        else if (Input.GetKeyDown(KeyCode.K) && bombSkillTime == 0)
        {
            StartCoroutine(CoolTime("스킬을 모두 소모했습니다."));
        }
    }

    private void UseHpUpSkill()
    {
        if (hpUpSkillCurTime > 0) hpUpSkillCurTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L) && hpUpSkillCurTime <= 0 && hpUpSkillTime > 0)
        {
            player.HpUp(hpUpValue);
            SoundManager.Instance.SFXPlay("HpUpSound", HpUpSound);
            hpUpSkillTime--;
            hpUpSkillCurTime = hpUpSkillCoolTime;
        }
        else if (Input.GetKeyDown(KeyCode.L) && hpUpSkillCurTime > 0 && hpUpSkillTime > 0)
        {
            StartCoroutine(CoolTime("스킬이 준비중입니다!"));
        }
        else if (Input.GetKeyDown(KeyCode.L) && hpUpSkillTime == 0)
        {
            StartCoroutine(CoolTime("스킬을 모두 소모했습니다."));
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
        hpUpSkillCurTime = 0;
        bombSkillCurTime = 0;
    }

    IEnumerator CoolTime(string text)
    {
        SoundManager.Instance.SFXPlay(text, CoolTimeSound);
        CoolTimeText.text = text;
        yield return new WaitForSeconds(2);
        CoolTimeText.text = "";

        yield break;
    }
}
