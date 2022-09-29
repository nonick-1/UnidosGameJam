using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] protected List<ItemType> ingredientAllowed;
    [SerializeField] protected List<AreaPosition> itemPositions;

    //Sets Vacant Position for Item
    public virtual bool IsAbleToPlaceItem(Item currentHeldIngredient)
    {
        foreach(var areaPosition in itemPositions)
        {
            if(!areaPosition.isPositionTaken && ingredientAllowed.Contains(currentHeldIngredient.GetCurrentItemType()))
            {
                areaPosition.isPositionTaken = true;
                currentHeldIngredient.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldIngredient.transform.SetParent(areaPosition.position.transform);
                currentHeldIngredient.SetCurrentSlotTaken(areaPosition);
                currentHeldIngredient = null;
                Picker.Instance.SetCurrentHeldItem(null); //Refactor
                Debug.Log("Placed!");
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class AreaPosition
{
    public GameObject position;
    public bool isPositionTaken;
}