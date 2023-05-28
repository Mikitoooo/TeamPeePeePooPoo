using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FeedbackController : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public Transform canvasTransform;

    // Update is called once per frame
    public void AssignText(string text, float lifeTime)
    {
        feedbackText.text = text;

        Color textColor = new Color(1, 1, 1, 0);

        canvasTransform.DOMove(new Vector3(canvasTransform.position.x, canvasTransform.position.y + 10, canvasTransform.position.z), lifeTime);
        feedbackText.DOColor(textColor, lifeTime);
        Destroy(canvasTransform.gameObject, lifeTime);
    }
}
