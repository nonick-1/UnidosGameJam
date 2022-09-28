using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Doneness { Raw = 0, Cooked = 1, Burnt = 2}
public enum IngredientTypes { Tortilla, AlPastor, Onions, Cilantro, Shrimp, CarneAsada, SalsaRojo, SalsaVerde, Plates}

public class Item : MonoBehaviour
{
    [SerializeField] IngredientTypes currentType;
    [SerializeField] Item clickedIngredient;
    [SerializeField] Sprite[] cookingStageSprites;

    //Could randomize to make it a little tricky
    [SerializeField] float timebeforeNextCookedStage = 5f;

    AreaPosition currentSlotTaken;

    Doneness currentDoneness = Doneness.Raw;

    bool isCooking;

    float timeElapsed = 0;

    SpriteRenderer currentSpriteRend;


    public void SetCurrentSlotTaken(AreaPosition slot) => currentSlotTaken = slot;
    public void SetIsCooking(bool isCookingCached) => isCooking = isCookingCached;

    private void Start()
    {
        currentSpriteRend = GetComponent<SpriteRenderer>();
    }

    public IngredientTypes GetCurrentIngredientType()
    {
        return currentType;
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

    public void Pickup(bool isIngredient)
    {
        isCooking = false;
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if(isIngredient)
            currentSlotTaken.isPositionTaken = false;
    }

    public void ResetTransform()
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public Doneness GetCurrentDoneness()
    {
        return currentDoneness;
    }
}