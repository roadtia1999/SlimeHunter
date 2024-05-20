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
    public static float projSize;
    public float playerMaxHP;
    public static bool invAfterHit;
    private bool invCoroutine;

    public static int currentEXP;
    public static int currentMaxEXP;
    public int levelOneMaxEXP;
    public static int currentLvl;

    public Slider HPBar;
    public Text HPText;
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
    public static int currentButtonATag;
    public static int currentButtonBTag;

    public Text treasureTitle;
    public Text treasureDescription;
    public Image treasureImage;
    public static int currentTreasureTag;
    public GameObject treasureUI;

    public List<ItemDatabase> itemDatabase;

    public static float volume;
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        moveSpeed = 0.1f;
        fixedSpeed = 10.1f;
        hitDamage = 10f;
        projSize = 1f;
        defense = 0;
        playerHP = playerMaxHP;
        invAfterHit = false;
        invCoroutine = false;
        ultTime = false;

        currentEXP = 0;
        currentMaxEXP = levelOneMaxEXP;
        currentLvl = 1;

        HPBar.value = playerHP / playerMaxHP;
        HPText.text = string.Format("{0} / {1}", playerHP, playerMaxHP);
        actionBar.value = 0;
        actionReady = 0;

        mainWeaponLvl = 0;

        volume = PlayerPrefs.GetFloat("volume");
        bgm.volume = volume;
        bgm.loop = true;
        bgm.Play();
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
        HPText.text = string.Format("{0} / {1}", playerHP, playerMaxHP);

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
        player.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(invincibleTime);

        playerColor.a = 1f;
        playerSprite.color = playerColor;
        invCoroutine = false;
        invAfterHit = false;
        hitEffect.gameObject.SetActive(false);
        player.GetComponent<Collider2D>().enabled = true;
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

        ItemButton(lvlUpChoiceTitle1, lvlUpChoiceDescription1, lvlUpChoiceImage1, ref currentButtonATag, a);
        ItemButton(lvlUpChoiceTitle2, lvlUpChoiceDescription2, lvlUpChoiceImage2, ref currentButtonBTag, b);
    }

    public void TreasureFound()
    {
        Time.timeScale = 0f;

        if (!PickChecker(0))
        {
            ItemButton(treasureTitle, treasureDescription, treasureImage, ref currentTreasureTag, 0);
            treasureUI.SetActive(true);
            return;
        }
        else
        {
            for (int i = 0; i < subweapons.Count; i++)
            {
                if (subweapons[i].upgrade < upgradeMax)
                {
                    ItemButton(treasureTitle, treasureDescription, treasureImage, ref currentTreasureTag, subweapons[i].code);
                    treasureUI.SetActive(true);
                    return;
                }
            }
            do
            {
                int i = Random.Range(1, itemDatabase.Count);
                if (itemDatabase[i].tag == "Instant" || itemDatabase[i].tag == "Passive")
                {
                    ItemButton(treasureTitle, treasureDescription, treasureImage, ref currentTreasureTag, i);
                    treasureUI.SetActive(true);
                    return;
                }
            } while(true);
        }
    }

    public void ItemButton(Text title, Text description, Image sprite, ref int tag, int value)
    {
        title.text = itemDatabase[value].itemName;
        description.text = itemDatabase[value].levelUpDescription[UpgradeChecker(value)];
        sprite.sprite = itemDatabase[value].sprite;
        tag = value;
    }

    public void UpgradeButtonClick(int tag)
    {
        switch (itemDatabase[tag].enhancementTag[UpgradeChecker(tag)])
        {
            case 0: // InstaKill
                InstaKill();
                break;
            case 1:
                UpgradeDamage(tag);
                break;
            case 2:
                UpgradeProjectile(tag);
                break;
            case 3:
                UpgradeSize(tag);
                break;
            case 4:
                HealUp();
                break;
            case 5:
                Vaccum();
                break;
            case 6:
                UpgradeMaxHP(tag);
                break;
            case 7:
                UpgradeSpeed(tag);
                break;
            case 8:
                UpgradeDefense(tag);
                break;
            default:
                break;
        }

        switch (itemDatabase[tag].tag)
        {
            case "MainWeapon":
                mainWeaponLvl += 1;
                break;
            case "SubWeapon":
                for (int i = 0; i < subweapons.Count; i++)
                {
                    if (tag == subweapons[i].code)
                    {
                        subweapons[i].upgrade++;
                        break;
                    }
                }
                break;
            default:
                break;
        }

        levelUpMenu.SetActive(false);
        treasureUI.SetActive(false);
        Time.timeScale = 1f;
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
                for (int i = 0; i < subweapons.Count; i++)
                {
                    if (n == subweapons[i].code && subweapons[i].upgrade >= upgradeMax)
                    {
                        return true;
                    }
                }
                if (subweapons.Count >= subweaponInventoryMax)
                {
                    return true;
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

    private void InstaKill()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemy)
        {
            EnemyManager em = obj.GetComponent<EnemyManager>();
            if (em.treasure == null)
            {
                em.EnemyDestroyed();
            }
        }
    }

    private void UpgradeDamage(int tag)
    {
        if (itemDatabase[tag].tag == "SubWeapon")
        {

        }
        else
        {
            hitDamage += itemDatabase[tag].enhancementValue;
        }
    }

    private void UpgradeProjectile(int tag)
    {
        if (itemDatabase[tag].tag == "SubWeapon")
        {

        }
        else
        {
            Shooter shooter = player.GetComponent<Shooter>();
            shooter.projUpgrade += 1;
        }
    }

    private void UpgradeSize(int tag)
    {
        if (itemDatabase[tag].tag == "SubWeapon")
        {

        }
        else
        {
            projSize += 0.1f;
        }
    }

    private void HealUp()
    {
        playerHP = playerMaxHP;
    }

    private void Vaccum()
    {
        GameObject[] exp = GameObject.FindGameObjectsWithTag("Exp");
        foreach (GameObject obj in exp)
        {
            ExpScript exs = obj.GetComponent<ExpScript>();
            exs.Vaccumed();
        }
    }

    private void UpgradeMaxHP(int tag)
    {
        playerMaxHP += itemDatabase[tag].enhancementValue;
    }

    private void UpgradeSpeed(int tag)
    {
        moveSpeed += itemDatabase[tag].enhancementValue;
    }

    private void UpgradeDefense(int tag)
    {
        defense += itemDatabase[tag].enhancementValue;
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}

class Subweapon
{
    public int code;
    public int upgrade;
}
