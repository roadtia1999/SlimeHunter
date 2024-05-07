using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject background;
    public Renderer bgMaterial;
    public Image hitEffect;

    private float h = 0;
    private float v = 0;
    public Animator animator;
    
    public static float moveSpeed;
    public static float fixedSpeed;
    public static float hitDamage;
    public static float playerHP;
    public float playerMaxHP;
    public static bool invAfterHit;
    private bool invCoroutine;

    public static int currentEXP;
    public static int currentMaxEXP;
    public int levelOneMaxEXP;
    public static int currentLvl;

    public Slider HPBar;
    public Slider actionBar;
    public Slider EXPBar;
    private float actionReady;
    public float actionRate;
    public float invincibleTime;
    public static bool ultTime;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        moveSpeed = 0.1f;
        fixedSpeed = 10.1f;
        hitDamage = 10f;
        playerHP = playerMaxHP;
        invAfterHit = false;
        invCoroutine = false;
        ultTime = false;

        currentEXP = 0;
        currentMaxEXP = levelOneMaxEXP;
        currentLvl = 1;

        HPBar.value = playerHP / playerMaxHP;
        actionBar.value = 0;
        actionReady = 0;
    }

    // Update is called once per frame
    void Update()
    {
        calcBar();

        if (playerHP > 0)
        {
            if (TimeCount.timePassed < 600)
            {
                bgMove();
            }

            if (invAfterHit && !invCoroutine)
            {
                invCoroutine = true;
                StartCoroutine(InvincibleTimeAfterHit());
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (actionBar.value >= 1)
                {
                    actionReady = 0;
                    ultTime = true;
                }
            }

            if (currentEXP >= currentMaxEXP)
            {
                lvlUp();
            }
        }
    }

    void bgMove()
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
            bgMaterial.material.mainTextureOffset += backgroundMove * moveSpeed * Time.deltaTime;
        }
    }

    void calcBar()
    {
        if (actionBar.value < 1 && playerHP > 0)
        {
            actionReady += Time.deltaTime;
        }
        
        if (actionReady / actionRate >= 1)
        {
            actionBar.value = 1;
        }
        else
        {
            actionBar.value = actionReady / actionRate;
        }

        if (playerHP / playerMaxHP < 0)
        {
            HPBar.value = 0;
        }
        else
        {
            HPBar.value = playerHP / playerMaxHP;
        }

        if (currentEXP / currentMaxEXP >= 1)
        {
            EXPBar.value = 1;
        }
        else
        {
            EXPBar.value = (float)currentEXP / (float)currentMaxEXP;
        }
    }

    IEnumerator InvincibleTimeAfterHit()
    {
        SpriteRenderer playerSprite = player.gameObject.GetComponent<SpriteRenderer>();
        Color playerColor = playerSprite.color;
        playerColor.a = 0.6f;
        playerSprite.color = playerColor;
        hitEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(invincibleTime);

        playerColor.a = 1f;
        playerSprite.color = playerColor;
        invCoroutine = false;
        invAfterHit = false;
        hitEffect.gameObject.SetActive(false);
    }

    void lvlUp()
    {
        currentEXP = 0;
        currentMaxEXP += 2;
        currentLvl++;
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
