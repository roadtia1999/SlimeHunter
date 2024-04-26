using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootPerSecond;
    private float idleTime;

    public int projUpgrade;
    public int projCount;

    public GameObject proj;

    // Start is called before the first frame update
    void Start()
    {
        idleTime = 0;
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
        yield return new WaitForSeconds(0);

        /*

        for (int i = 0; i < projCount; i++)
        {
            yield return new WaitForSeconds(0.2f);

            Vector2 mouse = Input.mousePosition - transform.position;
            mouse.Normalize();
            
            switch (projUpgrade)
            {
                case 1:
                    Instantiate(proj, transform.position, Quaternion.AngleAxis(0, mouse));
                    break;
                default:
                    break;
            }
        }

        */
    }
}
