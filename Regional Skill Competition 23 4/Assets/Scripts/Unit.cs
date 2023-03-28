using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Status")]
    public int hp;
    public float speed;

    [Header("Bullet Status")]
    public GameObject[] bulletObjs;
    public int bulletDamage;
    public float bulletSpeed;
    public float bulletSpread;
    public int bulletLevel;
    public float fireCurTime;
    public float fireRate;

    public abstract void Move();
    public abstract void Fire();
    public virtual void TakeDamage(int damage)
    {
        if (hp - damage <= 0) Die("");
        else hp -= damage;
    }
    public abstract void Die(string dieMessage);

    //Fire
    public void TankFire(int bulletCount, int bulletDamage, float bulletSpeed, float angle, Vector2 startPos)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 dir = new Vector2(InGameManager.instance.playerObj.transform.position.x + Random.Range(-angle , angle), InGameManager.instance.playerObj.transform.position.y + Random.Range(-angle, angle)) - startPos;

            bulletObjs[0].transform.position = startPos;
            bulletObjs[0].transform.up = dir.normalized;

            Instantiate(bulletObjs[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed, 0);
        }
    }
    public IEnumerator AssaultFire(int bulletCount, int bulletDamage, float bulletSpeed, float fireRate, float angle)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 dir = new Vector2(InGameManager.instance.playerObj.transform.position.x + Random.Range(-angle, angle), InGameManager.instance.playerObj.transform.position.y + Random.Range(-angle, angle)) - (Vector2)transform.position;

            bulletObjs[0].transform.position = transform.position;
            bulletObjs[0].transform.up = dir.normalized;

            Instantiate(bulletObjs[0]).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed, 0);
            yield return new WaitForSeconds(fireRate);
        }

        yield break;
    }
    public void MonsterSpawn(int monsterArr, Vector2 startPos)
    {
        Instantiate(InGameManager.instance.monsterObjs[monsterArr], startPos, Quaternion.identity);
    }
    public void RegularFire(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {

        }
    }
}
