using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameOverButton : MonoBehaviour
{
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
}
