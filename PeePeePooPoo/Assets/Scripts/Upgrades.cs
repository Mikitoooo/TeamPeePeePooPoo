using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public Text upgradeTitle;
    public int rng;

    public void AssignUpgradeToButton()
    {
        // Determine which upgrade the player is getting
        switch (rng)
        {
            case 1:
                // Increase rate of fires
                upgradeTitle.text = "Rate of Fire";
                break;
            case 2:
                // Increase damage
                upgradeTitle.text = "Increase Damage";
                break;
            case 3:
                // Add a slime buddy
                upgradeTitle.text = "Slime Buddy";
                break;
            case 4:
                // increase movement speed
                upgradeTitle.text = "Movement Speed";
                break;
            case 5:
                // Gain additional jump
                upgradeTitle.text = "Extra Jump";
                break;
        }
    }
}
