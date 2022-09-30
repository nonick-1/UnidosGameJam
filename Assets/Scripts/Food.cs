using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    bool isCooking;

    float timeElapsed = 0;
    [SerializeField] float timeBeforeNextCookedStage = 5f;

    Doneness currentDoneness = Doneness.Raw;

    [SerializeField] Sprite[] cookingStageSprites;

    Plate hoveredPlate;

    public void Update()
    {
        if (isCooking && (int)currentDoneness < cookingStageSprites.Length - 1)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > timeBeforeNextCookedStage)
            {
                currentDoneness++;
                currentSpriteRend.sprite = cookingStageSprites[(int)currentDoneness];
                timeElapsed = 0;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        hoveredPlate = collision.GetComponent<Plate>();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        hoveredPlate = null;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        hoveredPlate = collision.GetComponent<Plate>();
    }

    public Doneness GetCurrentDoneness()
    {
        return currentDoneness;
    }

    public void SetIsCooking(bool isCookingCached) => isCooking = isCookingCached;

    public override void HoverInteraction()
    {
        if (currentHoveredEquipment)
        {
            currentHoveredEquipment.IsAbleToPlaceItem(this);
            isCooking = true;
        }
        else if(hoveredPlate && this.currentDoneness == Doneness.Cooked)
        {
            hoveredPlate.VerifyTacoRecipeExists(this);
        }
    }

    public override void PickupInteraction()
    {
        Debug.Log("Picked up!");

        isCooking = false;
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if (GetCurrentSlotTaken() != null)
            GetCurrentSlotTaken().isPositionTaken = false;
    }
}
