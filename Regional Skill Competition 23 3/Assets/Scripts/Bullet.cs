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
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void SetBulletStatus(int damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && bulletType == BulletType.PlayerBullet)
        {
            collision.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && bulletType == BulletType.MonsterBullet)
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
