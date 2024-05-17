using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public Animator anim;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.playerHP <= 0)
        {
            anim.SetTrigger("Dead");
            StartCoroutine(AnimPause());
        }
    }

    IEnumerator AnimPause()
    {
        yield return new WaitForSeconds(2f);
        anim.speed = 0f;
        EnemySpawner.gameOver = true;
        gameOverMenu.SetActive(true);
    }
}
