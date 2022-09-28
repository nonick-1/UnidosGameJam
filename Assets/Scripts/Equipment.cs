using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] List<IngredientTypes> ingredientAllowed;
    [SerializeField] List<AreaPosition> itemPositions;

    [SerializeField] Sprite mouseOverCookingIcon;

    public CursorMode cursorMode = CursorMode.Auto;
    public Texture2D cursorTexture;
    public Texture2D defaultCursorTexture;
    public Vector2 hotSpot = Vector2.zero;


    //private void SetCursor()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //private void ResetCursor()
    //{
    //    Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
    //}    

    public void IsAbleToPlaceItem(Item currentHeldIngredient)
    {
        foreach(var areaPosition in itemPositions)
        {
            if(!areaPosition.isPositionTaken && ingredientAllowed.Contains(currentHeldIngredient.GetCurrentIngredientType()))
            {
                areaPosition.isPositionTaken = true;
                currentHeldIngredient.SetIsCooking(true);
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

    public void PlacePlate(Plate currentHeldPlate)
    {
        if (currentHeldPlate == null) return;

        foreach (var areaPosition in itemPositions)
        {
            if (!areaPosition.isPositionTaken && ingredientAllowed.Contains(currentHeldPlate.GetCurrentType()))
            {
                areaPosition.isPositionTaken = true;
                currentHeldPlate.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldPlate.transform.SetParent(areaPosition.position.transform);
                currentHeldPlate = null;
                //Picker.Instance.SetCurrentHeldPlate(null); //Refactor
                Debug.Log("Placed!");
                return;
            }
        }

        Debug.Log("Not Placed!");
    }

    public List<IngredientTypes> GetIngredientsAllowed()
    {
        return ingredientAllowed;
    }
}

[System.Serializable]
public class AreaPosition
{
    public GameObject position;
    public bool isPositionTaken;
}