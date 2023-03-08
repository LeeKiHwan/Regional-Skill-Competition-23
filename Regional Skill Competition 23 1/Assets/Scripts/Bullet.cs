using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        playerBullet,
        monsterBullet
    }

    [Header("BulletStatus")]
    [SerializeField] public BulletType bulletType;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        Move();
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
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
