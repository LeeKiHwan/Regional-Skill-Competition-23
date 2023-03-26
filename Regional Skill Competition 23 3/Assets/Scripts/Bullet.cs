using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        PlayerBullet,
        MonsterBullet
    }

    [Header("Status")]
    public BulletType bulletType;
    public int damage;
    public float speed;
    public float spread;
    public GameObject destroyEffect;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(spread * Time.deltaTime, speed * Time.deltaTime, 0);
    }

    public void SetBulletStatus(int damage, float speed, float spread)
    {
        this.damage = damage;
        this.speed = speed;
        this.spread = spread;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && bulletType == BulletType.PlayerBullet)
        {
            collision.GetComponent<Unit>().TakeDamage(damage);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && bulletType == BulletType.MonsterBullet)
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
