using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Doneness { Raw = 0, Cooked = 1, Burnt = 2}
public enum IngredientTypes { Tortilla, AlPastor, Onions, Cilantro, Shrimp, CarneAsada, SalsaRojo, SalsaVerde, Plates}

public class Item : MonoBehaviour
{
    [SerializeField] IngredientTypes currentType;
    [SerializeField] Item clickedIngredient;
    [SerializeField] Sprite[] cookingStageSprites;

    //Could randomize to make it a little tricky
    [SerializeField] float timebeforeNextCookedStage = 5f;

    //Used to vacant open position when pickedup
    AreaPosition currentSlotTaken;

    //Use to shift cooking sprites and doneness
    bool isCooking, isInHoverArea;
    float timeElapsed = 0;
    Doneness currentDoneness = Doneness.Raw;

    //Will be used for Final Cooking Product and Ingredient Cooked
    protected SpriteRenderer currentSpriteRend;

    IInteraction currentObjectInteraction;

    public void SetCurrentSlotTaken(AreaPosition slot) => currentSlotTaken = slot;
    public void SetIsCooking(bool isCookingCached) => isCooking = isCookingCached;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }

    public IngredientTypes GetCurrentIngredientType()
    {
        return currentType;
    }

    private void Update()
    {
        if(isInHoverArea && Input.GetMouseButtonDown(0) && this == Picker.Instance.GetCurrentHeldItem())
        {
            Debug.Log("In hover area!");
            currentObjectInteraction?.HoverInteraction(this);
        }

        if(isCooking && (int)currentDoneness < cookingStageSprites.Length -1)
        {
            timeElapsed += Time.deltaTime;
  
            if (timeElapsed > timebeforeNextCookedStage)
            {
                currentDoneness++;
                currentSpriteRend.sprite = cookingStageSprites[(int)currentDoneness];
                timeElapsed = 0;
            }
        }
    }

    //Issue with Collisions being to close. Ex Leaving the Tortialla Plancha going into the Meat Side would trigger the Exit and not reset
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("In area! Name: " + collision.gameObject.name);
        isInHoverArea = true;
        currentObjectInteraction = collision.GetComponent<IInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In area!");
        isInHoverArea = true;
        currentObjectInteraction = collision.GetComponent<IInteraction>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInHoverArea = false;
        currentObjectInteraction = null;
    }

    public void Pickup(bool isIngredient)
    {
        isCooking = false;
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if(isIngredient)
            currentSlotTaken.isPositionTaken = false;
    }

    public void ResetTransform()
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public Doneness GetCurrentDoneness()
    {
        return currentDoneness;
    }
}