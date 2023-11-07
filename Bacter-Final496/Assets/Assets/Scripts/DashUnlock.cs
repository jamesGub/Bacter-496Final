using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUnlock : MonoBehaviour
{
    public PlayerController playerController; 
    public MitosisGauge mitosisGauge;

    public void EnableDashAbility()
    {
        if (playerController != null)
        {
            playerController.dashUnlock = true; 
            playerController.StartDashAbility(); 
            mitosisGauge.ResumeGame();
        }
    }
}

