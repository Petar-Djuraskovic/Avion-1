using System;
using TMPro;
using UnityEngine;

public class UI_Handler : MonoBehaviour
{

    [SerializeField] public PlayerAvion PlayerAvion;

    public TextMeshProUGUI throttleText;
    public TextMeshProUGUI speedText;

    [SerializeField] public Functions Func;

    private double sine = 0f;

    [Range(0, 255)] public byte sineMinColor = 180;
    [Range(0, 255)] public byte sineMaxColor = 255;
    public float minSineIncrement = 0.1f;
    public float maxSineIncrement = 0.8f;
    [Range(0, 1)] public float sineIncrement = 0.1f;

    void FixedUpdate()
    {
        speedText.text = $"Speed: {Math.Round(PlayerAvion.velocityFloat, 2)}";
        sineIncrement = SineIncrementCalculator(PlayerAvion.velocityFloat);


        if (!PlayerAvion.isBraking)
        {
            throttleText.text = $"Throttle: {Math.Round(PlayerAvion.throttle)}";
            throttleText.color = new Color32(255, 255, 255, 255);
            speedText.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            sine = Func.Sinewawe(sineIncrement, sineMinColor, sineMaxColor);
            throttleText.text = $"Throttle: BRAKE {PlayerAvion.throttle}";
            throttleText.color = new Color32(((byte)sine), 0, 0, 255);
            speedText.color = new Color32(((byte)sine), 0, 0, 255);
        }
    }

    float SineIncrementCalculator(float velocity)
    {
        return Functions.Map(velocity, 0, PlayerAvion.maxSpeed, minSineIncrement, maxSineIncrement);
    }

}
