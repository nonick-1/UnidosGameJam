using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] int currentOfFoodItemsHeld;
    [SerializeField] int amountOfFoodItemsCanHold;

    [SerializeField] List<Ingredient> ingredientsAllowed;
    [SerializeField] List<AreaPosition> ingredientPositions;

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

    private GameObject GetFreeKitchenArea()
    {
        foreach(AreaPosition areaPosition in ingredientPositions)
        {
            if (!areaPosition.isPositionTaken)
            {
                areaPosition.isPositionTaken = true;
                return areaPosition.position;
            }
        }

        return null;
    }

    public void IsAbleToPlaceItem(Ingredient currentHeldIngredient)
    {
        foreach(var areaPosition in ingredientPositions)
        {
            if(!areaPosition.isPositionTaken && !ingredientsAllowed.Contains(currentHeldIngredient))
            {
                areaPosition.isPositionTaken = true;
                currentHeldIngredient.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldIngredient.transform.SetParent(areaPosition.position.transform);
                currentHeldIngredient = null;
                Picker.Instance.SetCurrentHeldItem(null); //Refactor
                return;
            }
        }
    }
}

[System.Serializable]
public class AreaPosition
{
    public GameObject position;
    public bool isPositionTaken;
}