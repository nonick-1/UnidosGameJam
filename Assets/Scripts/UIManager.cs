using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public delegate void OnStart();
    public static event OnStart onStart;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gamePlayMenu;

    [SerializeField] TextMeshProUGUI successfulText;
    [SerializeField] TextMeshProUGUI failedText;

    [SerializeField] Slider progressSlider;

    int failedMeals, successedMeals;
    float destinationRate = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
    }

    public void AddMoreTime()
    {
        progressSlider.value -= 0.1f;
    }

    private void OnEnable()
    {
        Character.onSuccessfulOrder += UpdateSuccessfulText;
        //Character.onSuccessfulOrder += AddMoreTime;

        Character.onFailedOrder += UpdateFailedText;
    }

    private void OnDisable()
    {
        Character.onSuccessfulOrder -= UpdateSuccessfulText;
        //Character.onSuccessfulOrder -= AddMoreTime;

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
