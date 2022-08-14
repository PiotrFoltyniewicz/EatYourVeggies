using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityBar : MonoBehaviour
{
    public List<Sprite> durabilitySprites = new List<Sprite>();
    public int durability;
    public int durabilityPoint;
    public int durabilityPointLeft;
    public int currentSprite;

    public void ChangeSprite()
    {
        durabilityPointLeft--;
        if(durabilityPointLeft < 0)
        {
            GetComponent<SpriteRenderer>().sprite = durabilitySprites[--currentSprite];
            durabilityPointLeft = durabilityPoint;
        }
    }
}
