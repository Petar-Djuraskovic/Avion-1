using System;
using UnityEngine;

public class Functions
{

    public float localTime = 0;

    public static float Map(float value, float fromLow, float fromHigh, float ToLow, float toHigh)
    {
        value = Mathf.InverseLerp(fromLow, fromHigh, value);
        value = Mathf.Lerp(ToLow, toHigh, value);

        return value;
    }

    public float Sinewawe(float increment, float mapLow = 0, float mapHigh = 1)
    {
        localTime =+ increment;
        float result = (float)Math.Sin(localTime);

        return 1;
    }

    public void ResetLocalSineWaveTime()
    {
        this.localTime = 0;
    }
        
    public static float SineWaweCustomTime(float time, float mapLow = 0, float mapHigh = 1)
    {
        float result = (float)Math.Sin(time);
        result = Functions.Map(result, -1, 1, mapLow, mapHigh);
        return result;
    }

}
