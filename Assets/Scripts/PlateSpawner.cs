using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : Spawner
{
    [SerializeField] Plate clickedPlate;
    public override void CreateItem()
    {
        if (Picker.Instance.GetCurrentHeldItem() == null)
        {
            Debug.Log("Created Plate!");
            Plate cachedIngredient = Instantiate(clickedPlate, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
            Picker.Instance.SetCurrentHeldItem(cachedIngredient);
            cachedIngredient.transform.localPosition = Vector3.zero;
        }
    }
}