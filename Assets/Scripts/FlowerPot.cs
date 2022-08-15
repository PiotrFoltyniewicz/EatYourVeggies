using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    public string plant = "None";
    public float plantGrowTime;
    public List<Sprite> plantSprites;
    private Sprite[] spritePair = new Sprite[2];
    private SpriteRenderer spriteRenderer;
    private bool playerNearby;
    private GameObject player;
    private int plantStage = -1;
    public Sprite[] healthbarSprites = new Sprite[6];
    private GameObject healthBar;
    private float healthPoint;
    private float healthPointTime;
    private int healthBarStage;
    public AudioSource flowerPot;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = plantSprites[0];
        player = GameObject.Find("Player");
        spritePair[0] = plantSprites[0];
        spritePair[1] = plantSprites[1];
        healthBar = GameObject.Find("PlantHealthBar");
        healthBar.SetActive(false);
    }

    private void Update()
    {
        healthPointTime -= Time.deltaTime;
        if(healthPointTime < 0 && plantStage > -1)
        {
            healthPointTime = healthPoint;
            ChangeHealthSprite(healthBarStage--);
        }
        plantGrowTime -= Time.deltaTime;
        if(plantGrowTime < 0 && plant != "None" && plantStage != 3)
        {
            plant = "None";
            spritePair = ChoosePlant(plant);
            if(playerNearby) spriteRenderer.sprite = spritePair[1];
            else spriteRenderer.sprite = spritePair[0];
        }
        if(Input.GetKeyDown(KeyCode.E))
            if(playerNearby)
                if(plant == "None")
                {
                    plant = player.GetComponent<PlayerStats>().GetSeed() + "0";
                    if(plant == "None0")
                        plant = "None";
                    else
                    {
                        flowerPot.Play();
                        spritePair = ChoosePlant(plant);
                        spriteRenderer.sprite = spritePair[1];
                    }
                }
                else if(plantStage == 3)
                {
                    flowerPot.Play();
                    player.GetComponent<PlayerStats>().GiveWeapon(plant.Substring(0, plant.Length - 1));
                    plant = "None";
                    spritePair = ChoosePlant(plant);
                    if (playerNearby) spriteRenderer.sprite = spritePair[1];
                    else spriteRenderer.sprite = spritePair[0];
                }
                else if(player.GetComponent<PlayerStats>().waterPoints == 13)
                {
                    flowerPot.Play();
                    player.GetComponent<PlayerStats>().GetWater();
                    WaterPlant();
                    if (playerNearby) spriteRenderer.sprite = spritePair[1];
                    else spriteRenderer.sprite = spritePair[0];
                }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                spriteRenderer.sprite = spritePair[1];
                playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spriteRenderer.sprite = spritePair[0];
            playerNearby = false;
        }
    }

    private Sprite[] ChoosePlant(string s)
    {
        Sprite[] sprites = new Sprite[2];
        switch(s)
        {
            case "None":
                healthBar.SetActive(false);
                sprites[0] = plantSprites[0];
                sprites[1] = plantSprites[1];
                plantStage = -1;
                break;
            case "Carrot0":
                healthBar.SetActive(true);
                sprites[0] = plantSprites[2];
                sprites[1] = plantSprites[3];
                plantStage = 0;
                plantGrowTime = 40;
                break;
            case "Carrot1":
                sprites[0] = plantSprites[4];
                sprites[1] = plantSprites[5];
                plantStage = 1;
                plantGrowTime = 40;
                break;
            case "Carrot2":
                sprites[0] = plantSprites[6];
                sprites[1] = plantSprites[7];
                plantStage = 2;
                plantGrowTime = 40;
                break;
            case "Carrot3":
                sprites[0] = plantSprites[8];
                sprites[1] = plantSprites[9];
                plantStage = 3;
                plantGrowTime = 120;
                break;
            case "Leek0":
                healthBar.SetActive(true);
                sprites[0] = plantSprites[10];
                sprites[1] = plantSprites[11];
                plantStage = 0;
                plantGrowTime = 30;
                break;
            case "Leek1":
                sprites[0] = plantSprites[12];
                sprites[1] = plantSprites[13];
                plantStage = 1;
                plantGrowTime = 30;
                break;
            case "Leek2":
                sprites[0] = plantSprites[14];
                sprites[1] = plantSprites[15];
                plantStage = 2;
                plantGrowTime = 30;
                break;
            case "Leek3":
                sprites[0] = plantSprites[16];
                sprites[1] = plantSprites[17];
                plantStage = 3;
                plantGrowTime = 120;
                break;
        }
        healthPoint = plantGrowTime / 6;
        healthBarStage = 5;
        ChangeHealthSprite(5);
        return sprites;
    }

    private void WaterPlant()
    {
        plant = plant.Remove(plant.Length - 1) + (plantStage + 1);
        spritePair = ChoosePlant(plant);
        healthBar.GetComponent<SpriteRenderer>().sprite = healthbarSprites[5];
    }

    private void ChangeHealthSprite(int sprite)
    {
        healthBar.GetComponent<SpriteRenderer>().sprite = healthbarSprites[sprite];
    }
}
