using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    enum MonsterType
    {
        assault,
        tank,
        speed
    }

    [SerializeField] private MonsterType monsterType;
    [SerializeField] private Transform player;
    [SerializeField] private int score;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    protected override void Move()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    protected override void Fire()
    {

        if (fireTime > 0) fireTime -= Time.deltaTime;
        else
        {
            switch (monsterType)
            {
                case MonsterType.assault:
                    StartCoroutine(AssaultFire());
                    break;

                case MonsterType.tank:
                    TankFire();
                    break;

                case MonsterType.speed:
                    SpeedFire();
                    break;
            }
        }
    }

    protected override void Die()
    {
        GameManager.Instance.AddScore(score);
        Destroy(gameObject);
    }

    IEnumerator AssaultFire()
    {
        fireTime = fireRate;

        for (int i = 0; i < 3; i++)
        {
            Vector2 dir = (Vector2)player.position - (Vector2)transform.position;

            bulletObj[0].transform.up = dir.normalized;
            bulletObj[0].transform.position = transform.position;

            Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            yield return new WaitForSeconds(0.25f);
        }

        yield break;
    }
    private void TankFire()
    {
        for (int i = 0; i < 4; i++)
        {
            float randX = Random.Range(-3.0f, 3.0f);
            float randY = Random.Range(-3.0f, 3.0f);
            Vector2 randPlayerPos = new Vector2(player.position.x + randX, player.position.y + randY);

            Vector2 dir = randPlayerPos - (Vector2)transform.position;

            bulletObj[0].transform.up = dir.normalized;
            bulletObj[0].transform.position = transform.position;

            Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        }
        fireTime = fireRate;
    }
    private void SpeedFire()
    {
        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;

        bulletObj[0].transform.up = dir.normalized;
        bulletObj[0].transform.position = transform.position;

        Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        fireTime = fireRate;
    }
}
