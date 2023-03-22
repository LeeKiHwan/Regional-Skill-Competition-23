using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    public enum BossType
    {
        Boss1,
        Boss2,
        Boss3
    }

    public BossType bossType;
    public int score;
    public int maxHp;

    private void Update()
    {
        Fire();
        Move();
    }

    public override void Die()
    {
        GameManager.instance.AddScore(score);
        GameManager.instance.ClearStage(GameManager.instance.currentStage);
        UIManager.instance.BossHpSliderOnOff(false);
        Destroy(gameObject);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;

        if (fireCurTime <= 0)
        {
            switch (bossType)
            {
                case BossType.Boss1:
                    FirstBossFire();
                    break;
                case BossType.Boss2:
                    SecondBossFire();
                    break;
                case BossType.Boss3:
                    ThirdBossFire();
                    break;
            }
            fireCurTime = fireRate;
        }
    }
    
    public void FirstBossFire()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(0, 10, bulletDamage, bulletSpeed, 0.1f));
                break;
            case 1:
                TankFire(0, 15, bulletDamage, bulletSpeed, 8, new Vector2(transform.position.x, transform.position.y));
                break;
            case 2:
                StartCoroutine(AssaultFire(0, 3, bulletDamage, bulletSpeed + 5, 1));
                break;
        }
    }

    public void SecondBossFire()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(0, 10, bulletDamage, bulletSpeed, 0.1f));
                break;
            case 1:
                TankFire(0, 10, bulletDamage, bulletSpeed, 10, new Vector2(transform.position.x + 3, transform.position.y));
                TankFire(0, 10, bulletDamage, bulletSpeed, 10, new Vector2(transform.position.x - 3, transform.position.y));
                break;
            case 2:
                StartCoroutine(AssaultFire(0, 5, bulletDamage, bulletSpeed + 5, 0.5f));
                break;
        }
    }
    
    public void ThirdBossFire()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                StartCoroutine(AssaultFire(0, 10, bulletDamage, bulletSpeed, 0.1f));
                break;
            case 1:
                TankFire(0, 10, bulletDamage, bulletSpeed, 10, new Vector2(transform.position.x + 3, transform.position.y));
                TankFire(0, 10, bulletDamage, bulletSpeed, 10, new Vector2(transform.position.x - 3, transform.position.y));
                TankFire(0, 10, bulletDamage, bulletSpeed, 10, new Vector2(transform.position.x, transform.position.y + 5));
                break;
            case 2:
                StartCoroutine(AssaultFire(0, 2, bulletDamage, bulletSpeed + 5, 1));
                break;
        }
    }

    public override void Move()
    {
        switch (bossType)
        {
            case BossType.Boss1:
                if (transform.position.y > 3.8f) transform.Translate(0, -speed * Time.deltaTime, 0);
                break;
            case BossType.Boss2:
                if (transform.position.y > 3.3f) transform.Translate(0, -speed * Time.deltaTime, 0);
                break;
            case BossType.Boss3:
                if (transform.position.y > 3.9f) transform.Translate(0, -speed * Time.deltaTime, 0);
                break;
        }
    }
}
