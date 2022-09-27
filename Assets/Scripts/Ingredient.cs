using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool IsOverValidEquipment { get; set; }
    public Equipment EquipmentHovered { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EquipmentHovered = collision.GetComponent<Equipment>())
        {
            Debug.Log("Is over valid equipment!");
            IsOverValidEquipment = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"Collision Out: {collision}");

        if (collision.GetComponent<Equipment>())
        {
            Debug.Log("Is not over valid equipment!");
            IsOverValidEquipment = false;
            EquipmentHovered = null;
        }
    }

    public void PlaceIngredientOnEquipment()
    {
        EquipmentHovered.IsAbleToPlaceItem(this);
    }
}
