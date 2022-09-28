using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] protected List<IngredientTypes> ingredientAllowed;
    [SerializeField] protected List<AreaPosition> itemPositions;

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

    public virtual void IsAbleToPlaceItem(Item currentHeldIngredient)
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