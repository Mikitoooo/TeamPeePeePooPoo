using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    // leveling stuff
    public float currentExp;
    public float expRequired;
    public int playerLevel = 1;

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
    }
    public void UpdateExpRequirments()
    {
        float baseXPAmount = 75f;
        float ev = 1.25f;
        expRequired = Mathf.Round(baseXPAmount * Mathf.Pow(playerLevel, ev));
    }
}
