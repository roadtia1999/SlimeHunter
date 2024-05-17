using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    private float volume;
    public AudioSource bgm;
    public AudioSource buttonSound;
    public AudioSource confirmSound;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSound()
    {

    }

    public void ConfirmSound()
    {

    }
}
