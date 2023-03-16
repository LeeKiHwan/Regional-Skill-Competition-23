using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public enum MonsterType
    {
        Tank,
        Assault,
        Speed
    }

    [Header("Monster Status")]
    public MonsterType monsterType;
    public int score;

    private void Update()
    {
        Move();
        Fire();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        GameManager.instance.AddScore(score);
        Destroy(gameObject);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
        else
        {
            switch (monsterType)
            {
                case MonsterType.Tank:
                    TankFire(0, 4, bulletDamage, bulletSpeed, 3, new Vector2(transform.position.x, transform.position.y));
                    break;
                case MonsterType.Assault:
                    StartCoroutine(AssaultFire(0, 3, bulletDamage, bulletSpeed, 0.15f));
                    break;
                case MonsterType.Speed:
                    StartCoroutine(AssaultFire(0, 1, bulletDamage, bulletSpeed, 0));
                    break;
            }
            fireCurTime = fireRate;
        }
    }

    public override void Move()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}
