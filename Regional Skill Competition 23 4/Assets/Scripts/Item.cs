using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        HpUp,
        FuelUp,
        InvcOn,
        BulletUp
    }

    public ItemType itemType;

    private void Update()
    {
        transform.Translate(0, -3 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(itemType)
            {
                case ItemType.HpUp:
                    collision.GetComponent<Player>().HpUp(10);
                    break;
                case ItemType.FuelUp:
                    collision.GetComponent<Player>().FuelUp();
                    break;
                case ItemType.InvcOn:
                    collision.GetComponent<Player>().InvcTimeUp(3);
                    break;
                case ItemType.BulletUp:
                    collision.GetComponent<Player>().BulletUp();
                    break;
            }
            InGameManager.instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
