using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject target;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = target.transform.position - transform.position;
        move.Normalize();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 backgroundMove = new Vector2(-h, -v);
        backgroundMove.Normalize();

        if (move.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        transform.Translate(move * moveSpeed * Time.deltaTime);
        transform.Translate(backgroundMove * GameController.moveSpeed * Time.deltaTime * GameController.fixedSpeed);
    }
}
