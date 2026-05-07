using System;
using UnityEngine;
using TMPro;
using static UnityEditor.Handles;

public class Tank
{
    private float curVol;
    private float maxVol;
    private string name;
    private TankDisplay hud;

    public string Name { get { return name; } }
    public float Volume { get { return curVol; } }
    public float MaxVolume { get { return maxVol; } }

    public Tank(string name, float startVol, float maxVol, TankDisplay hud = null)
    {
        this.curVol = startVol;
        this.maxVol = maxVol;
        this.name = name;
        this.hud = hud;
    }

    public void AssignDisplay(TankDisplay hud)
    {
        this.hud = hud;
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
            hud?.UpdateTankDisplay(name, curVol, maxVol);
            return true;
        }
        else return false;
    }

    public void CanAddResources(float amt)
    {
        float temp = amt + curVol;
        curVol += amt;
        if(curVol >= maxVol)
        {
            curVol = maxVol;
        }
        hud?.UpdateTankDisplay(name, curVol, maxVol);
    }

    public void Print()
    {
        Debug.Log(name + ": " + curVol);
    }
}

public class TankDisplay
{
    private TextMeshProUGUI textObj;
    public TankDisplay(TextMeshProUGUI textObj)
    {
        this.textObj = textObj;
    }

    public void UpdateTankDisplay(string name, float newVol, float maxVol)
    {
        textObj.text = name + ": " + newVol.ToString() + "/" + maxVol.ToString();
    }
}