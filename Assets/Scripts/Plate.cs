using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
    List<IngredientTypes> ingredientsAdded = new List<IngredientTypes>();

    public void ItemPlace(Item ingredientToAdd)
    {
        if (ingredientToAdd.GetCurrentIngredientType() == IngredientTypes.Plates) return;

        if (ingredientsAdded.Contains(ingredientToAdd.GetCurrentIngredientType()))
            return;
        else if(ingredientToAdd.GetCurrentIngredientType() == IngredientTypes.Tortilla)
        {
            //Currently do not have a tortilla only plate
            ingredientToAdd.transform.SetParent(gameObject.transform);
            ingredientToAdd.ResetTransform();
            Picker.Instance.SetCurrentHeldItem(null);
        }
        else
            ingredientsAdded.Add(ingredientToAdd.GetCurrentIngredientType());
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