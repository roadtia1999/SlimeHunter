using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public static float volume;
    public AudioSource bgm;
    public AudioSource buttonSound;
    public AudioSource confirmSound;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("volume");
        bgm.volume = volume;
        bgm.loop = true;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        volume = PlayerPrefs.GetFloat("volume");
        bgm.volume = volume;
        buttonSound.volume = volume;
        confirmSound.volume = volume;
    }

    public void ButtonSound()
    {

    }

    public void ConfirmSound()
    {

    }
}
