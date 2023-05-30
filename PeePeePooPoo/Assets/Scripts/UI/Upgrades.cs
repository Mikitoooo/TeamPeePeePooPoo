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
                break;
            case 2:
                // Increase damage
                upgradeTitle.text = "Increase Damage";
                ButtonImage.sprite = uiImages[1];
                break;
            case 3:
                // Add a slime buddy
                upgradeTitle.text = "Slime Buddy";
                ButtonImage.sprite = uiImages[2];
                break;
            case 4:
                // increase movement speed
                upgradeTitle.text = "Movement Speed";
                ButtonImage.sprite = uiImages[3];
                break;
            case 5:
                // Gain additional jump
                upgradeTitle.text = "Extra Jump";
                ButtonImage.sprite = uiImages[4];
                break;
            case 6:
                // Jump Height
                upgradeTitle.text = "Jump Height";
                ButtonImage.sprite = uiImages[5];
                break;
            case 7:
                // Health Regen Increase
                upgradeTitle.text = "Health Regen";
                ButtonImage.sprite = uiImages[6];
                break;
            case 8:
                // Dash Force
                upgradeTitle.text = "Dash Force";
                ButtonImage.sprite = uiImages[7];
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
            case 6:
                // Jump Height
                AddJumpHeight();
                PlayerStats.instance.LevelUpFeedback("+ Jump Height");
                break;
            case 7:
                // Health Regen Increase
                AddHealthRegen();
                PlayerStats.instance.LevelUpFeedback("+ Health Regen");
                break;
            case 8:
                // Dash Force
                AddDashForce();
                PlayerStats.instance.LevelUpFeedback("+ Dash Force");
                break;
        }
    }

    void IncreaseFireRate()
    {
        PlayerStats.instance.rateOfFireEV = PlayerStats.instance.rateOfFireEV + RateOfFireIncrease;

        PlayerShoot.instance.fireRate = Mathf.Pow(PlayerShoot.instance.fireRate, PlayerStats.instance.rateOfFireEV);

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void IncreaseDamage()
    {
        PlayerShoot.instance.damage = PlayerShoot.instance.damage + damageIncrease;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void AddSlimeBuddy()
    {
        PlayerShoot.instance.SpawnSlimeBuddy();

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void IncreaseMovementSpeed()
    {
        PlayerController.instance.moveSpeed = PlayerController.instance.moveSpeed + moveSpeedIncrease;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void AddExtraJump()
    {
        PlayerController.instance.maxNumberOfJumps++;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void AddJumpHeight()
    {
        PlayerController.instance.jumpForce = PlayerController.instance.jumpForce + 2;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void AddHealthRegen()
    {
        PlayerStats.instance.healthRegenRate = PlayerStats.instance.healthRegenRate + PlayerStats.instance.healthRegenRateIncrease;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

    void AddDashForce()
    {
        PlayerController.instance.dashDistance = PlayerController.instance.dashDistance + 2;

        //Hide Upgrade window
        upgradeWindow.SetActive(false);

    }

}
