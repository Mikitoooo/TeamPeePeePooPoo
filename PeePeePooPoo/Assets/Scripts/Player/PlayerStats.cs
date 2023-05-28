using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float maxHealth;
    public float currentHealth;
    public float levelUpHealthIncrease;
    public float healthRegenRate;
    public float healthRegenRateIncrease;

    // leveling stuff
    public float currentExp;
    public float expRequired;
    public int playerLevel = 1;
    [HideInInspector]
    public float rateOfFireEV = 1;

    public GameObject feedbackCanvas;
    public Transform canvasSpawn;
    public Transform playerToGrow;

    // Start is called before the first frame update
    void Awake()
    {
        //Ensure there's only once instance of the player stats script
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        // Calculate xp requirements
        UpdateExpRequirments();
        //Set health to max health
        currentHealth = maxHealth;
        // Begin Health Regen
        StartCoroutine(HeatlhRegen());
    }

    public void LevelUp()
    {
        // slime scale growth amount
        float growthAmount = 0.2f;
        // update the level requirements
        UpdateExpRequirments();
        // increase the size of the player
        playerToGrow.DOScale(new Vector3(playerToGrow.localScale.x + growthAmount, playerToGrow.localScale.y + growthAmount, playerToGrow.localScale.z + growthAmount),0.5f);
        // Adjust camera distance on level up
        CameraAdjustToScale.instance.AdjustCameraDistance();
        //Reset player xp to 0
        currentExp = 0;
        // Update the level text UI display
        UIController.instance.UpdateLevelNumbers();
        // Enable upgrade window pop up
        UIController.instance.upgradeWindow.SetActive(true);
        // Assign Button Upgrades
        UIController.instance.AssignUpgrades();
        // Increase Health on Level Up
        maxHealth = maxHealth + levelUpHealthIncrease;
        currentHealth = currentHealth + levelUpHealthIncrease;
        UIController.instance.UpdateHealthBar();
    }
    //Updates xp requirement amounts
    public void UpdateExpRequirments()
    {
        float baseXPAmount = 75f;
        float ev = 1.15f;
        expRequired = Mathf.Round(baseXPAmount * Mathf.Pow(playerLevel, ev));
    }

    // Feedback when picking up xp cubes
    public void ExpCollectionFeedback(float xpAmount)
    {
        //instantiate canvas
        GameObject canvasInstance = Instantiate(feedbackCanvas, canvasSpawn.transform.position, canvasSpawn.transform.rotation,canvasSpawn);
        canvasInstance.GetComponent<FeedbackController>().AssignText("+ " + Mathf.Round(xpAmount) + " Exp",1f);
    }

    // Feedback when leveling xp cubes
    public void LevelUpFeedback(string upgradePurchased)
    {
        //instantiate canvas
        GameObject canvasInstance = Instantiate(feedbackCanvas, canvasSpawn.transform.position, canvasSpawn.transform.rotation,canvasSpawn);
        canvasInstance.GetComponent<FeedbackController>().AssignText(upgradePurchased,2f);
    }

    IEnumerator HeatlhRegen()
    {
        yield return new WaitForSeconds(1);
        if(currentHealth < maxHealth)
        {
            currentHealth += healthRegenRate;
            UIController.instance.UpdateHealthBar();

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
        StartCoroutine(HeatlhRegen());
    }
}
