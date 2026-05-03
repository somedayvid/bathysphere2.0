using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class TimersManager : MonoBehaviour
{
    Dictionary<string, Toll> nameTankPair;

    private void Awake()
    {
        nameTankPair = new Dictionary<string, Toll>();
    }

    private float TempTollRateTest()
    {
        return 1.0f;
    }

    private float TempTollAmtTest()
    {
        return 1.0f;
    }

    void Start()
    {
        nameTankPair.Add("Air", new Toll(TempTollRateTest, LevelManager.Get().BreathingToll));
        nameTankPair.Add("Water", new Toll(TempTollRateTest, TempTollAmtTest));
        nameTankPair.Add("Nutrients", new Toll(TempTollRateTest, TempTollAmtTest));
        nameTankPair.Add("Energy", new Toll(TempTollRateTest, TempTollAmtTest));

        nameTankPair.Add("Oxygen", new Toll());
        nameTankPair.Add("Nitrogen", new Toll());
        nameTankPair.Add("Hydrogen", new Toll());
        nameTankPair.Add("Carbon", new Toll());

        foreach (string key in nameTankPair.Keys)
        {
            if(nameTankPair[key].TollActive){
                StartCoroutine(TollRateTicker(key, nameTankPair[key].TimeBetween(), nameTankPair[key].TollAmt()));
            }
        }

        TankDisplayManager.Get().Initialize();

        //in some cases timers may already be counting down when players enter an area of greater toll or rate, update to keep these things consistent by considering
        //the new rate and then doing some math or something to make up the difference

        foreach(string key in nameTankPair.Keys)
        {
            Tank temp = GetTank(key);
            TankDisplayManager.Get().GetTankDisplay(key).UpdateTankDisplay(key, temp.Volume, temp.MaxVolume);
        }
    }

    private Tank GetTank(string name)
    {
        return AdjustableValuesBank.Get().GetTank(name);
    }

    private IEnumerator TollRateTicker(string name, float time, float amt)
    {
        yield return new WaitForSeconds(time);
        Tank temp = GetTank(name);
        if (!temp.CanSubtractResources(amt))
        {
            print("cannot subtract from here: " + temp.Name);
            //for essentials trigger warning
            //for resources not neccesary just make a sound
        }
        //for testing in the case of enough being there is subtract
        else
        { 
            // temp.Print();
            TankDisplayManager.Get().GetTankDisplay(name).UpdateTankDisplay(name, temp.Volume, temp.MaxVolume);
            //then get the hud display of the tank, and then update it with the new value
        }
        
        StartCoroutine(TollRateTicker(name, time, amt));
    }
}

public class Toll
{
    private Func<float> tollRateFunc;
    private Func<float> tollAmtFunc;
    private bool tollActive = true;

    public bool TollActive { get { return tollActive; } }

    public Toll(Func<float> tollRateFunc, Func<float> tollAmtFunc)
    {
        this.tollRateFunc = tollRateFunc;
        this.tollAmtFunc = tollAmtFunc;
    }

    public Toll()
    {
        tollActive = false;
    }

    public float TimeBetween()
    {
        return tollRateFunc.Invoke();
    }

    public float TollAmt()
    {
        return tollAmtFunc.Invoke();
    }
}
