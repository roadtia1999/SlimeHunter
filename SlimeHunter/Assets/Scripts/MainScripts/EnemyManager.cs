using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float HP;
    public float Damage;
    private float healingTime;
    private float healingRoutine;
    public GameObject dropEXP;
    public GameObject treasure;
    public GameObject cheese;

    // Start is called before the first frame update
    void Start()
    {
        healingTime = 0;
        healingRoutine = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.endTime == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Projectile"))
        {
            HP -= GameController.hitDamage;
        }
        else if (collision.gameObject.name.Contains("SubWeapon"))
        {
            float dmg = collision.gameObject.GetComponent<SubWeaponDmg>().dmg;
            HP -= dmg;
        }

        if (HP <= 0)
        {
            EnemyDestroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !GameController.invAfterHit)
        {
            GameController.invAfterHit = true;
            if (Damage - GameController.defense > 1)
            {
                GameController.playerHP -= Damage - GameController.defense;
            }
            else
            {
                GameController.playerHP -= 1;
            }
        }
        else if (collision.tag == "EnemyHeal" && !gameObject.name.Contains("Healer"))
        {
            healingTime += Time.deltaTime;

            if (healingTime > healingRoutine)
            {
                HP += 50;
            }
        }
    }

    public void EnemyDestroyed()
    {
        if (treasure != null)
        {
            Instantiate(treasure, transform.position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            int rand = Random.Range(1, 100);
            if (rand <= 3)
            {
                Instantiate(cheese, transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        Instantiate(dropEXP, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
    }
}
