using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAdjustToScale : MonoBehaviour
{
    public static CameraAdjustToScale instance;
    public float startCameraDistance;
    public float distanceGrowthAmount;

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

        startCameraDistance = this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
    }

    // Update is called once per frame
    public void AdjustCameraDistance()
    {
        this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = (startCameraDistance - distanceGrowthAmount) + (PlayerStats.instance.playerLevel * distanceGrowthAmount);
    }
}
