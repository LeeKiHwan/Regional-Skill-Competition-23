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
            CircleFire(15, pos);
            pos = new Vector3(transform.position.x + 2, transform.position.y, 0);
            CircleFire(15, pos);
        }
        else if (rand == 1) StartCoroutine(BirstFire(5, 5));
        else if (rand == 2) SniperFire();
    }

    private void SecondBossFire()
    {

    }

    private void ThirdBossFire()
    {

    }

    private void CircleFire(int count, Vector3 pos)
    {
        for (int i = 0; i < count; i++)
        {
            float randX = Random.Range(-6.0f, 6.0f);
            float randY = Random.Range(-6.0f, 6.0f);
            Vector2 randPlayerPos = new Vector2(player.position.x + randX, player.position.y + randY);

            Vector2 dir = randPlayerPos - (Vector2)transform.position;

            bulletObj[0].transform.up = dir.normalized;
            bulletObj[0].transform.position = pos;
            bulletSpeed = 2f;

            Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        }

        fireTime = fireRate;
    }

    IEnumerator BirstFire(int count, int birstCount)
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
                bulletSpeed = 6.5f;

                Instantiate(bulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.75f);
        }
        isBirstFiring=false;
        yield break;
    }

    private void SniperFire()
    {
        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;

        bulletObj[1].transform.up = dir.normalized;
        bulletObj[1].transform.position = transform.position;
        bulletSpeed = 10;

        Instantiate(bulletObj[1]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
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
        }
    }

    private void Hp()
    {
        HpSlider.value = hp / maxHp;
    }
}
