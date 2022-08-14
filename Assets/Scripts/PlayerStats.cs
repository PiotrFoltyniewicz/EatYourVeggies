using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthPoints = 3;
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
    private List<GameObject> seedsAround = new List<GameObject>();
    private GameObject closestSeed;

    public Animator animator;
    void Awake()
    {
        healthSpriteRend = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        waterSpriteRend = GameObject.Find("WaterBottle").GetComponent<SpriteRenderer>();
        seedSpriteRend = GameObject.Find("HoldingSeed").GetComponent<SpriteRenderer>();
        weaponSpriteRend = GameObject.Find("CurrentWeapon").GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMovement>();
        waterSpriteRend.sprite = waterSprites[0];
    }
    private void Start()
    {
        ChooseWeapon("None");
    }

    void Update()
    {
        if (seedsAround.Count == 0)
        {
            foreach (GameObject seed in GameObject.FindGameObjectsWithTag("Seed"))
            {
                seed.GetComponent<Seed>().StopGlow();
            }

        }
        foreach (GameObject seed in seedsAround)
        {
            float distance = 999f;
            if (Vector2.Distance(transform.position, seed.transform.position) < distance)
            {
                distance = Vector2.Distance(transform.position, seed.transform.position);
                foreach (GameObject seed2 in GameObject.FindGameObjectsWithTag("Seed"))
                {
                    seed2.GetComponent<Seed>().StopGlow();
                }
                seed.GetComponent<Seed>().Glow();
                closestSeed = seed;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && currentSeed == "None" && seedsAround.Contains(closestSeed))
        {
            GiveSeed(closestSeed.GetComponent<Seed>().seedName);
            Destroy(closestSeed);
            closestSeed = null;
        }
    }

    public void TakeDamage()
    {
        healthPoints--;
        if (healthPoints == 0)
        {
            Debug.Log("Game Over");
            return;
        }
        healthSpriteRend.sprite = healthSprites[healthPoints - 1];
    }

    public void GiveWater()
    {
        waterPoints++;
        if (waterPoints > maxWaterPoints)
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

    public void GiveSeed(string seedName)
    {
        currentSeed = seedName.Remove(seedName.Length - 4);
        switch (seedName)
        {
            case "CarrotSeed":
                seedSpriteRend.sprite = seedSprites[0];
                break;
        }
    }

    public void GiveWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        ChooseWeapon(weaponName);
    }

    private void ChooseWeapon(string s)
    {
        Destroy(transform.GetChild(1).gameObject);
        switch (s)
        {
            case "None":
                {
                    weaponSpriteRend.sprite = weaponSprites[0];
                    GameObject temp = Instantiate(weapons[0]);
                    temp.transform.parent = transform;
                    playerMove.animator.SetInteger("Weapon", 0);
                }
                break;
            case "Carrot":
                {
                    weaponSpriteRend.sprite = weaponSprites[1];
                    GameObject temp = Instantiate(weapons[1]);
                    temp.transform.parent = transform;
                    playerMove.animator.SetInteger("Weapon", 1);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Seed")
        {
            seedsAround.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Seed")
        {
            seedsAround.Remove(collision.gameObject);
            
        }
    }

}
