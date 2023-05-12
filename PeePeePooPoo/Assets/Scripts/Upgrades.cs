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
                //upgradeButton.onClick.AddListener(IncreaseFireRate);
                break;
            case 2:
                // Increase damage
                upgradeTitle.text = "Increase Damage";
                //upgradeButton.onClick.AddListener(IncreaseDamage);
                break;
            case 3:
                // Add a slime buddy
                upgradeTitle.text = "Slime Buddy";
                //upgradeButton.onClick.AddListener(AddSlimeBuddy);
                break;
            case 4:
                // increase movement speed
                upgradeTitle.text = "Movement Speed";
                //upgradeButton.onClick.AddListener(IncreaseMovementSpeed);
                break;
            case 5:
                // Gain additional jump
                upgradeTitle.text = "Extra Jump";
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
                break;
            case 2:
                // Increase damage
                IncreaseDamage();
                break;
            case 3:
                // Add a slime buddy
                AddSlimeBuddy();
                break;
            case 4:
                // increase movement speed
                IncreaseMovementSpeed();
                break;
            case 5:
                // Gain additional jump
                AddExtraJump();
                break;
        }
    }

    void IncreaseFireRate()
    {
        PlayerStats.instance.rateOfFireEV = PlayerStats.instance.rateOfFireEV + RateOfFireIncrease;

        PlayerShoot.instance.fireRate = Mathf.Pow(PlayerShoot.instance.fireRate, PlayerStats.instance.rateOfFireEV);

        // Remove all listeners
        upgradeButton.onClick.RemoveAllListeners();
        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded fire rate");
    }

    void IncreaseDamage()
    {
        PlayerShoot.instance.damage = PlayerShoot.instance.damage + damageIncrease;

        // Remove all listeners
        upgradeButton.onClick.RemoveAllListeners();
        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded Damage");
    }

    void AddSlimeBuddy()
    {
        PlayerShoot.instance.SpawnSlimeBuddy();
        // Remove all listeners
        upgradeButton.onClick.RemoveAllListeners();
        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Added slime buddy");
    }

    void IncreaseMovementSpeed()
    {
        PlayerController.instance.moveSpeed = PlayerController.instance.moveSpeed + moveSpeedIncrease;

        // Remove all listeners
        upgradeButton.onClick.RemoveAllListeners();
        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Upgraded movement speed");
    }

    void AddExtraJump()
    {
        PlayerController.instance.maxNumberOfJumps++;

        // Remove all listeners
        upgradeButton.onClick.RemoveAllListeners();
        //Hide Upgrade window
        upgradeWindow.SetActive(false);

        print("Added extra jump");
    }

}
