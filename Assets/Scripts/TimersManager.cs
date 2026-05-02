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

        foreach (var pair in nameTankPair.Values)
        {
            StartCoroutine(TollRateTicker(pair.TimeBetween(), pair.TollAmt()));
        }

        //in some cases timers may already be counting down when players enter an area of greater toll or rate, update to keep these things consistent by considering
        //the new rate and then doing some math or something to make up the difference
    }

    private IEnumerator TollRateTicker(float time, float amt)
    {
        yield return new WaitForSeconds(time);
        //do the things
        //start new coroutine
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
