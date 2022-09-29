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

    //Use to shift cooking sprites and doneness
    bool isInHoverArea;
    bool canBePickedUp;

    //Used to vacant open position when pickedup
    AreaPosition currentSlotTaken;

    //Will be used for Final Cooking Product and Ingredient Cooked
    protected SpriteRenderer currentSpriteRend;

    protected Equipment currentHoveredEquipment;

    public void SetCurrentSlotTaken(AreaPosition slot) => currentSlotTaken = slot;
    public void SetCanBePickedUp(bool cachedCanBePickedUp) => canBePickedUp = cachedCanBePickedUp;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }
    public virtual void Update()
    {
        if (isInHoverArea && Input.GetMouseButtonDown(0) && this == Picker.Instance.GetCurrentHeldItem())
        {
            Debug.Log("In hover area! Current Held Item: " + Picker.Instance.GetCurrentHeldItem());
            HoverInteraction(currentHoveredEquipment);
        }
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

    //Issue with Collisions being to close. Ex Leaving the Tortialla Plancha going into the Meat Side would trigger the Exit and not reset
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("In area start! Name: " + collision.gameObject.name);
        isInHoverArea = true;
        currentHoveredEquipment = collision.GetComponent<Equipment>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In area enter! Name: " + collision.gameObject.name);
        isInHoverArea = true;
        currentHoveredEquipment = collision.GetComponent<Equipment>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("In area exit! Name: " + collision.gameObject.name);
        isInHoverArea = false;
        currentHoveredEquipment = null;
    }

    public virtual void HoverInteraction(Equipment equipmentHovered)
    {
        Debug.Log("Implemented by Child");
    }

    public virtual void PickupInteraction()
    {
        Debug.Log("Implemented by Child");
    }
}