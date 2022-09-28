using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTable : Equipment
{
    public override void IsAbleToPlaceItem(Item currentHeldIngredient)
    {
        foreach (var areaPosition in itemPositions)
        {
            if (!areaPosition.isPositionTaken && ingredientAllowed.Contains(currentHeldIngredient.GetCurrentIngredientType()))
            {
                areaPosition.isPositionTaken = true;
                currentHeldIngredient.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldIngredient.transform.SetParent(areaPosition.position.transform);
                currentHeldIngredient.SetCurrentSlotTaken(areaPosition);
                currentHeldIngredient = null;
                Picker.Instance.SetCurrentHeldItem(null); //Refactor
                Debug.Log("Placed!");
                return;
            }
        }
        return;
    }
}
