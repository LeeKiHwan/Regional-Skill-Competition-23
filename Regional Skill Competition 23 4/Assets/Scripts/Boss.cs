using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    public enum BossType
    {
        FirstBoss,
        SecondBoss,
        ThirdBoss
    }

    public BossType bossType;
    public int maxHp;
    public int score;
    public bool isDie;
    public GameObject DieEffectObj;

    private void Update()
    {
        Move();
        if (InGameManager.instance.playerClass)
        {
            Fire();
        }
    }
    public override void Move()
    {
        if (transform.position.y > 4)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }

    public override void Fire()
    {
        if (!isDie)
        {
            if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
            else
            {
                switch (bossType)
                {
                    case BossType.FirstBoss:
                        FirstBossFire();
                        break;
                    case BossType.SecondBoss:
                        SecondBossFire();
                        break;
                    case BossType.ThirdBoss:
                        ThirdBossFire();
                        break;
                }
                fireCurTime = fireRate;
            }
        }
    }
    public void FirstBossFire()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(8, bulletDamage, bulletSpeed, 0.2f, 0));
                break;
            case 1:
                TankFire(10, bulletDamage, bulletSpeed, 3, transform.position);
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 1, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed, 0.15f, 3));
                break;
        }
    }
    public void SecondBossFire()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed, 0.15f, 0));
                break;
            case 1:
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x + 1, transform.position.y));
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x - 1, transform.position.y));
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 0.5f, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed, 0.15f, 3));
                break;
        }
    }
    public void ThirdBossFire()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed, 0.15f, 0));
                break;
            case 1:
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x + 1, transform.position.y));
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x - 1, transform.position.y));
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 0.5f, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed, 0.15f, 3));
                break;
        }
    }


    public override void Die(string dieMessage)
    {
        if (!isDie)
        {
            isDie = true;
            InGameManager.instance.AddScore(score);
            InGameManager.instance.BossClear();
            Destroy(gameObject);
        }
    }
}
