using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Doneness { Raw = 0, Cooked = 1, Burnt = 2}
public enum IngredientTypes { Tortilla, AlPastor, Onions, Cilantro, Shrimp, CarneAsada, SalsaRojo, SalsaVerde, Plates}

public class Ingredient : MonoBehaviour
{
    [SerializeField] IngredientTypes currentType;

    //Ingredient to Spawn when clicked
    [SerializeField] Ingredient clickedIngredient;

    Doneness currentDoneness = Doneness.Raw;

    bool isCooking;
    public bool IsOverValidEquipment { get; set; }
    public Equipment EquipmentHovered { get; set; }

    float timeElapsed = 0;

    SpriteRenderer currentSpriteRend;
    [SerializeField] Sprite[] cookingStageSprites;

    //Could randomize to make it a little tricky
    [SerializeField] float timebeforeNextCookedStage = 5f;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }

    public IngredientTypes GetCurrentIngredientType()
    {
        return currentType;
    }

    public void Pickup()
    {
        if (Picker.Instance.GetCurrentHeldIngredient() == null)
        {
            Ingredient cachedIngredient = Instantiate(clickedIngredient, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
            Picker.Instance.SetCurrentHeldIngredient(cachedIngredient);
            cachedIngredient.transform.localPosition = Vector3.zero;
        }
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
        isCooking = EquipmentHovered.IsAbleToPlaceIngredient(this);
    }
}