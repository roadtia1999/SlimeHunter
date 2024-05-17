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
        if (GameController.playerHP > 0)
        {
            Vector3 move = target.transform.position - transform.position;
            move.Normalize();

            if (move.x >= 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.Translate(move * moveSpeed * Time.deltaTime);
        }
    }
}
