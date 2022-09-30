using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void OnStart();
    public static event OnStart onStart;

    [SerializeField] GameObject canvasGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPress()
    {
        canvasGameObject.SetActive(false);
        onStart?.Invoke();
    }
}
