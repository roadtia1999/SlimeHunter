using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x - (Screen.width / 2);
        if (mouseX >= 0)
        {
            spriteRenderer.flipX = false;
            circleCollider.offset = new Vector2(-0.1f, 0);
        }
        else
        {
            spriteRenderer.flipX = true;
            circleCollider.offset = new Vector2(0.1f, 0);
        }
    }
}
