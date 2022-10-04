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

    [SerializeField] Animator smokeAnimator;

    Plate hoveredPlate;

    public override void Start()
    {
        base.Start();
        PlayCookingAnimation(false);
    }

    public void Update()
    {
        if (isCooking && (int)currentDoneness < cookingStageSprites.Length - 1)
        {
            PlayCookingAnimation(true);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > timeBeforeNextCookedStage)
            {
                currentDoneness++;
                currentSpriteRend.sprite = cookingStageSprites[(int)currentDoneness];
                timeElapsed = 0;
            }

            if (currentDoneness == Doneness.Burnt)
                PlayCookingAnimation(false);
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

    public void PlayCookingAnimation(bool isPlaying)
    {
        if(smokeAnimator)
            smokeAnimator.gameObject.SetActive(isPlaying);
    }

    public Doneness GetCurrentDoneness()
    {
        return currentDoneness;
    }

    public void SetIsCooking(bool isCookingCached) => isCooking = isCookingCached;

    public override void HoverInteraction()
    {
        if (currentHoveredEquipment && currentHoveredEquipment.IsAbleToPlaceItem(this))
        {
            isCooking = true;
            Debug.Log("Is cooking tortilla!");
        }
        else if(hoveredPlate && this.currentDoneness == Doneness.Cooked)
        {
            hoveredPlate.VerifyTacoRecipeExists(this);
        }
    }

    public override void PickupInteraction()
    {
        isCooking = false;
        PlayCookingAnimation(false);
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;

        if (GetCurrentSlotTaken() != null)
            GetCurrentSlotTaken().isPositionTaken = false;
    }
}
