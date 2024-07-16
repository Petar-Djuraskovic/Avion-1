using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{

    public PlayerAvion PlayerAvion;

    public TextMeshProUGUI throttleText;
    public TextMeshProUGUI speedText;
    public GameObject throttleSlider;
    public Image speedometerHead;
    public GameObject speedometerHeadPivot;

    public Functions Func;

    public float minThrottleSliderY;
    public float maxThrottleSliderY;

    public float minSpeedometerRotation;
    public float maxSpeedometerRotation;

    private double sine;
    private double speedometerSine;

    [Range(0, 255)] public byte sineMinColor;
    [Range(0, 255)] public byte sineMaxColor;
    [Range(0, 255)] public byte speedometerSineMinColor;
    [Range(0, 255)] public byte speedometerSineMaxColor;
    public float minSineIncrement;
    public float maxSineIncrement;
    [Range(0, 1)] public float sineIncrement;

    public string unitOfMeasurement;
    public float machOneInUnit;
    public Dictionary<string, int> machOneDictionary = new()
    {
        ["kmh"] = 1235,
        ["mph"] = 767,
        ["knots"] = 666,
        ["m/s"] = 343,
        ["unities/s"] = 343,
        ["bananas/s"] = 1927,
        ["zoinks/glorp"] = 2147483647
    };
    private void Start()
    {
        machOneInUnit = machOneDictionary[unitOfMeasurement];
    }

    void FixedUpdate()
    {
        TextHandler(PlayerAvion, throttleText, speedText, speedometerHead, Func);
        ThrottleSliderHandler(PlayerAvion, throttleSlider.transform);
        SpeedometerHandler(PlayerAvion, speedometerHeadPivot.transform);
    }

    void TextHandler(PlayerAvion playerAvion, TextMeshProUGUI throttleText, TextMeshProUGUI speedText, Image speedometerHead, Functions Func)
    {
        speedText.text = $"Speed: {Math.Round(playerAvion.velocityFloat, 2)}";
        sineIncrement = SineIncrementCalculator(playerAvion.velocityFloat);

        if (!playerAvion.isBraking)
        {
            throttleText.text = $"Throttle: {Math.Round(playerAvion.throttle)}%";
            throttleText.color = new Color32(255, 255, 255, 255);
            speedText.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            sine = Func.Sinewawe(sineIncrement, sineMinColor, sineMaxColor);
            speedometerSine = Func.Sinewawe(sineIncrement, speedometerSineMinColor, speedometerSineMaxColor);
            throttleText.text = $"Throttle: BRAKE {Math.Round(playerAvion.prevThrottle)}%";
            throttleText.color = new Color32(((byte)sine), 0, 0, 255);
            speedText.color = new Color32(((byte)sine), 0, 0, 255);
            speedometerHead.color = new Color32(((byte)speedometerSine), ((byte)speedometerSine), ((byte)speedometerSine), 255);
        }
    }

    void ThrottleSliderHandler(PlayerAvion playerAvion, Transform throttleSliderTransform) // fuck you
    {
        float displayThrottle; if (playerAvion.isBraking) { displayThrottle = playerAvion.prevThrottle; } else { displayThrottle = playerAvion.throttle; }; throttleSliderTransform.localPosition = new Vector3(0, Functions.Map(displayThrottle, playerAvion.throttleMin, playerAvion.throttleMax, minThrottleSliderY, maxThrottleSliderY), 0);
    }

    void SpeedometerHandler(PlayerAvion playerAvion, Transform speedometerHeadPivotTransform)
    {
        float a = Functions.Map(playerAvion.velocityFloat, 0, playerAvion.maxSpeed, minSpeedometerRotation, maxSpeedometerRotation);
        speedometerHeadPivotTransform.eulerAngles = new Vector3(0, 0, a);
    }

    float SineIncrementCalculator(float velocity)
    {
        return Functions.Map(velocity, 0, PlayerAvion.maxSpeed, minSineIncrement, maxSineIncrement);
    }

}
