using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Doneness { Raw = 0, Cooked = 1, Burnt = 2}
public enum ItemType { Tortilla, AlPastor, Onions, Cilantro, Shrimp, CarneAsada, SalsaRojo, SalsaVerde, Plates}

public class Item : MonoBehaviour, IInteraction
{
    [SerializeField] ItemType currentType;

    bool isInHoverArea;
    bool canBePickedUp;

    //Used to vacant open position when pickedup
    AreaPosition currentSlotTaken;

    //Will be used for Final Cooking Product and Ingredient Cooked
    protected SpriteRenderer currentSpriteRend;

    protected DropOffArea currentHoveredEquipment;

    public void SetCurrentSlotTaken(AreaPosition slot) => currentSlotTaken = slot;
    public void SetCanBePickedUp(bool cachedCanBePickedUp) => canBePickedUp = cachedCanBePickedUp;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }

    public bool GetCanBePickedUp()
    {
        return canBePickedUp;
    }

    public ItemType GetCurrentItemType()
    {
        return currentType;
    }

    protected AreaPosition GetCurrentSlotTaken() 
    {
        return currentSlotTaken;
    } 
    
    //Called in picker
    public void ItemHoverInteraction()
    {
        if (isInHoverArea)
            HoverInteraction();
    }

    //Issue with Collisions being to close. Ex Leaving the Tortialla Plancha going into the Meat Side would trigger the Exit and not reset
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        isInHoverArea = true;
        currentHoveredEquipment = collision.GetComponent<DropOffArea>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In area enter! Name: " + collision.gameObject.name);
        isInHoverArea = true;
        currentHoveredEquipment = collision.GetComponent<DropOffArea>();
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("In area exit! Name: " + collision.gameObject.name);
        isInHoverArea = false;
        currentHoveredEquipment = null;
    }

    public virtual void HoverInteraction()
    {
        Debug.Log("Implemented by Child");
    }

    public virtual void PickupInteraction()
    {
        Debug.Log("Implemented by Child");
    }
}