using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [Header("Player Status")]
    public float fuel;
    public float invcTime;
    public float bulletDis;
    public float autoFireCurTime;
    public float autoFireRate;

    [Header("Bomb Status")]
    public GameObject bombEffectObj;
    public int bombDamage;
    public int bombTime;
    public float bombCurTime;
    public float bombRate;

    [Header("HpUp Status")]
    public int hpUpValue;
    public int hpUpTime;
    public float hpUpCurTime;
    public float hpUpRate;

    [Header("Player Sound")]
    public AudioClip fireSound;
    public AudioClip bombSound;
    public AudioClip hpUpSound;

    private void Update()
    {
        Move();
        Fire();
        AutoFire();
        FuelDown();
        InvcTimeDown();
        
        BombSkill();
        HpUpSkill();
    }

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);
    }
    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
        else if (fireCurTime <= 0 && Input.GetKey(KeyCode.Keypad1))
        {
            for (int i = 1; i <= bulletLevel; i++)
            {
                Instantiate(bulletObjs[0], new Vector3(transform.position.x + (bulletDis * i), transform.position.y, 0), Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed, bulletSpread * i);
                Instantiate(bulletObjs[0], new Vector3(transform.position.x - (bulletDis * i), transform.position.y, 0), Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed, -bulletSpread * i);
            }
            SoundManager.instance.SFXPlay("PlayerFire", fireSound);
            fireCurTime = fireRate;
        }
    }
    public void AutoFire()
    {
        if (autoFireCurTime > 0) autoFireCurTime -= Time.deltaTime;
        else if (autoFireCurTime <= 0 && GameObject.FindGameObjectWithTag("Monster"))
        {
            Vector2 dir = (Vector2)GameObject.FindGameObjectWithTag("Monster").transform.position - (Vector2)transform.position;

            bulletObjs[1].transform.position = transform.position;
            bulletObjs[1].transform.up = dir.normalized;

            Instantiate(bulletObjs[1]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed, 0);

            autoFireCurTime = autoFireRate;
        }
    }


    public override void TakeDamage(int damage)
    {
        if (invcTime <= 0)
        {
            if (hp - damage <= 0)
            {
                hp = 0;
                Die("내구도 부족");
            }
            else
            {
                hp -= damage;
                InvcTimeUp(1.5f);
            }
        }
    }
    public override void Die(string dieMessage)
    {
        InGameManager.instance.PlayerDie(dieMessage);
        Destroy(gameObject);
    }

    public void HpUp(int addHp)
    {
        if (hp + addHp >= 100) hp = 100;
        else hp += addHp;
    }
    
    public void BulletUp()
    {
        if (bulletLevel < 4)
        {
            bulletLevel++;
            autoFireRate -= 0.05f;
        }
    }

    public void FuelUp()
    {
        fuel = 100;
    }
    public void FuelDown()
    {
        if (fuel > 0 && InGameManager.instance.monsterSpawnable) fuel -= Time.deltaTime * 2;
        else if (fuel <= 0) Die("연료 부족");
    }

    public void InvcTimeUp(float addInvcTime)
    {
        if (invcTime < addInvcTime) invcTime = addInvcTime;
    }
    public void InvcTimeDown()
    {
        if (invcTime > 0)
        {
            invcTime -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void BombSkill()
    {
        if (bombCurTime > 0 && bombTime > 0) bombCurTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (bombCurTime <= 0)
            {
                SoundManager.instance.SFXPlay("PlayerBomb", bombSound);
                Instantiate(bombEffectObj, new Vector3(0,0,0), Quaternion.identity);
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster")) obj.GetComponent<Unit>().TakeDamage(bombDamage);
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(obj);
                bombTime--;
                bombCurTime = bombRate;
            }
            else if (bombCurTime > 0 && bombTime > 0)
            {
                StartCoroutine(InGameUIManager.instance.WarningTextOn("스킬이 쿨타임 중 입니다!"));
            }
            else if (bombTime == 0)
            {
                StartCoroutine(InGameUIManager.instance.WarningTextOn("스킬이 모두 소모했습니다!"));
            }
        }
    }
    public void HpUpSkill()
    {
        if (hpUpCurTime > 0 && hpUpTime > 0) hpUpCurTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (hp == 100)
            {
                StartCoroutine(InGameUIManager.instance.WarningTextOn("체력이 최대입니다!"));
            }
            else if (hpUpCurTime <= 0)
            {
                SoundManager.instance.SFXPlay("PlayerHpUp", hpUpSound);
                HpUp(hpUpValue);
                hpUpTime--;
                hpUpCurTime = hpUpRate;
            }
            else if (bombCurTime > 0 && bombTime > 0)
            {
                StartCoroutine(InGameUIManager.instance.WarningTextOn("스킬이 쿨타임 중 입니다!"));
            }
            else if (bombTime == 0)
            {
                StartCoroutine(InGameUIManager.instance.WarningTextOn("스킬이 모두 소모했습니다!"));
            }
        }
    }

    public void ResetStatus()
    {
        bombTime = 3;
        bombCurTime = 0;
        hpUpTime = 3;
        hpUpCurTime = 0;

        FuelUp();
    }
}
