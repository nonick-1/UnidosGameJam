using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : DropOffArea
{
    public override bool IsAbleToPlaceItem(Item currentHeldIngredient)
    {
        Picker.Instance.SetCurrentHeldItem(null, true); //Refactor
        return true;
    }
}