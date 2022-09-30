using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public delegate void OnStart();
    public static event OnStart onStart;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gamePlayMenu;

    [SerializeField] TextMeshProUGUI successfulText;
    [SerializeField] TextMeshProUGUI failedText;

    int failedMeals, successedMeals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Character.onSuccessfulOrder += UpdateSuccessfulText;
        Character.onFailedOrder += UpdateFailedText;
    }

    private void OnDisable()
    {
        Character.onSuccessfulOrder -= UpdateSuccessfulText;
        Character.onFailedOrder -= UpdateFailedText;
    }

    public void UpdateSuccessfulText()
    {
        successedMeals++;
        successfulText.text = $"Successful Orders: {successedMeals}";
    }

    public void UpdateFailedText()
    {
        failedMeals++;
        failedText.text = $"Failed Orders: {failedMeals}";
    }

    public void StartButtonPress()
    {
        Debug.Log("Pressed!");
        mainMenu.SetActive(false);
        gamePlayMenu.SetActive(true);
        onStart?.Invoke();
    }
}
