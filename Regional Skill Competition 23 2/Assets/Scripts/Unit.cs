using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Status")]
    public int hp;
    public float speed;

    [Header("Bullet Status")]
    public GameObject[] BulletObj;
    public int bulletDamage;
    public int bulletSpeed;
    public float fireCurTime;
    public float fireRate;
    public bool BirstFiring;

    public virtual void TakeDamage(int damage)
    {
        if (hp > damage) hp -= damage;
        else Die();
    }

    public abstract void Die();
    public abstract void Move();
    public abstract void Fire();

    public void TankFire(int bulletLevel, int bulletCount, int bulletDamage, int bulletSpeed, float angle, Vector2 startPos)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float randX = Random.Range(-angle, angle);
            float randY = Random.Range(-angle, angle);
            Vector2 randPlayerPos = new Vector2(GameManager.instance.PlayerObj.transform.position.x + randX, GameManager.instance.PlayerObj.transform.position.y + randY);
            Vector2 dir = randPlayerPos - startPos;

            BulletObj[bulletLevel].transform.position = transform.position;
            BulletObj[bulletLevel].transform.up = dir.normalized;

            Instantiate(BulletObj[bulletLevel]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
        }
    }

    public IEnumerator AssaultFire(int bulletLevel, int bulletCount, int bulletDamage, int bulletSpeed, float fireRate)
    {
        if (BirstFiring) yield break;
        else BirstFiring = true;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 dir = (Vector2)GameManager.instance.PlayerObj.transform.position - (Vector2)transform.position;

            BulletObj[bulletLevel].transform.position = transform.position;
            BulletObj[bulletLevel].transform.up = dir.normalized;

            Instantiate(BulletObj[bulletLevel]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            yield return new WaitForSeconds(fireRate);
        }

        BirstFiring = false;
        yield break;
    }
}