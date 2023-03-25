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
        if (collision.CompareTag("Player"))
        {
            switch(itemType)
            {
                case ItemType.HpUp:
                    InGameManager.instance.PlayerClass.HpUp(20);
                    break;
                case ItemType.FuelUp:
                    InGameManager.instance.PlayerClass.FuelUp();
                    break;
                case ItemType.InvcOn:
                    InGameManager.instance.PlayerClass.InvcTimeUp(3);
                    break;
                case ItemType.BulletUp:
                    InGameManager.instance.PlayerClass.BulletLevelUp();
                    break;
            }
            InGameManager.instance.AddScore(50);
            Destroy(gameObject);
        }
    }
}
