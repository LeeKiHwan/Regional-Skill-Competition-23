using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public enum MonsterType
    {
        Tank,
        Assault,
        Speed,
        Meteor
    }

    [Header("Monster Status")]
    public MonsterType monsterType;
    public int score;
    public GameObject[] ItemObj;

    private void Update()
    {
        Move();
        Fire();
    }

    public override void Die()
    {
        GameManager.instance.AddScore(score);
        DropItem();
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

    public void DropItem()
    {
        int rand = Random.Range(0, 10);

        switch(rand)
        {
            case 0: case 1: case 2:
                Instantiate(ItemObj[0], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(ItemObj[1], transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(ItemObj[2], transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(ItemObj[3], transform.position, Quaternion.identity);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && monsterType == MonsterType.Meteor)
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(bulletDamage);
        }
        if (collision.gameObject.name == "MonsterKillZone")
        {
            Destroy(gameObject);
        }
    }
}
