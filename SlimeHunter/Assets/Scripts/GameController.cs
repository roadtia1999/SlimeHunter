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
    public static float defense;
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

    public GameObject levelUpMenu;
    private int mainWeaponLvl;
    private List<Subweapon> subweapons;
    public int upgradeMax;
    public int subweaponInventoryMax;

    public Text lvlUpChoiceTitle1;
    public Text lvlUpChoiceDescription1;
    public Image lvlUpChoiceImage1;
    public Text lvlUpChoiceTitle2;
    public Text lvlUpChoiceDescription2;
    public Image lvlUpChoiceImage2;
    private int currentButtonATag;
    private int currentButtonBTag;

    public List<ItemDatabase> itemDatabase;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        moveSpeed = 0.1f;
        fixedSpeed = 10.1f;
        hitDamage = 10f;
        defense = 0;
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

        mainWeaponLvl = 0;
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
        Time.timeScale = 0f;
        levelUpMenu.SetActive(true);
        UpgradePick();
    }

    void UpgradePick()
    {
        int a, b;
        bool notGoodToGo = true;
        do
        {
            a = Random.Range(0, itemDatabase.Count);
            b = Random.Range(0, itemDatabase.Count);
            notGoodToGo = PickChecker(a) || PickChecker(b);
        } while (a == b || notGoodToGo);

        lvlUpChoiceTitle1.text = itemDatabase[a].itemName;
        lvlUpChoiceDescription1.text = itemDatabase[a].levelUpDescription[UpgradeChecker(a)];
        lvlUpChoiceImage1.sprite = itemDatabase[a].sprite;
        currentButtonATag = a;

        lvlUpChoiceTitle2.text = itemDatabase[b].itemName;
        lvlUpChoiceDescription2.text = itemDatabase[b].levelUpDescription[UpgradeChecker(b)];
        lvlUpChoiceImage2.sprite = itemDatabase[b].sprite;
        currentButtonBTag = b;
    }

    private bool PickChecker(int n)
    {
        switch(itemDatabase[n].tag)
        {
            case "MainWeapon":
                if (mainWeaponLvl >= upgradeMax)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "SubWeapon":
                if (subweapons.Count - 1 >= subweaponInventoryMax)
                {
                    return true;
                }
                for (int i = 0; i < subweapons.Count; i++)
                {
                    if (n == subweapons[i].code && subweapons[i].upgrade >= upgradeMax)
                    {
                        return true;
                    }
                }
                return false;
            default:
                return false;
        }
    }

    private int UpgradeChecker(int n)
    {
        switch (itemDatabase[n].tag)
        {
            case "MainWeapon":
                return mainWeaponLvl;
            case "SubWeapon":
                for (int i = 0; i < subweapons.Count; i++)
                {
                    if (n == subweapons[i].code)
                    {
                        return subweapons[i].upgrade;
                    }
                }
                return 0;
            default:
                return 0;
        }
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

class Subweapon
{
    public int code;
    public int upgrade;
}
