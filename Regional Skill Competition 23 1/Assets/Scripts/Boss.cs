using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit
{
    enum BossType
    {
        FirstBoss,
        SecondBoss,
        ThirdBoss
    }

    [SerializeField] private float maxHp;
    [SerializeField] private BossType bossType;
    [SerializeField] private Transform player;
    [SerializeField] private int score;
    [SerializeField] private Slider HpSlider;
    [SerializeField] private bool isBirstFiring;
    [SerializeField] private bool isMeteorShowering;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        HpSlider = GameObject.Find("Boss Hp Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        Fire();
        Move();
        Hp();
    }

    protected override void Die()
    {
        HpSlider.gameObject.SetActive(false);
        GameManager.Instance.AddScore(score);
        GameManager.Instance.KilledBoss();
        Destroy(gameObject);
    }

    protected override void Fire()
    {
        if (fireTime > 0) fireTime -= Time.deltaTime;
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

    private void FirstBossFire()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            Vector3 pos = new Vector3(transform.position.x - 2, transform.position.y, 0);
            CircleFire(15, pos, 2);
            pos = new Vector3(transform.position.x + 2, transform.position.y, 0);
            CircleFire(15, pos, 2);
        }
        else if (rand == 1) StartCoroutine(BirstFire(5, 5, 6.5f));
        else if (rand == 2) SniperFire(10);
    }

    private void SecondBossFire()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
            CircleFire(10, pos, 2);
            pos = new Vector3(transform.position.x - 2, transform.position.y, 0);
            CircleFire(10, pos, 2);
            pos = new Vector3(transform.position.x + 2, transform.position.y, 0);
            CircleFire(10, pos, 2);
        }
        else if (rand == 1) StartCoroutine(BirstFire(10, 3, 8f));
        else if (rand == 2) SniperFire(10);
        else if (rand == 3) StartCoroutine(MeteorShower(10));
    }

    private void ThirdBossFire()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0) StartCoroutine(CircleAttackCoroutine(10));
        else if (rand == 1) StartCoroutine(BirstFire(5, 1, 12f));
        else if (rand == 2) SniperFire(15);
        else if (rand == 3) StartCoroutine(MeteorShower(15));
    }

    IEnumerator CircleAttackCoroutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
            CircleFire(5, pos, 10);

            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }

    private void CircleFire(int count, Vector3 pos, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            float randX = Random.Range(-6.0f, 6.0f);
            float randY = Random.Range(-6.0f, 6.0f);
            Vector2 randPlayerPos = new Vector2(player.position.x + randX, player.position.y + randY);

            Vector2 dir = randPlayerPos - (Vector2)transform.position;

            bulletObj[0].transform.up = dir.normalized;
            bulletObj[0].transform.position = pos;
            bulletSpeed = speed;

            Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        }

        fireTime = fireRate;
    }

    IEnumerator BirstFire(int count, int birstCount, float speed)
    {
        fireTime = fireRate;

        if (isBirstFiring)
        {
            fireTime = 0;
            yield break;
        }
        else if (!isBirstFiring) isBirstFiring = true;
        
        for (int i =0; i < birstCount; i++)
        {
            for (int j = 0; j < count; j++)
            {
                Vector2 dir = (Vector2)player.position - (Vector2)transform.position;

                bulletObj[0].transform.up = dir.normalized;
                bulletObj[0].transform.position = transform.position;
                bulletSpeed = speed;

                Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.75f);
        }
        isBirstFiring=false;
        yield break;
    }

    private void SniperFire(float speed)
    {
        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;

        bulletObj[1].transform.up = dir.normalized;
        bulletObj[1].transform.position = transform.position;
        bulletSpeed = speed;

        Instantiate(bulletObj[1]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
    }

    IEnumerator MeteorShower(int count)
    {

        fireTime = fireRate;

        if (isMeteorShowering)
        {
            fireTime = 0;
            yield break;
        }
        else if (!isMeteorShowering) isMeteorShowering = true;

        for (int i = 0; i< count; i++)
        {
            float rand = Random.Range(-8.0f, 8.0f);

            Vector3 pos = new Vector3(transform.position.x + rand, transform.position.y + 3, transform.position.z);

            Instantiate(bulletObj[2], pos, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }

        isMeteorShowering = false;

        yield break;
    }

    protected override void Move()
    {
        switch (bossType)
        {
            case BossType.FirstBoss:
                if (transform.position.y > 3.5f) transform.Translate(0, -1.5f * Time.deltaTime, 0);
                break;
            case BossType.SecondBoss:
                if (transform.position.y > 3.8f) transform.Translate(0, -1.5f * Time.deltaTime, 0);
                break;
            case BossType.ThirdBoss:
                if (transform.position.y > 4) transform.Translate(0, -1.5f * Time.deltaTime, 0);
                break;
        }
    }

    private void Hp()
    {
        HpSlider.value = hp / maxHp;
    }
}
