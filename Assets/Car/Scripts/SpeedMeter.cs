using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private TMP_Text speedText;
    private Rigidbody rb;
    private int speedInt;

    private void Awake()
    {
        rb = car.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        speedInt = (int)Math.Round(3.6f * rb.velocity.magnitude);
        speedText.text = "Speed: " + speedInt + "km/h";
    }

}
