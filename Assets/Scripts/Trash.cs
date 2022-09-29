using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, IInteraction
{
    public void HoverInteraction(Item item)
    {
        Picker.Instance.SetCurrentHeldItem(null, true);
    }
}
