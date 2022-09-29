using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : Item, IInteraction
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
    List<IngredientTypes> currentIngredientsOnPlate = new List<IngredientTypes>();

    public void ItemPlace(Item ingredientToAdd)
    {
        //Can't combine plates
        if (ingredientToAdd.GetCurrentIngredientType() == IngredientTypes.Plates || currentIngredientsOnPlate.Contains(ingredientToAdd.GetCurrentIngredientType())) 
            return;

        if (ingredientToAdd.GetCurrentDoneness() != Doneness.Cooked)
            return;

        VerifyTacoRecipeExists(ingredientToAdd);
    }

    private void VerifyTacoRecipeExists(Item ingredientToAdd)
    {
        List<IngredientTypes> possibleTacoCombination = currentIngredientsOnPlate.Select(ingredient => new IngredientTypes()).ToList();
        possibleTacoCombination.Add(ingredientToAdd.GetCurrentIngredientType());

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
                    currentIngredientsOnPlate.Add(ingredientToAdd.GetCurrentIngredientType());
                    currentSpriteRend.sprite = combination.desiredPlateLook;

                    Debug.Log("Found Sprite!");

                    Picker.Instance.SetCurrentHeldItem(null, true);
                }
            }
        }
    }

    public void HoverInteraction(Item item)
    {
        Debug.Log("Over Plate!");

        if (item.GetCurrentIngredientType() == IngredientTypes.Plates || currentIngredientsOnPlate.Contains(item.GetCurrentIngredientType()))
            return;

        VerifyTacoRecipeExists(item);
    }
}

[System.Serializable]
public class TacoCombinations
{
    public List<IngredientTypes> recipeCombinations;
    public Sprite desiredPlateLook;
}