using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : Item
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
    List<ItemType> currentIngredientsOnPlate = new List<ItemType>();

    public void VerifyTacoRecipeExists(Item ingredientToAdd)
    {
        Debug.Log("Verifyinig!");

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

                    Debug.Log("Found Sprite!");

                    Picker.Instance.SetCurrentHeldItem(null, true);
                }
            }
        }
    }

    public override void HoverInteraction()
    {
        currentHoveredEquipment.IsAbleToPlaceItem(this);
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