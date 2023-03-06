using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float hp;
    [SerializeField] protected float speed;

    [Header("BulletStatus")]
    [SerializeField] protected GameObject[] bulletObj;
    [SerializeField] protected int bulletLevel;
    [SerializeField] protected float bulletDamage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float fireRate;
    protected float fireTime;
    public void TakeDamage(float damage)
    {
        if (hp > damage) hp -= damage;
        else Die();
    }

    protected abstract void Die();

    protected abstract void Fire();

    protected abstract void Move();
}
