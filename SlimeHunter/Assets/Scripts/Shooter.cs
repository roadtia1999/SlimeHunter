using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootPerSecond;
    private float idleTime;
    public static bool playerAction;

    public int projUpgrade;
    public int projCount;

    public GameObject proj;

    // Start is called before the first frame update
    void Start()
    {
        idleTime = 0;
        playerAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        idleTime += Time.deltaTime;
        if (idleTime >= shootPerSecond)
        {
            StartCoroutine(Shoot());
            idleTime = 0;
        }
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < projCount; i++)
        {
            yield return new WaitForSeconds(0.2f);

            switch (projUpgrade)
            {
                case 1:
                    Instantiate(proj, transform.position, Quaternion.Euler(0, 0, 0));
                    break;
                default:
                    break;
            }
        }
    }
}
