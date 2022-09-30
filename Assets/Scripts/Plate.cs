using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : Item
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
    List<ItemType> currentIngredientsOnPlate = new List<ItemType>();

    Character hoveredCharacter;

    public void VerifyTacoRecipeExists(Item ingredientToAdd)
    {
        List<ItemType> possibleTacoCombination = currentIngredientsOnPlate.Select(ingredient => new ItemType()).ToList();
        possibleTacoCombination.Add(ingredientToAdd.GetCurrentItemType());

        foreach(var combination in allTacoCombinations)
        {
            var recipe = combination.recipeCombinations;

            var equal = (possibleTacoCombination.Count == recipe.Count);

            if (equal)
            {
                possibleTacoCombination.Sort();
                recipe.Sort();
                equal = possibleTacoCombination.SequenceEqual(recipe);

                //Reusing Equal to verify contents
                if(equal)
                {
                    currentIngredientsOnPlate.Add(ingredientToAdd.GetCurrentItemType());
                    currentSpriteRend.sprite = combination.desiredPlateLook;

                    Picker.Instance.SetCurrentHeldItem(null, true);
                }
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        hoveredCharacter = collision.GetComponent<Character>();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        hoveredCharacter = null;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        hoveredCharacter = collision.GetComponent<Character>();
    }

    public override void HoverInteraction()
    {
        if (currentHoveredEquipment)
            currentHoveredEquipment.IsAbleToPlaceItem(this);
        else if (hoveredCharacter)
            hoveredCharacter.TurnInFood(this);

    }

    public List<ItemType> GetCurrentIngredientsOnPlate()
    {
        return currentIngredientsOnPlate;
    }

    public override void PickupInteraction()
    {
        Debug.Log("Plate Pickedup");

        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if (GetCurrentSlotTaken() != null)
            GetCurrentSlotTaken().isPositionTaken = false;
    }
}

[System.Serializable]
public class TacoCombinations
{
    public List<ItemType> recipeCombinations;
    public Sprite desiredPlateLook;
}