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

    public BulletType bulletType;
    public int damage;
    public float speed;
    public float spread;
    public GameObject hitEffectObj;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
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
            Instantiate(hitEffectObj, transform.position, Quaternion.identity);
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
