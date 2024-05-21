using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
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

    public void MakeSound()
    {
        aud.volume = GameController.volume;
        aud.loop = false;
        aud.Play();
    }
}
