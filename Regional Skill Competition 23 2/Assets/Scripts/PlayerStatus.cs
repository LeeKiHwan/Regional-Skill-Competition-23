using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Unit
{
    [Header("Player Status")]
    public float fuel;
    public float invcTime;
    public int bulletLevel;


    void Update()
    {
        FuelDown();
        Fire();
        Move();
        InvcTimeDown();
    }

    public override void TakeDamage(int damage)
    {
        if (invcTime > 0) return;
        else
        {
            base.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void Fire()
    {
        if (fireCurTime > 0) fireCurTime -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.J) && fireCurTime <= 0)
        {
            BulletFire();
            fireCurTime = fireRate;
        }
    }

    void BulletFire()
    {
        float x = 1;
        float dis = 0.25f;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 1; i <= bulletLevel; i++)
        {
            if (bulletLevel % 2 != 0)
            {
                Instantiate(BulletObj[0], pos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
                pos.x = x * 0.4f;
                x *= -1;
            }
            else if (bulletLevel % 2 == 0)
            {
                if (i == 3) dis += 0.3f;
                pos.x = x * dis;
                Instantiate(BulletObj[0], pos, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
                x *= -1;
            }
        }
    }

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);
    }

    public void HpUp(int hp)
    {
        if (this.hp + hp >= 100) this.hp = 100;
        else this.hp += hp;
    }

    void FuelDown()
    {
        if (fuel > 0) fuel -= Time.deltaTime;
        else Die();
    }
    public void FuelUp()
    {
        fuel = 100;
    }

    void InvcTimeDown()
    {
        invcTime -= Time.deltaTime;
    }
    public void OnInvcTime(float time)
    {
        if (invcTime >= time) return;
        else invcTime = time;
    }


}
