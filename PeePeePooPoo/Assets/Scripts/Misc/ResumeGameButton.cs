using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameButton : MonoBehaviour
{
    public void ResumeGame()
    {
        //Time.timeScale = 1;
        UIController.instance.pause = false;
        UIController.instance.pauseWindow.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
