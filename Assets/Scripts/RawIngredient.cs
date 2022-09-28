using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawIngredient : MonoBehaviour
{
    [SerializeField] Ingredient clickedIngredient;

    private void OnMouseDown()
    {
        if(Picker.Instance.GetCurrentHeldIngredient() == null)
        {
            Ingredient cachedIngredient = Instantiate(clickedIngredient, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
            Picker.Instance.SetCurrentHeldIngredient(cachedIngredient);
            cachedIngredient.transform.localPosition = Vector3.zero;
        }
    }
}
