using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [Header("Level Up Fillbar")]
    public Image levelUpFillbar;
    public Text nextLevelText;
    [Header("Level Up Window")]
    public GameObject upgradeWindow;
    public Upgrades[] upgradeButton;
    [Header("Health Fillbar")]
    public Image healthFillbar;
    [Header ("Display TImer")]
    public TextMeshProUGUI timerText;
    [Header("Display Score")]
    public TextMeshProUGUI scoreText;
    public float score;
    [Header("PlayerDeathUI")]
    public GameObject deathWindow;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI finalTime;
    [Header("PauseWindow")]
    public GameObject pauseWindow;
    public bool pause;

    private float startTime;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        // Set upgrade window to inactive at the start
        upgradeWindow.SetActive(false);
        // Calculate xp needed
        UpdateOnXpCollection();
        // Set player's healthbar
        UpdateHealthBar();
        // Set Start Time
        startTime = Time.time;
        elapsedTime = 0f;
    }
    private void Update()
    {
        if (upgradeWindow.activeSelf)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;

        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
        //Pause Game
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = true;
        }
        if (!pause)
        {
            // Run Timer
            elapsedTime = Time.time - startTime;
            // Display the time
            UpdateTimerDisplay(elapsedTime);
            Time.timeScale = 1;
        }
        else
        {
            pauseWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            print(Cursor.visible);
            Time.timeScale = 0;
        }
    }

    // Updates fill bar on level up
    public void UpdateOnXpCollection()
    {
        levelUpFillbar.DOFillAmount(PlayerStats.instance.currentExp / PlayerStats.instance.expRequired, 0.25f);
    }

    // Updates fill bar on health
    public void UpdateHealthBar()
    {
        healthFillbar.DOFillAmount(PlayerStats.instance.currentHealth / PlayerStats.instance.maxHealth, 0.25f);
    }
    // Updates level text
    public void UpdateLevelNumbers()
    {
        //Next Level number
        int nextLevel = PlayerStats.instance.playerLevel + 1;

        nextLevelText.text = nextLevel.ToString();
    }

    public void AssignUpgrades()
    {
        int[] randomNumbers = new int[3];
        int min = 1;
        int max = 8;

        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(min, max + 1);

            // Check for duplicates
            while (System.Array.IndexOf(randomNumbers, randomNumber) >= 0)
            {
                randomNumber = Random.Range(min, max + 1);
            }

            randomNumbers[i] = randomNumber;
            // assign number to button
            upgradeButton[i].rng = randomNumber;
            upgradeButton[i].AssignUpgradeToButton();
        }

    }

    private void UpdateTimerDisplay(float time)
    {
        // Format the time as minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update the timer text
        timerText.text = timeString;
    }

    public void UpdatePlayerScore(float value)
    {
        score = Mathf.Round(score + value);

        // Update the timer text
        scoreText.text = score.ToString();
    }


    public void PlayerDiedUI()
    {
        pause = true;
        deathWindow.SetActive(true);
        finalScore.text = score.ToString();
        finalTime.text = timerText.text;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
