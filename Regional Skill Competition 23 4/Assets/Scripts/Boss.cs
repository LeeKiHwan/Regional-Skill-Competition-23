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

    [Header("Boss Attack Objs")]
    public GameObject BombAttackObj;

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
                        StartCoroutine(FirstBossFire());
                        break;
                    case BossType.SecondBoss:
                        StartCoroutine(SecondBossFire());
                        break;
                    case BossType.ThirdBoss:
                        StartCoroutine(ThirdBossFire());
                        break;
                }
                fireCurTime = fireRate;
            }
        }
    }
    public IEnumerator FirstBossFire()
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed + 3, 0.2f, 0));
                break;
            case 1:
                for (int i = 0; i<4;i++)
                {
                    TankFire(5, bulletDamage, bulletSpeed + 3, 3, transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 0.5f, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed + 3, 0.05f, 5));
                break;
            case 4:
                StartCoroutine(RegularFire(10, 3, 90, 20, 0.1f, 0));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(RegularFire(12, 3, 80, 20, 0.1f, 0));
                break;
            case 5:
                StartCoroutine(RegularFire(5, 1, 170, 5, 0, 1));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(RegularFire(5, 1, 170, 5, 0, -1));
                break;
        }
        yield break;
    }
    public IEnumerator SecondBossFire()
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed + 3, 0.15f, 0));
                break;
            case 1:
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x, transform.position.y));
                yield return new WaitForSeconds(1);
                TankFire(10, bulletDamage, bulletSpeed, 5, new Vector2(transform.position.x, transform.position.y));
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 0.5f, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed, 0.15f, 3));
                break;
            case 4:
                StartCoroutine(RegularFire(10, 4, 90, 20, 0.05f, 0));
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(RegularFire(12, 4, 80, 20, 0.05f, 0));
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(RegularFire(10, 4, 90, 20, 0.05f, 0));
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(RegularFire(12, 4, 80, 20, 0.05f, 0));
                break;
            case 5:
                MonsterSpawn(4, new Vector2(transform.position.x + (Random.Range(-2.5f, 2.5f)), transform.position.y));
                yield return new WaitForSeconds(0.5f);
                MonsterSpawn(4, new Vector2(transform.position.x + (Random.Range(-2.5f, 2.5f)), transform.position.y));
                yield return new WaitForSeconds(0.5f);
                MonsterSpawn(4, new Vector2(transform.position.x + (Random.Range(-2.5f, 2.5f)), transform.position.y));
                break;
        }
        yield break;
    }
    public IEnumerator ThirdBossFire()
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed, 0.1f, 0));
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    TankFire(5, bulletDamage, bulletSpeed, 2, new Vector2(transform.position.x, transform.position.y));
                    yield return new WaitForSeconds(0.25f);
                }
                break;
            case 2:
                StartCoroutine(AssaultFire(3, bulletDamage, bulletSpeed + 5, 0.3f, 0));
                break;
            case 3:
                StartCoroutine(AssaultFire(15, bulletDamage, bulletSpeed, 0.15f, 3));
                break;
            case 4:
                for (int i =0; i< 4; i++)
                {
                    StartCoroutine(RegularFire(10, 1, 90, 20, 0, 0));
                    yield return new WaitForSeconds(0.1f);
                    StartCoroutine(RegularFire(12, 1, 80, 20, 0, 0));
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case 5:
                int randMon = Random.Range(0, 4);
                MonsterSpawn(randMon, new Vector2(transform.position.x + 0.25f, transform.position.y - 0.5f));
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
