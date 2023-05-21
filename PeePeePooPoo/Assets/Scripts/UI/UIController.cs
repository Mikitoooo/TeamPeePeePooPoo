using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [Header("Level Up Fillbar")]
    public Image levelUpFillbar;
    public Text currentLevelText;
    public Text nextLevelText;
    [Header("Level Up Window")]
    public GameObject upgradeWindow;
    public Upgrades[] upgradeButton;

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
    }

    // Updates fill bar on level up
    public void UpdateOnXpCollection()
    {
        levelUpFillbar.DOFillAmount(PlayerStats.instance.currentExp / PlayerStats.instance.expRequired, 0.25f);
    }
    // Updates level text
    public void UpdateLevelNumbers()
    {
        //Next Level number
        int nextLevel = PlayerStats.instance.playerLevel + 1;

        currentLevelText.text = PlayerStats.instance.playerLevel.ToString();
        nextLevelText.text = nextLevel.ToString();
    }

    public void AssignUpgrades()
    {
        int[] randomNumbers = new int[3];
        int min = 1;
        int max = 5;

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

        foreach (int number in randomNumbers)
        {
            Debug.Log(number);
        }
    }
}