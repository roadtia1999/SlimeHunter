using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMouseOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Click();
    }

    private void OnMouseUp()
    {
        Click();
    }

    private void Click()
    {
        SoundMaker sm = GameObject.Find("SoundMaker").GetComponent<SoundMaker>();
        sm.ButtonSound();
    }
}
