using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeldIngredients
{
    Tortillas,
    Fajitas,
    Salsa
}

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


    private void SetCursor()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
    }

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

    void OnMouseDown()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //SetCursor();

        GameObject availablePosition = GetFreeKitchenArea();

        //No free positions
        if (!availablePosition)
            return;

        if (ingredientsAllowed.Contains(other.GetComponent<Ingredient>()))
        {
            //Allow Drop

            //Set Item on Equipment
            other.gameObject.transform.position = availablePosition.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetCursor();


    }
}

[System.Serializable]
class AreaPosition
{
    public GameObject position;
    public bool isPositionTaken;
}