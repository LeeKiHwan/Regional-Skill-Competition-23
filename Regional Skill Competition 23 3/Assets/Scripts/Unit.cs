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
    public float bulletSpeed;
    public int bulletLevel;
    public float fireRate;
    public float fireCurTime;
    public bool assaultFiring;

    public abstract void Move();

    public abstract void Fire();

    public virtual void TakeDamage(int damage)
    {
        if (hp - damage <= 0) Die();
        else hp -= damage;
    }

    public abstract void Die();

    public void TankShot(int bulletCount, int bulletDamage, float bulletSpeed, float angle, Vector2 startPos)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float x = Random.Range(-angle, angle);
            float y = Random.Range(-angle, angle);

            Vector2 randPlayerPos = new Vector2(InGameManager.instance.PlayerObj.transform.position.x + x, InGameManager.instance.PlayerObj.transform.position.y + y);
            Vector2 dir = randPlayerPos - startPos;

            GameObject bullet = Instantiate(BulletObj[0], startPos, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            bullet.transform.up = dir.normalized;
        }
    }

    public IEnumerator AssaultShot(int bulletCount, int bulletDamage, float bulletSpeed, float fireRate)
    {
        if (assaultFiring) yield break;
        assaultFiring = true;

        for (int i = 0; i < bulletCount;i++)
        {
            Vector3 dir = InGameManager.instance.PlayerObj.transform.position - transform.position;

            BulletObj[0].transform.position = transform.position;
            BulletObj[0].transform.up = dir.normalized;

            Instantiate(BulletObj[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);

            yield return new WaitForSeconds(fireRate);
        }

        assaultFiring = false;
        yield break;
    }
}
