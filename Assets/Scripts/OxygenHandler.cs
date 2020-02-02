using System;
using UnityEngine;

public class OxygenHandler : MonoBehaviour
{
    public int Max = 100;
    public int HullBreaches;
    public int OxygenPerSecond;
    public int OxygenLeft;
    public TimeSpan Second = TimeSpan.FromSeconds(1);
    public DateTime lastTick = DateTime.UtcNow;


    public OxygenHandler()
    {
        OxygenLeft = Max;
        OxygenPerSecond = 2;
        HullBreaches = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTick + Second < DateTime.UtcNow)
        {
            OxygenLeft += OxygenPerSecond;
            Debug.Log($"Adding {OxygenPerSecond} to Oxygen");
            OxygenLeft -= HullBreaches;
            Debug.Log($"Subtracting {HullBreaches} from oxygen");
            lastTick = DateTime.UtcNow;
        }

        OxygenLeft = Math.Min(OxygenLeft, Max);
    }
}
