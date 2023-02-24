using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GearAndRPM : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private TMP_Text RPMText;
    [SerializeField] private TMP_Text GearText;
    [SerializeField] private CarControllerNew carController;
    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        RPMText.text = "RPM: " + carController.RPM.ToString("0,000");
        GearText.text = generateGearText();
    }

    private string generateGearText()
    {
        if (carController.gearState == GearState.Neutral)
        {
            return "Gear: N";
        }
        else if (carController.gearState == GearState.Reverse)
        {
            return "Gear: R";
        }
        else
        {
            return "Gear: " + (carController.currentGear + 1).ToString();
        }
    }
}
