using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffArea : MonoBehaviour
{
    [SerializeField] protected List<ItemType> ingredientAllowed;
    [SerializeField] protected List<AreaPosition> itemPositions;

    private void OnEnable()
    {
        UIManager.onStart += CleanArea;
    }

    private void OnDisable()
    {
        UIManager.onStart -= CleanArea;
    }

    public void CleanArea()
    {
        foreach(var position in itemPositions)
        {
            if(position.isPositionTaken)
            {
                Destroy(position.position.transform.GetChild(0).gameObject);
                position.isPositionTaken = false;
            }
        }
    }

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