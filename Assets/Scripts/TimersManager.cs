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

        //oxygen;
        //nitrogen;
        //hydrogen;
        //carbon

        foreach (string key in nameTankPair.Keys)
        {
            StartCoroutine(TollRateTicker(key, nameTankPair[key].TimeBetween(), nameTankPair[key].TollAmt()));
        }

        //in some cases timers may already be counting down when players enter an area of greater toll or rate, update to keep these things consistent by considering
        //the new rate and then doing some math or something to make up the difference
    }

    private IEnumerator TollRateTicker(string name, float time, float amt)
    {
        yield return new WaitForSeconds(time);
        Tank temp = AdjustableValueTank.Get().GetTank(name);
        if (!temp.CanSubtractResources(amt))
        {
            print("hey i cant subtract from here, in this case trigger a warning in some cases");
            //for essentials trigger warning
            //for resources not neccesary just make a sound
        }
        //for testing
        else{ temp.Print(); }
        
        StartCoroutine(TollRateTicker(name, time, amt));
    }
}

public class Toll
{
    private Func<float> tollRateFunc;
    private Func<float> tollAmtFunc;
    public Toll(Func<float> tollRateFunc, Func<float> tollAmtFunc)
    {
        this.tollRateFunc = tollRateFunc;
        this.tollAmtFunc = tollAmtFunc;
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
