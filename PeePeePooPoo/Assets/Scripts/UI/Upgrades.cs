using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public float RateOfFireIncrease;
    public float damageIncrease;
    public float moveSpeedIncrease;

    public Text upgradeTitle;
    public Button upgradeButton;
    public GameObject upgradeWindow;
    [Header("Upgrade Images")]
    public Image ButtonImage;
    public Sprite[] uiImages;
    //[HideInInspector]
    public int rng;

    public void AssignUpgradeToButton()
    {
        // Determine which upgrade the player is getting
        switch (rng)
        {
            case 1:
                // Increase rate of fires
                upgradeTitle.text = "Rate of Fire";
                ButtonImage.sprite = uiImages[0];
                //upgradeButton.onClick.AddListener(IncreaseFireRate);
                break;
            case 2:
                // Increase damage
                upgradeTitle.text = "Increase Damage";
                ButtonImage.sprite = uiImages[1];
                //upgradeButton.onClick.AddListener(IncreaseDamage);
                break;
            case 3:
                // Add a slime buddy
                upgradeTitle.text = "Slime Buddy";
                ButtonImage.sprite = uiImages[2];
                //upgradeButton.onClick.AddListener(AddSlimeBuddy);
                break;
            case 4:
                // increase movement speed
                upgradeTitle.text = "Movement Speed";
                ButtonImage.sprite = uiImages[3];
                //upgradeButton.onClick.AddListener(IncreaseMovementSpeed);
                break;
            case 5:
                // Gain additional jump
                upgradeTitle.text = "Extra Jump";
                ButtonImage.sprite = uiImages[4];
                //upgradeButton.onClick.AddListener(AddExtraJump);
                break;
        }
    }

    public void UpgradeSelected()
    {
        switch (rng)
        {
            case 1:
                // Increase rate of fires
                IncreaseFireRate();
                PlayerStats.instance.LevelUpFeedback("+ Fire Rate");
                break;
            case 2:
                // Increase damage
                IncreaseDamage();
                PlayerStats.instance.LevelUpFeedback("+ Damage");
                break;
            case 3:
                // Add a slime buddy
                AddSlimeBuddy();
                PlayerStats.instance.LevelUpFeedback("+ Slime Buddy");
                break;
            case 4:
                // increase movement speed
                IncreaseMovementSpeed();
                PlayerStats.instance.LevelUpFeedback("+ Movement Speed");
                break;
            case 5:
                // Gain additional jump
                AddExtraJump();
                PlayerStats.instance.LevelUpFeedback("+ Extra Jump");
                break;
        }
    }

    void IncreaseFireRate()
    {
        PlayerStats.instance.rateOfFireEV = PlayerStats.instance.rateOfFireEV + RateOfFireIncrease;

        PlayerShoot.instance.fireRate = Mathf.Pow(PlayerShoot.instance.fireRate, PlayerStats.instance.rateOfFireEV);

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded fire rate");
    }

    void IncreaseDamage()
    {
        PlayerShoot.instance.damage = PlayerShoot.instance.damage + damageIncrease;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded Damage");
    }

    void AddSlimeBuddy()
    {
        PlayerShoot.instance.SpawnSlimeBuddy();

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Added slime buddy");
    }

    void IncreaseMovementSpeed()
    {
        PlayerController.instance.moveSpeed = PlayerController.instance.moveSpeed + moveSpeedIncrease;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded movement speed");
    }

    void AddExtraJump()
    {
        PlayerController.instance.maxNumberOfJumps++;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Added extra jump");
    }

}
