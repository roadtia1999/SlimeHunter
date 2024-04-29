using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject background;
    public Renderer bgMaterial;

    private float h = 0;
    private float v = 0;
    public Animator animator;
    
    public static float moveSpeed;
    public static float fixedSpeed;
    public static float hitDamage;
    public static float playerHP;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        moveSpeed = 0.1f;
        fixedSpeed = 14f;
        hitDamage = 10f;
        playerHP = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 backgroundMove = new Vector2(-h, -v);
        backgroundMove.Normalize();

        if (h == 0 && v == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
            bgMove(backgroundMove);
        }
    }

    void bgMove(Vector2 moved)
    {
        bgMaterial.material.mainTextureOffset += moved * moveSpeed * Time.deltaTime;
    }
}
