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
        Meteor
    }

    public MonsterType monsterType;
    public int score;
    public GameObject[] ItemObjs;

    private void Update()
    {
        Move();
        Fire();
    }

    public override void Move()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
        else
        {
            if (InGameManager.instance.PlayerObj)
            {
                switch (monsterType)
                {
                    case MonsterType.Assault:
                        StartCoroutine(AssaultShot(3, bulletDamage, bulletSpeed, 0.2f));
                        break;
                    case MonsterType.Tank:
                        TankShot(5, bulletDamage, bulletSpeed, 3, transform.position);
                        break;
                    case MonsterType.Speed:
                        StartCoroutine(AssaultShot(1, bulletDamage, bulletSpeed, 0));
                        break;
                }
            }

            fireCurTime = fireRate;
        }
    }

    public override void Die()
    {
        InGameManager.instance.AddScore(score);
        DropItem();
        Destroy(gameObject);
    }

    public void DropItem()
    {
        int rand = Random.Range(0, 10);
        if (rand < 3) Instantiate(ItemObjs[0], transform.position, Quaternion.identity);
        else if (rand == 3) Instantiate(ItemObjs[1], transform.position, Quaternion.identity);
        else if (rand == 4) Instantiate(ItemObjs[2], transform.position, Quaternion.identity);
        else if (rand == 5) Instantiate(ItemObjs[3], transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && monsterType == MonsterType.Meteor) collision.GetComponent<Player>().TakeDamage(bulletDamage);
        if (collision.name == "MonsterKillZone") Destroy(gameObject);
    }
}
