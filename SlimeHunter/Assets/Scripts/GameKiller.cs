using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeCount.timePassed > 602)
        {
            GameController.playerHP = 0;
        }
    }
}
