using System;
using UnityEngine;

public class Functions : MonoBehaviour
{

    public float localTime = 0;

    public static float Map(float value, float fromLow, float fromHigh, float ToLow, float toHigh)
    {
        value = Mathf.InverseLerp(fromLow, fromHigh, value);
        value = Mathf.Lerp(ToLow, toHigh, value);

        return value;
    }

    public static float MapFromZero(float value, float fromHigh, float toHigh)
    {
        value /= fromHigh; 
        value *= toHigh;

        return value;
    }

    public float Sinewawe(float increment, float mapLow = 0, float mapHigh = 1)
    {
        localTime += increment;
        float result = (float)Math.Sin(localTime);
        return Functions.Map(result, -1, 1, mapLow, mapHigh);
    }
        
    public static float SineWaweCustomTime(float time, float mapLow = 0, float mapHigh = 1)
    {
        float result = (float)Math.Sin(time);
        return Functions.Map(result, -1, 1, mapLow, mapHigh);
    }

}
