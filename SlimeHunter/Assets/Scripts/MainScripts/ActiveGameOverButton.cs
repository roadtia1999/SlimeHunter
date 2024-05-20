using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameOverButton : MonoBehaviour
{
    public AudioSource gameOverSound;
    public GameObject restartButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnimEnd()
    {
        restartButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void GameOverSound()
    {
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.bgm.Stop();

        gameOverSound.volume = GameController.volume;
        gameOverSound.loop = false;
        gameOverSound.Play();
    }
}
