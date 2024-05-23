using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon9Spawner : MonoBehaviour
{
    public GameObject mine;
    public float attackRate;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EnemySpawner.gameOver)
        {
            timeCount += Time.deltaTime;
            if (timeCount > attackRate)
            {
                GameObject obj = Instantiate(mine);
                SubWeaponDmg dmg = obj.GetComponent<SubWeaponDmg>();
                // dmg.dmg += dmgUpgrade;
                timeCount = 0;
            }
        }
    }
}
