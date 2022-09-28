using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plates : MonoBehaviour
{
    [SerializeField] IngredientTypes currentType;
    public Equipment EquipmentHovered { get; set; }
    public bool IsOverValidEquipment { get; set; }

    public IngredientTypes GetCurrentType()
    {
        return currentType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EquipmentHovered = collision.GetComponent<Equipment>();
        if (EquipmentHovered && EquipmentHovered.GetIngredientsAllowed() == currentType)
        {
            Debug.Log("Is over valid equipment!");
            IsOverValidEquipment = true;
        }
    }

    //TODO: Refactor into interface with Ingredient.cs
    public void PlacePlateOnEquipment()
    {
        EquipmentHovered.PlacePlate(this);
    }
}
