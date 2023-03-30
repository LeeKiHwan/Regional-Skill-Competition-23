using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public int damage;
    public float curTime;
    public float destroyTime;
    public GameObject effectObj;

    private void Awake()
    {
        StartCoroutine(AttackTimeDown());
    }

    private void Update()
    {

    }

    public void SetAttackInfo(int damage, float curTime, float destroyTime, Vector3 scale)
    {
        this.damage = damage;
        this.curTime = curTime;
        this.destroyTime = destroyTime;
        transform.localScale = scale;
    }

    public IEnumerator AttackTimeDown()
    {
        float targetTime = curTime;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, curTime / targetTime, curTime / targetTime);
            yield return null;
        }
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        GameObject obj = Instantiate(effectObj, transform.position, Quaternion.identity);
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        obj.transform.parent = transform;

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
