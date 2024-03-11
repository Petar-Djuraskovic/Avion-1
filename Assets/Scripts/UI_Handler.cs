using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class UI_Handler : MonoBehaviour
{

    [SerializeField] public PlayerAvion PlayerAvion;

    public TextMeshProUGUI throttleText;
    public TextMeshProUGUI speedText;

    Functions Functions;

    private double time = 0f;
    private double sine = 0f;
    private double sine100to255;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = $"Speed: {Math.Round(PlayerAvion.velocityFloat, 2)}";

        if (!PlayerAvion.brakeState)
        {
            throttleText.text = $"Throttle: {Math.Round(PlayerAvion.throttle)}";
            throttleText.color = new Color32(255, 255, 255, 255);
            speedText.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            sine = Functions.Sinewawe(Time.deltaTime, 180, 255);
            throttleText.text = $"Throttle: BRAKE {PlayerAvion.brakeThrottle}";
            throttleText.color = new Color32(((byte)sine), 0, 0, 255);
            speedText.color = new Color32(255, 0, 0, 255);
        

        }

        time += Time.deltaTime;
        Debug.Log(time);

    }

}
