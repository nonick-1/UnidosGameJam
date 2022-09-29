using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Item clickedIngredient;

    public virtual void CreateItem()
    {
        Debug.Log("Spawned Item!");
        if (Picker.Instance.GetCurrentHeldItem() == null)
        {
            Item cachedIngredient = Instantiate(clickedIngredient, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
            Picker.Instance.SetCurrentHeldItem(cachedIngredient);
            cachedIngredient.transform.localPosition = Vector3.zero;
        }
    }
}