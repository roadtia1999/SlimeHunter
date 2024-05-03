using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    private Text text;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.playerHP > 0)
        {
            timePassed += Time.deltaTime;
            text.text = string.Format("{0:D2}:{1:D2}", (int)timePassed / 60, (int)timePassed % 60);
        }
    }
}
