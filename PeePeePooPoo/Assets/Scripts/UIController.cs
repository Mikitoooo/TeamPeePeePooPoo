using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image levelUpFillbar;
    public Text currentLevelText;
    public Text nextLevelText;

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

        UpdateOnXpCollection();
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
}
