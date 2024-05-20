using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float shootPerSecond;
    private float idleTime;
    public float ultTime;
    public static bool playerAction;

    public int projUpgrade;
    public int projCount;
    private bool ulting;

    public GameObject proj;
    public GameObject ultEffect;
    public AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        idleTime = 0;
        playerAction = false;
        ulting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.playerHP > 0 && TimeCount.timePassed < 600)
        {
            if (GameController.ultTime == true)
            {
                idleTime = 0;
                if (!ulting)
                {
                    ulting = true;
                    StartCoroutine(UltShoot());
                }
            }
            else
            {
                idleTime += Time.deltaTime;
                if (idleTime >= shootPerSecond)
                {
                    StartCoroutine(NormalShoot());
                    idleTime = 0;
                }
            }
        }
    }

    void Shoot()
    {
        switch (projUpgrade)
        {
            case 1:
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, -5));
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, 5));
                break;
            case 3:
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, -10));
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(proj, transform.position, Quaternion.Euler(0, 0, 10));
                break;
            default:
                break;
        }

        shootSound.volume = GameController.volume;
        shootSound.loop = false;
        shootSound.Play();
    }

    IEnumerator NormalShoot()
    {
        for (int i = 0; i < projCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Shoot();
        }
    }

    IEnumerator UltShoot()
    {
        ultEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UltTimeCounter());

        while(GameController.ultTime)
        {
            yield return new WaitForSeconds(0.1f);
            Shoot();
        }
    }

    IEnumerator UltTimeCounter()
    {
        yield return new WaitForSeconds(ultTime);
        GameController.ultTime = false;
        ulting = false;
    }
}
