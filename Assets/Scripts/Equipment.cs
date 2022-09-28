using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] IngredientTypes ingredientAllowed;
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

    public bool IsAbleToPlaceIngredient(Ingredient currentHeldIngredient)
    {
        foreach(var areaPosition in itemPositions)
        {
            if(!areaPosition.isPositionTaken && ingredientAllowed == currentHeldIngredient.GetCurrentIngredientType())
            {
                areaPosition.isPositionTaken = true;
                currentHeldIngredient.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldIngredient.transform.SetParent(areaPosition.position.transform);
                currentHeldIngredient = null;
                Picker.Instance.SetCurrentHeldIngredient(null); //Refactor
                Debug.Log("Placed!");
                return true;
            }
        }

        return false;
    }

    public void PlacePlate(Plates currentHeldPlate)
    {
        foreach (var areaPosition in itemPositions)
        {
            if (!areaPosition.isPositionTaken && ingredientAllowed == currentHeldPlate.GetCurrentType())
            {
                areaPosition.isPositionTaken = true;
                currentHeldPlate.gameObject.transform.position = areaPosition.position.transform.position;
                currentHeldPlate.transform.SetParent(areaPosition.position.transform);
                currentHeldPlate = null;
                Picker.Instance.SetCurrentHeldPlate(null); //Refactor
                Debug.Log("Placed!");
            }
        }

        Debug.Log("Not Placed!");
    }

    public IngredientTypes GetIngredientsAllowed()
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