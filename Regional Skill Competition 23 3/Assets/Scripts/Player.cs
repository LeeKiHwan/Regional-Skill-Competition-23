using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [Header("PlayerStatus")]
    public float fuel;
    public float invcTime;

    [Header("Skill")]
    public float bombCurTime;
    public float bombCoolTime;
    public int bombTime;
    public int bombDamage;
    public float hpUpCurTime;
    public float hpUpCoolTime;
    public int hpUpTime;
    public int hpUpValue;
    public GameObject bombObj;

    [Header("SFX")]
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip bombSound;
    public AudioClip hpUpSound;

    private void Update()
    {
        Move();
        Fire();
        FuelDown();
        InvcTimeDown();

        BombSkill();
        HpUpSkill();
    }

    public override void TakeDamage(int damage)
    {
        if (invcTime <= 0)
        {
            SoundManager.instance.SFXPlay("PlayerHit", hitSound);
            base.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        InGameManager.instance.EndGame();
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;

        if (fireCurTime <= 0 && Input.GetKey(KeyCode.Keypad1)) BulletFire();
    }

    public void BulletFire()
    {
        SoundManager.instance.SFXPlay("PlayerFire", fireSound);
        float dis = 0.3f;
        for (int i = 1; i <= bulletLevel; i++)
        {
            Vector3 firePos = transform.position;
            if (bulletLevel % 2 != 0)
            {
                if (i == 2) firePos.x += dis;
                if (i == 3) firePos.x -= dis;
                Instantiate(BulletObj[0], firePos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            }
            else
            {
                if (i == 1) firePos.x += dis * 0.5f;
                if (i == 2) firePos.x -= dis * 0.5f;
                if (i == 3) firePos.x += dis * 2;
                if (i == 4) firePos.x -= dis * 2;
                Instantiate(BulletObj[0], firePos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            }
        }
        fireCurTime = fireRate;
    }

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);
    }

    public void HpUp(int addHp)
    {
        if (hp + addHp >= 100) hp = 100;
        else hp += addHp;
    }
    
    public void FuelUp()
    {
        fuel = 100;
    }
    public void FuelDown()
    {
        if (fuel > 0 && InGameManager.instance.monsterSpawnable) fuel -= Time.deltaTime * 2;
        else if (fuel <= 0) Die();
    }

    public void InvcTimeUp(float time)
    {
        if (invcTime <= time) invcTime = time;
    }
    public void InvcTimeDown()
    {
        if (invcTime > 0)
        {
            invcTime -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }

    public void BulletLevelUp()
    {
        if (bulletLevel < 4)
        {
            bulletLevel++;
        }
    }

    public void BombSkill()
    {
        if (bombCurTime > 0) bombCurTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (bombCurTime <= 0 && bombTime > 0)
            {
                SoundManager.instance.SFXPlay("BombSkill", bombSound);
                GameObject BombObj = Instantiate(bombObj, new Vector3(0,0,0), Quaternion.identity);
                Destroy(BombObj, 0.25f);
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) obj.GetComponent<Unit>().TakeDamage(bombDamage);
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(obj);
                bombTime--;
                bombCurTime = bombCoolTime;
            }
            else if (bombCurTime > 0 && bombTime > 0) StartCoroutine(InGameUIManager.instance.OnWaringText("스킬이 아직 쿨타임입니다!"));
            else if (bombTime == 0) StartCoroutine(InGameUIManager.instance.OnWaringText("스킬을 모두 소모했습니다!"));
        }
    }
    public void HpUpSkill()
    {
        if (hpUpCurTime > 0) hpUpCurTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (hpUpCurTime <= 0 && hpUpTime > 0)
            {
                SoundManager.instance.SFXPlay("HpUpSkill", hpUpSound);
                HpUp(hpUpValue);
                hpUpTime--;
                hpUpCurTime = hpUpCoolTime;
            }
            else if (hpUpCurTime > 0 && hpUpTime > 0) StartCoroutine(InGameUIManager.instance.OnWaringText("스킬이 아직 쿨타임입니다!"));
            else if (hpUpTime == 0) StartCoroutine(InGameUIManager.instance.OnWaringText("스킬을 모두 소모했습니다!"));
        }
    }

    public void SkillReset()
    {
        bombTime = 3;
        hpUpTime = 3;
        bombCurTime = 0;
        hpUpCurTime = 0;
    }
}
