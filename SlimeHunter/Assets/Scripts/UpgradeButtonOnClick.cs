using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonOnClick : MonoBehaviour
{
    GameController gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void ButtonA()
    {
        gc.UpgradeButtonClick(GameController.currentButtonATag);
    }

    public void ButtonB()
    {
        gc.UpgradeButtonClick(GameController.currentButtonBTag);
    }
}
