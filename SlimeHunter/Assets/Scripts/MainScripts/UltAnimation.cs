using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltAnimation : MonoBehaviour
{
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartSoundPlay()
    {
        aud.volume = GameController.volume;
        aud.loop = false;
        aud.Play();
    }

    private void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
