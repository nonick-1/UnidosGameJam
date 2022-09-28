using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStacks : MonoBehaviour
{
    [SerializeField] Plate plate;

    private void OnMouseDown()
    {
        //if (Picker.Instance.GetCurrentHeldPlate() == null)
        //{
        //    Plate cachedPlate = Instantiate(plate, Vector3.zero, Quaternion.identity, Picker.Instance.Handler.transform);
        //    Picker.Instance.SetCurrentHeldPlate(cachedPlate);
        //    cachedPlate.transform.localPosition = Vector3.zero;
        //}
    }
}
