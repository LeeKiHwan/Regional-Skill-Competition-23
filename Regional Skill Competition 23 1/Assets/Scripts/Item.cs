using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    enum ItemType
    {
        BulletUpgrade,
        InvincibilityOn,
        HpUp,
        FuelUp
    }

    [SerializeField] private ItemType itemType;

    private void Awake()
    {
        Destroy(gameObject, 8);
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(0, -3 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.BulletUpgrade:
                    collision.GetComponent<PlayerStatus>().BulletUpgrade();
                    break;
                case ItemType.InvincibilityOn:
                    collision.GetComponent<PlayerStatus>().InvincibilityOn(5);
                    break;
                case ItemType.HpUp:
                    collision.GetComponent<PlayerStatus>().HpUp(10);
                    break;
                case ItemType.FuelUp:
                    collision.GetComponent<PlayerStatus>().FuelUp();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
