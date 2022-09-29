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

    public override void Update()
    {
        base.Update();

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

    public Doneness GetCurrentDoneness()
    {
        return currentDoneness;
    }

    public void SetIsCooking(bool isCookingCached) => isCooking = isCookingCached;

    public override void HoverInteraction(Equipment equipmentHovered)
    {
        if (currentHoveredEquipment.IsAbleToPlaceItem(this))
            isCooking = true;
    }

    public override void PickupInteraction()
    {
        isCooking = false;
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if (GetCurrentSlotTaken() != null)
            GetCurrentSlotTaken().isPositionTaken = false;
    }
}
