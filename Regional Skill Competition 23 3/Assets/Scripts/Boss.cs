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

    private void Update()
    {
        Move();
        Fire();
    }

    public override void Move()
    {
        if (transform.position.y > 4) transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    public override void Fire()
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
        }
    }

    public void FirstBossFire()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                TankShot(15, bulletDamage, bulletSpeed, 8, transform.position);
                break;
            case 1:
                StartCoroutine(AssaultShot(5, bulletDamage, bulletSpeed, 0.3f));
                break;
            case 2:
                StartCoroutine(AssaultShot(2, bulletDamage, bulletSpeed + 5, 0.5f));
                break;
        }
        fireCurTime = fireRate;
    }

    public void SecondBossFire()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                TankShot(10, bulletDamage, bulletSpeed, 6, new Vector3(transform.position.x + 2, transform.position.y, 0));
                TankShot(10, bulletDamage, bulletSpeed, 6, new Vector3(transform.position.x - 2, transform.position.y, 0));
                break;
            case 1:
                StartCoroutine(AssaultShot(8, bulletDamage, bulletSpeed, 0.2f));
                break;
            case 2:
                StartCoroutine(AssaultShot(3, bulletDamage, bulletSpeed + 5, 0.3f));
                break;
        }
        fireCurTime = fireRate;
    }

    public void ThirdBossFire()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                TankShot(10, bulletDamage, bulletSpeed, 6, new Vector3(transform.position.x + 3, transform.position.y, 0));
                TankShot(10, bulletDamage, bulletSpeed, 6, new Vector3(transform.position.x - 3, transform.position.y, 0));
                TankShot(10, bulletDamage, bulletSpeed, 6, new Vector3(transform.position.x, transform.position.y + 1, 0));
                break;
            case 1:
                StartCoroutine(AssaultShot(10, bulletDamage, bulletSpeed, 0.1f));
                break;
            case 2:
                StartCoroutine(AssaultShot(3, bulletDamage, bulletSpeed + 5, 0.3f));
                break;
        }
        fireCurTime = fireRate;
    }

    public override void Die()
    {
        InGameManager.instance.AddScore(score);
        InGameManager.instance.BossClear();
        Destroy(gameObject);
    }
}
