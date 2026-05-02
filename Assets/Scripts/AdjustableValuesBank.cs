using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AdjustableValuesBank : MonoBehaviour
{
    private static AdjustableValuesBank instance;

    private float startEVol = 100.0f;
    private float startRVol = 0.0f;
    private float maxTankVol = 100.0f;

    private Tank air;
    private Tank water;
    private Tank nutrients;
    private Tank energy;

    private Tank oxygen;
    private Tank nitrogen;
    private Tank hydrogen;
    private Tank carbon;

    private Dictionary<string, Tank> stringTankPair;

    public static AdjustableValuesBank Get()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        stringTankPair = new Dictionary<string, Tank>();

        air = new Tank("Air", startEVol, maxTankVol);
        water = new Tank("Water", startEVol, maxTankVol);
        nutrients = new Tank("Nutrients", startEVol, maxTankVol);
        energy = new Tank("Energy", startEVol, maxTankVol);

        oxygen    = new Tank("Oxygen", startRVol, maxTankVol);
        nitrogen  = new Tank("Nitrogen", startRVol, maxTankVol);
        hydrogen  = new Tank("Hydrogen", startRVol, maxTankVol);
        carbon    = new Tank("Carbon", startRVol, maxTankVol);

        stringTankPair.Add(air.Name, air);
        stringTankPair.Add(water.Name, water);
        stringTankPair.Add(nutrients.Name, nutrients);
        stringTankPair.Add(energy.Name, energy);

        stringTankPair.Add(oxygen.Name, oxygen);
        stringTankPair.Add(nitrogen.Name, nitrogen);
        stringTankPair.Add(hydrogen.Name, hydrogen);
        stringTankPair.Add(carbon.Name, carbon);
    }

    public Tank GetTank(string key)
    {
        return stringTankPair[key];
    }

    //
    public float currency = 1000;
   
}