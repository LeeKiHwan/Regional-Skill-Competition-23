using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Unit
{
    [Header("Player Status")]
    public float fuel;
    public float invcTime;
    public int bulletLevel;
    public float autoFireCurTime;
    public float autoFireRate;

    [Header("Bomb Skill")]
    public int bombTime;
    public float bombCurTime;
    public float bombCoolTime;
    public int bombDamage;

    [Header("HpUp Skill")]
    public int HpUpTime;
    public float HpUpCurTime;
    public float HpUpCoolTime;
    public int hpUpValue;

    void Update()
    {
        FuelDown();
        Fire();
        Move();
        InvcTimeDown();

        BombSkill();
        HpUpSkill();
    }

    public override void TakeDamage(int damage)
    {
        if (invcTime > 0) return;
        else
        {
            base.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        StartCoroutine(GameManager.instance.PlayerDie());
        Destroy(gameObject);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.J) && fireCurTime <= 0)
        {
            BulletFire();
            fireCurTime = fireRate;
        }

        if (autoFireCurTime > 0) autoFireCurTime -= Time.deltaTime;
        else
        {
            AutoFire();
            autoFireCurTime = autoFireRate;
        }
    }

    void BulletFire()
    {
        float dis = 0.25f;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 1; i <= bulletLevel; i++)
        {
            Vector3 newPos = pos;
            if (bulletLevel % 2 != 0)
            {
                if (i == 2) newPos.x += dis * 2;
                if (i == 3) newPos.x -= dis * 2;
                Instantiate(BulletObj[0], newPos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            }
            else if (bulletLevel % 2 == 0)
            {
                if (i == 1) newPos.x += dis;
                else if (i == 2) newPos.x -= dis;
                else if (i == 3) newPos.x -= dis * 2;
                else if (i == 4) newPos.x += dis * 2;
                Instantiate(BulletObj[0], newPos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            }
        }
    }

    void AutoFire()
    {
        if (GameObject.FindGameObjectWithTag("Monster"))
        {
            Transform Monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Transform>();
            Vector2 dir = (Vector2)Monster.position - (Vector2)transform.position;

            BulletObj[0].transform.position = transform.position;
            BulletObj[0].transform.up = dir.normalized;

            Instantiate(BulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        }
    }

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);
    }

    public void HpUp(int hp)
    {
        if (this.hp + hp >= 100) this.hp = 100;
        else this.hp += hp;
    }

    void FuelDown()
    {
        if (fuel > 0) fuel -= Time.deltaTime;
        else Die();
    }
    public void FuelUp()
    {
        fuel = 100;
    }

    void InvcTimeDown()
    {
        if (invcTime > 0)
        {
            invcTime -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    public void OnInvcTime(float time)
    {
        if (invcTime >= time) return;
        else invcTime = time;
    }

    public void BulletUpgrade()
    {
        if (bulletLevel < 4)
        {
            bulletLevel++;
            autoFireRate -= 0.1f;
        }
    }

    public void BombSkill()
    {
        if (bombCurTime > 0) bombCurTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (bombCurTime <= 0 && bombTime > 0)
            {
                foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Monster")) Obj.GetComponent<Unit>().TakeDamage(bombDamage);
                foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("MonsterBullet")) Destroy(Obj);

                bombCurTime = bombCoolTime;
                bombTime--;
            }
            else if (bombCurTime > 0 && bombTime > 0) StartCoroutine(UIManager.instance.WaringTextShow("Skill's On CoolTime!"));
            else if (bombTime == 0) StartCoroutine(UIManager.instance.WaringTextShow("Used All Skill!"));
        }
    }
    public void HpUpSkill()
    {
        if (HpUpCurTime > 0) HpUpCurTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (HpUpCurTime <= 0 && HpUpTime > 0)
            {
                HpUp(hpUpValue);

                HpUpCurTime = HpUpCoolTime;
                HpUpTime--;
            }
            else if (HpUpCurTime > 0 && HpUpTime > 0) StartCoroutine(UIManager.instance.WaringTextShow("Skill's On CoolTime!"));
            else if (HpUpTime == 0) StartCoroutine(UIManager.instance.WaringTextShow("Used All Skill!"));
        }
    }
}
