using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : Item
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
    List<IngredientTypes> currentIngredientsOnPlate = new List<IngredientTypes>();

    public void ItemPlace(Item ingredientToAdd)
    {
        //Can't combine plates
        if (ingredientToAdd.GetCurrentIngredientType() == IngredientTypes.Plates) return;

        if (currentIngredientsOnPlate.Contains(ingredientToAdd.GetCurrentIngredientType()))
            return;

        //Custom use case do not have plate with Tortilla. TODO: Change later
        if (ingredientToAdd.GetCurrentIngredientType() == IngredientTypes.Tortilla)
        {
            //Currently do not have a tortilla only plate
            currentIngredientsOnPlate.Add(ingredientToAdd.GetCurrentIngredientType());
            ingredientToAdd.transform.SetParent(gameObject.transform);
            ingredientToAdd.ResetTransform();
            Picker.Instance.SetCurrentHeldItem(null);
            return;
        }

        VerifyTacoRecipeExists(ingredientToAdd);
    }

    private void VerifyTacoRecipeExists(Item ingredientToAdd)
    {
        List<IngredientTypes> possibleTacoCombination = currentIngredientsOnPlate.Select(ingredient => new IngredientTypes()).ToList();
        possibleTacoCombination.Add(ingredientToAdd.GetCurrentIngredientType());

        foreach (var ingred in possibleTacoCombination)
            Debug.Log("Ingred: " + ingred);

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
                    Debug.Log($"Possible Combination: " + equal);
                    Debug.Log($"Count: " + possibleTacoCombination.Count + " " + recipe.Count);

                    currentIngredientsOnPlate.Add(ingredientToAdd.GetCurrentIngredientType());
                    currentSpriteRend.sprite = combination.desiredPlateLook;

                    Picker.Instance.SetCurrentHeldItem(null, true);

                    //Delete when we have plate artwork
                    if (transform.GetChild(0))
                        transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }

    void UpdatePlate()
    {
        //var listOne = new List<int> { 1, 2, 3, 4, 5 };
        //var listTwo = new List<int> { 1, 2, 3, 4, 5, 7 };

        //var equal = (listOne.Count == listTwo.Count);

        //if (equal)
        //{
        //    listOne.Sort();
        //    listTwo.Sort();
        //    equal = listOne.SequenceEqual(listTwo)
        //}

    }
}

[System.Serializable]
public class TacoCombinations
{
    public List<IngredientTypes> recipeCombinations;
    public Sprite desiredPlateLook;
}