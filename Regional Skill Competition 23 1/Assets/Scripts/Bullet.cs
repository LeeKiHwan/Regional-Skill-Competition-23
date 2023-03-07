using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private enum BulletType
    {
        playerBullet,
        monsterBullet
    }

    [Header("BulletStatus")]
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;

    private void Update()
    {
        Move();
        LifeTime();
    }

    private void Move()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
    }

    public void SetBulletStatus(float damage, float speed)
    {
        bulletDamage = damage;
        bulletSpeed = speed;
    }

    private void LifeTime()
    {
        if (lifeTime > 0) lifeTime -= Time.deltaTime;
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.playerBullet)
        {
            if (collision.CompareTag("Monster"))
            {
                collision.gameObject.GetComponent<Unit>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }

        if (bulletType == BulletType.monsterBullet)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Unit>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
