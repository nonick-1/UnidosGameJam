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
    [SerializeField] GameObject gameOverMenu;

    [SerializeField] TextMeshProUGUI successfulText;
    [SerializeField] TextMeshProUGUI failedText;

    int failedMeals, successedMeals;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                ShowGameOverScreen();
            }
        }
    }

    public void ResetGame()
    {
        mainMenu.gameObject.SetActive(false);
        gamePlayMenu.gameObject.SetActive(true);
        gameOverMenu.gameObject.SetActive(false);

        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining = 70f;

        successedMeals = 0;
        failedMeals = 0;
    }

    void ShowGameOverScreen()
    {
        mainMenu.gameObject.SetActive(false);
        gamePlayMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(true);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddMoreTime()
    {
        timeRemaining += 5f;
    }

    private void OnEnable()
    {
        Character.onSuccessfulOrder += UpdateSuccessfulText;
        Character.onSuccessfulOrder += AddMoreTime;

        Character.onFailedOrder += UpdateFailedText;
    }

    private void OnDisable()
    {
        Character.onSuccessfulOrder -= UpdateSuccessfulText;
        Character.onSuccessfulOrder -= AddMoreTime;

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

        ResetGame();

        if(Picker.Instance.GetCurrentHeldItem() != null)
            Picker.Instance.SetCurrentHeldItem(null, true);

        mainMenu.SetActive(false);
        gamePlayMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        onStart?.Invoke();

        // Starts the timer automatically
        timerIsRunning = true;
    }
}