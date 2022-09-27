using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Doneness { Raw = 0, Cooked = 1, Burnt = 2}
public enum IngredientTypes { Tortilla, Meat}

public class Ingredient : MonoBehaviour
{
    [SerializeField] IngredientTypes currentIngredientType;
    Doneness currentDoneness = Doneness.Raw;

    bool isCooking;
    public bool IsOverValidEquipment { get; set; }
    public Equipment EquipmentHovered { get; set; }

    float timeElapsed = 0;

    SpriteRenderer currentSpriteRend;
    [SerializeField] Sprite[] cookingStageSprites;

    //Could randomize to make it a little tricky
    [SerializeField] float timebeforeNextCookedStage = 5f;

    //Since we have the stages for each food I'm hard coding stage 2. Refactor
    int currentFoodStage;
    const int stageFoodIsDone = 2;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }

    public IngredientTypes GetCurrentIngredientType()
    {
        return currentIngredientType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EquipmentHovered = collision.GetComponent<Equipment>();
        if (EquipmentHovered && EquipmentHovered.GetIngredientsAllowed() == currentIngredientType)
        {
            Debug.Log("Is over valid equipment!");
            IsOverValidEquipment = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void Update()
    {
        if(isCooking && (int)currentDoneness < cookingStageSprites.Length -1)
        {
            timeElapsed += Time.deltaTime;
  
            if (timeElapsed > timebeforeNextCookedStage)
            {
                currentDoneness++;
                currentSpriteRend.sprite = cookingStageSprites[(int)currentDoneness];
                timeElapsed = 0;
            }
        }
    }

    public void PlaceIngredientOnEquipment()
    {
        isCooking = EquipmentHovered.IsAbleToPlaceItem(this);
    }
}