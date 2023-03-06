using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Unit
{
    private void Update()
    {
        Move();
        Fire();
    }

    protected override void Die()
    {

    }

    protected override void Fire()
    {
        if (fireTime > 0) fireTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.J) && fireTime <= 0)
        {
            Instantiate(bulletObj[bulletLevel], transform.position, Quaternion.identity).GetComponent<Bullet>().SetBulletStatus(bulletDamage, bulletSpeed);
            fireTime = fireRate;
        }
    }

    protected override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);
    }
}
