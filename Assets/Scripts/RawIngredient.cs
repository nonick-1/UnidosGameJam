using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawIngredient : MonoBehaviour
{
    [SerializeField] Ingredient clickedIngredient;

    private void OnMouseDown()
    {
        //if(Picker.Instance.CurrentHeldIngredient == null)
        {
            Picker.Instance.CurrentHeldIngredient = Instantiate(clickedIngredient, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
            Picker.Instance.CurrentHeldIngredient.transform.localPosition = Vector3.zero;
            Debug.Log("Created Item!");
        }
    }
}
