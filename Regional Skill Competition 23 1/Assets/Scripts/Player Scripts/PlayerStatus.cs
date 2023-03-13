using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : Unit
{
    [Header("Player Status")]
    [SerializeField] private float fuel;
    [SerializeField] private float invincibilityTime;

    [Header("Player Status UI")]
    [SerializeField] private Slider PlayerHpSlider;
    [SerializeField] private TextMeshProUGUI PlayerHpText;
    [SerializeField] private Slider PlayerFuelSlider;
    [SerializeField] private TextMeshProUGUI PlayerFuelText;

    private void Awake()
    {
        if (GameObject.Find("Tutorial Manager")) return;
        PlayerHpSlider = GameObject.Find("Hp Slider").GetComponent<Slider>();
        PlayerHpText = GameObject.Find("Hp Text").GetComponent<TextMeshProUGUI>();

        PlayerFuelSlider = GameObject.Find("Fuel Slider").GetComponent<Slider>();
        PlayerFuelText = GameObject.Find("Fuel Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Move();
        Fire();
        FuelDown();
        InvincibilityTimeDown();
        PlayerStatusUI();
    }

    public override void TakeDamage(float damage)
    {
        if (invincibilityTime <= 0)
        {
            InvincibilityOn(1.5f);

            float lastDamage = damage;

            switch(GameManager.Instance.currentStage)
            {
                case 1:
                    lastDamage *= 1;
                    break;
                case 2:
                    lastDamage *= 1.5f;
                    break;
                case 3:
                    lastDamage *= 2;
                    break;
            }

            if (hp > lastDamage) hp -= lastDamage;
            else Die();
        }
    }

    protected override void Die()
    {
        GameManager.Instance.PlayerDied();
        Destroy(gameObject);
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

    public void BulletUpgrade()
    {
        if (bulletLevel < 3)
        {
            bulletDamage += 3;
            bulletLevel++;
        }
    }

    public void InvincibilityOn(float time)
    {
        if (time > invincibilityTime)
        {
            invincibilityTime = time;
        }
    }
    public void InvincibilityTimeDown()
    {
        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void HpUp(float AdditionalHp)
    {
        if (hp + AdditionalHp < 100) hp += AdditionalHp;
        else if (hp + AdditionalHp >= 100) hp = 100;
    }

    public void FuelUp()
    {
        fuel = 100;
    }
    private void FuelDown()
    {
        if (GameManager.Instance.monsterSpawnable) fuel -= Time.deltaTime * 2;
    }

    private void PlayerStatusUI()
    {
        int intHp = (int)hp;
        int intFuel = (int)fuel;

        PlayerHpSlider.value = hp / 100;
        PlayerHpText.text = intHp.ToString() + " / 100";

        PlayerFuelSlider.value = fuel / 100;
        PlayerFuelText.text = intFuel.ToString() + " / 100";
    }
}
