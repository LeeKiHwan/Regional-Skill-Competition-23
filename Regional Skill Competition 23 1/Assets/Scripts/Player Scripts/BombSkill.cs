using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : MonoBehaviour
{
    [SerializeField] private float damage;

    private void Awake()
    {
        Destroy(gameObject, 0.25f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null && collision.GetComponent<Bullet>().bulletType == Bullet.BulletType.monsterBullet) Destroy(collision.gameObject);
        if (collision.GetComponent<Monster>() != null) collision.GetComponent<Unit>().TakeDamage(damage);
    }
}
