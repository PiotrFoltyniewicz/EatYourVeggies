using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int healthPoints = 3;
    private int maxHealthPoints = 3;
    public int waterPoints = 0;
    private int maxWaterPoints = 13;

    public Sprite[] healthSprites = new Sprite[3];
    private SpriteRenderer healthSpriteRend;
    public Sprite[] waterSprites = new Sprite[14];
    private SpriteRenderer waterSpriteRend;
    public List<Sprite> seedSprites = new List<Sprite>(); 
    private SpriteRenderer seedSpriteRend;
    public List<Sprite> weaponSprites = new List<Sprite>();
    public List<GameObject> weapons = new List<GameObject>();
    private SpriteRenderer weaponSpriteRend;
    public Sprite transparentSprite;
    public string currentSeed;
    public string currentWeapon;
    private PlayerMovement playerMove;

    public Animator animator;
    void Awake()
    {
        healthSpriteRend = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        waterSpriteRend = GameObject.Find("WaterBottle").GetComponent<SpriteRenderer>();
        seedSpriteRend = GameObject.Find("HoldingSeed").GetComponent<SpriteRenderer>();
        weaponSpriteRend = GameObject.Find("CurrentWeapon").GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMovement>();
    }

    void Update()
    {
    }

    public void TakeDamage()
    {
        healthPoints--;
        if(healthPoints == 0)
        {
            Debug.Log("Game Over");
            return;
        }
        healthSpriteRend.sprite = healthSprites[healthPoints - 1];
    }

    public void GiveWater()
    {
        waterPoints++;
        if(waterPoints > maxWaterPoints)
        {
            waterPoints = maxWaterPoints;
        }
        waterSpriteRend.sprite = waterSprites[waterPoints];
    }

    public void GetWater()
    {
        if (waterPoints < maxWaterPoints) return;
        waterPoints = 0;
        waterSpriteRend.sprite = waterSprites[0];
    }

    public string GetSeed()
    {
        string temp = currentSeed;
        seedSpriteRend.sprite = transparentSprite;
        currentSeed = "None";
        return temp;
    }

    public void GiveWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        ChooseWeapon(weaponName);
    }


    private void ChooseSeed()
    {

    }

    private void ChooseWeapon(string s)
    {
        switch(s)
        {
            case "Carrot":
                weaponSpriteRend.sprite = weaponSprites[0];
                GameObject temp = Instantiate(weapons[0]);
                temp.transform.parent = transform;
                playerMove.animator.SetInteger("Weapon", 1);
                break;
        }
    }

}