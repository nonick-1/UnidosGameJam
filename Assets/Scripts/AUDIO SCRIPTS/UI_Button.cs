using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    // Start is called before the first frame update
    public void onClick()
    {
        AkSoundEngine.PostEvent("UI_Button_Positive_1", gameObject);
    }

}
