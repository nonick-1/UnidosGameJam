using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toppings : Item
{
    Plate hoveredPlate;

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

    public override void HoverInteraction()
    {
        if (currentHoveredEquipment)
        {
            Debug.Log("Over Equipment");
            currentHoveredEquipment.IsAbleToPlaceItem(this);
        }
        else if (hoveredPlate)
        {
            Debug.Log("Over Plate");
            hoveredPlate.VerifyTacoRecipeExists(this);
        }
    }

    public override void PickupInteraction()
    {
        this.transform.SetParent(Picker.Instance.Handler.transform);
        Picker.Instance.SetCurrentHeldItem(this);
        this.transform.localPosition = Vector3.zero;
    }
}
