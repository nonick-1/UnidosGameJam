using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : DropOffArea
{
    public override bool IsAbleToPlaceItem(Item currentHeldIngredient)
    {
        Debug.Log("Item: " + currentHeldIngredient.name);

        Picker.Instance.SetCurrentHeldItem(null, true); //Refactor
        return true;
    }
}