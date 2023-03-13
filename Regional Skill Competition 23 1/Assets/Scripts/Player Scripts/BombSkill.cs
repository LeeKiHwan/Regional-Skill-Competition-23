using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : MonoBehaviour
{
    [SerializeField] private float damage;

    private void Awake()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Monster"))
        {
            obj.GetComponent<Unit>().TakeDamage(damage);
        }
        foreach(Bullet obj in FindObjectsOfType<Bullet>())
        {
            Destroy(obj.gameObject);
        }

        Destroy(gameObject, 0.25f);
    }


}
