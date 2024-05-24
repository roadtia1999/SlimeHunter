using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.playerHP > 0 && TimeCount.timePassed < 600)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 backgroundMove = new Vector2(-h, -v);
            backgroundMove.Normalize();

            transform.Translate(backgroundMove * GameController.moveSpeed * Time.deltaTime * GameController.fixedSpeed);
        }
    }
}
