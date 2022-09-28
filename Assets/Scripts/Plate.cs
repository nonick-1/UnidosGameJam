using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] IngredientTypes currentType;
    public Equipment EquipmentHovered { get; set; }
    public bool IsOverValidEquipment { get; set; }

    [SerializeField] List<TacoCombinations> allTacoCombinations;

    SpriteRenderer plateSpriteRenderer;

    private void Start()
    {
        plateSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IngredientTypes GetCurrentType()
    {
        return currentType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EquipmentHovered = collision.GetComponent<Equipment>();
        if (EquipmentHovered && EquipmentHovered.GetIngredientsAllowed().Contains(currentType))
        {
            Debug.Log("Is over valid equipment!");
            IsOverValidEquipment = true;
        }
    }

    //TODO: Refactor into interface with Ingredient.cs. Picker Places the object down but checks this object itself whether it can
    public void PlacePlateOnEquipment()
    {
        EquipmentHovered.PlacePlate(this);
    }
}

[System.Serializable]
public class TacoCombinations
{
    public List<IngredientTypes> recipeCombinations;
    public Sprite desiredPlateLook;
}
