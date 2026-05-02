using System;
using UnityEngine;
using static UnityEditor.Handles;

public class Tank
{
    private float curVol;
    private float maxVol;
    private string name;

    public string Name { get { return name; } }
    public float Volume { get { return maxVol; } }
    public float MaxVolume { get { return maxVol; } }

    public Tank(string name, float startVol, float maxVol)
    {
        this.curVol = startVol;
        this.maxVol = maxVol;
    }

    public float GetVolRatio()
    {
        return curVol/maxVol;
    }

    public bool CanSubtractResources(float amt)
    {
        if (amt <= curVol)
        {
            curVol -= amt;
            return true;
        }
        else return false;
    }
}
