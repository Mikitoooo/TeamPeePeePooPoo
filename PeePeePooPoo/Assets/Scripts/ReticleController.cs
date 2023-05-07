using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReticleController : MonoBehaviour
{
    public static ReticleController instance;

    Vector2 leftReticleStartPos;
    Vector2 rightReticleStartPos;
    Vector2 topReticleStartPos;
    Vector2 botReticleStartPos;

    public float leftReticleMaxPosX;
    public float rightReticleMaxPosX;
    public float topReticleMaxPosY;
    public float botReticleMaxPosY;

    public RectTransform leftReticle;
    public RectTransform rightReticle;
    public RectTransform topReticle;
    public RectTransform botReticle;

    public float reticleKickAmount;

    bool recentlyShot = false;
    float currentTime = 0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        //Initialize start pos
        leftReticleStartPos = leftReticle.anchoredPosition;
        rightReticleStartPos = rightReticle.anchoredPosition;
        topReticleStartPos = topReticle.anchoredPosition;
        botReticleStartPos = botReticle.anchoredPosition;
    }

    private void Update()
    {
        if (recentlyShot == false)
        {
            leftReticle.DOAnchorPos(leftReticleStartPos, 1);
            rightReticle.DOAnchorPos(rightReticleStartPos, 1);
            topReticle.DOAnchorPos(topReticleStartPos, 1);
            botReticle.DOAnchorPos(botReticleStartPos, 1);
        }
        else if(recentlyShot == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 0.2f)
                recentlyShot = false;
        }
    }

    public void ReticleKick()
    {
        recentlyShot = true;

        // Left Reticle
        if (leftReticle.anchoredPosition.x >= leftReticleMaxPosX)
            leftReticle.DOAnchorPos(new Vector3(leftReticle.anchoredPosition.x - reticleKickAmount, 0, 0), 1);
        // Right Reticle
        if (rightReticle.anchoredPosition.x <= rightReticleMaxPosX)
            rightReticle.DOAnchorPos(new Vector3(rightReticle.anchoredPosition.x + reticleKickAmount, 0, 0), 1);
        // Top Reticle
        if (topReticle.anchoredPosition.y <= topReticleMaxPosY)
            topReticle.DOAnchorPos(new Vector3(0, topReticle.anchoredPosition.y + reticleKickAmount, 0), 1);
        // Bot Reticle
        if (botReticle.anchoredPosition.y >= botReticleMaxPosY)
            botReticle.DOAnchorPos(new Vector3(0, botReticle.anchoredPosition.y - reticleKickAmount, 0), 1);

        currentTime = 0f;
    }
}
