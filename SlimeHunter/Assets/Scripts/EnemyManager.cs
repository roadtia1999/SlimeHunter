using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float HP;
    public float Damage;
    public GameObject dropEXP;

    // Start is called before the first frame update
    void Start()
    {
        
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
            if (HP <= 0)
            {
                Instantiate(dropEXP, transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameController.invAfterHit)
        {
            GameController.invAfterHit = true;
            GameController.playerHP -= Damage;
        }
    }
}
