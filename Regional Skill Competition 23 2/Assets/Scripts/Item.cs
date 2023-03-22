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
        transform.Translate(0, -2 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
            switch(itemType)
            {
                case ItemType.HpUp:
                    player.HpUp(10);
                    break;
                case ItemType.FuelUp:
                    player.FuelUp();
                    break;
                case ItemType.InvcOn:
                    player.OnInvcTime(3);
                    break;
                case ItemType.BulletUp:
                    player.BulletUpgrade();
                    break;
            }
            GameManager.instance.AddScore(50);
            Destroy(gameObject);
        }
    }
}
