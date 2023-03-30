using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public enum MonsterType
    {
        Assault,
        Tank,
        Speed,
        Big,
        Meteor
    }

    public MonsterType monsterType;
    public int score;
    public GameObject[] item;

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
        if (monsterType == MonsterType.Big)
        {
            if (transform.position.y > 3) transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;

        else
        {
            switch (monsterType)
            {
                case MonsterType.Assault:
                    StartCoroutine(AssaultFire(5, bulletDamage, bulletSpeed, 0.25f, bulletSpread, transform.position));
                    break;
                case MonsterType.Tank:
                    TankFire(6, bulletDamage, bulletSpeed, 3, transform.position);
                    break;
                case MonsterType.Speed:
                    StartCoroutine(AssaultFire(1, bulletDamage, bulletSpeed, 0, bulletSpread, transform.position));
                    break;
                case MonsterType.Big:
                    StartCoroutine(AssaultFire(10, bulletDamage, bulletSpeed, 0.2f, 3, transform.position));
                    break;
            }
            fireCurTime = fireRate;
        }
    }

    public override void Die(string dieMessage)
    {
        DropItem();
        InGameManager.instance.AddScore(score);
        Destroy(gameObject);
    }

    public void DropItem()
    {
        int rand = Random.Range(0, 10);
        switch(rand)
        {
            case 0: case 1: case 2:
                Instantiate(item[0], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(item[1], transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(item[2], transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(item[3], transform.position, Quaternion.identity);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && monsterType == MonsterType.Meteor)
        {
            collision.GetComponent<Player>().TakeDamage(bulletDamage);
        }
        if (collision.gameObject.name == "MonsterKillZone")
        {
            Destroy(gameObject);
        }
    }
}
