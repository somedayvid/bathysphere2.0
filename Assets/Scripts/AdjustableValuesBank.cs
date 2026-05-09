using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using TMPro;

public class AdjustableValuesBank : MonoBehaviour
{
    private static AdjustableValuesBank instance;

    public static AdjustableValuesBank Get()
    {
        return instance;
    }

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

    private void InitializeTanks()
    {
        stringTankPair = new Dictionary<string, Tank>();

        TankDisplayManager tnk = TankDisplayManager.Get();

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
        

        List<Transform> hudTankList = new List<Transform>();

        foreach (Transform child in HUDManager.Get().hudTankParentTrans)
        {
            hudTankList.Add(child);
        }

        int iterator = 0;
        foreach(string key in stringTankPair.Keys)
        {
            stringTankPair[key].AssignDisplay(new TankDisplay(hudTankList[iterator].GetChild(0).GetComponent<TextMeshProUGUI>()));
            iterator++;
        }
    }
    
    public Dictionary<string, Tank> StringTankPair
    {
        get { return stringTankPair; }
    }

    public Tank GetTank(string key)
    {
        return stringTankPair[key];
    }

    // public float BreathingToll()
    // {
    //     //update with formula to calculate for inbetween movement speeds as well for 8-70lpm
    //     // return currentAtm * minGasNeed;
    //     return 0.0f;
    // }

    //currency
    public float currency = 1000;

    // //energy usage stats
    // public float base_usage = 10;
    // public float cur_usage = base_usage;
    // public float max_usage = 100;

    // public float base_drillToll = 10;
    // public float cur_drillToll = base_drillToll;

    // public float base_hammerToll = 15;
    // public float cur_hammerToll = base_hammerToll;

    // public float base_sawToll = 8;
    // public float cur_sawToll = base_sawToll;

    // //collections functionality
    // public float base_collectionsUsage = 10;
    // public float cur_collectionsUsage = base_collectionsUsage;

    // private float base_collectionsRate = 1.0f;
    // public float cur_collectionsRate = base_collectionsRate;

    private float base_collectionsTimeBetween = 0.25f;
    private float cur_collectionsTimeBetween;

    public float CollectionsTime
    {
        get { return cur_collectionsTimeBetween; }
    }

    private void InitializeTools()
    {
        cur_collectionsTimeBetween = base_collectionsTimeBetween;
    }

    
    //percentage values
    //should this be changed? how the collections are handeld?

    private int base_oxygenRatio = 75;
    private int base_nitrogenRatio = 20;
    private int base_hydrogenRatio = 5;

    private int cur_oxygenRatio;
    private int cur_nitrogenRatio;
    private int cur_hydrogenRatio;

    private int totalChemPercent = 100;
    public int TotalChemPercent { get {return totalChemPercent;} }

    private Dictionary<string, int> chemNameRatioDict;

    private void InitializeChemicals()
    {
        cur_oxygenRatio = base_oxygenRatio;
        cur_nitrogenRatio = base_nitrogenRatio;
        cur_hydrogenRatio = base_hydrogenRatio;

        chemNameRatioDict = new Dictionary<string, int>();
        chemNameRatioDict.Add("Oxygen", cur_oxygenRatio);
        chemNameRatioDict.Add("Nitrogen", cur_nitrogenRatio);
        chemNameRatioDict.Add("Hydrogen", cur_hydrogenRatio);
        totalChemPercent = base_oxygenRatio + base_nitrogenRatio + base_hydrogenRatio;
    }

    public int ReturnOdds(string name)
    {
        return chemNameRatioDict[name];
    }

    /// <summary>
    /// Adjsuts individual and total chemical percents for being rolled
    /// </summary>
    /// <param name="chemName">Name of chemical</param>
    /// <param name="amt">Amt being changed by; can be negative</param>
    public void AdjustRatio(string chemName, int amt = 1)
    {
        //checks if the value being increased was less th
        //pos -> pos : simply add values together
        //pos -> neg : if value is negative, adds amt to value
        //neg -> pos : if new value is positive, adds amt to value
        //neg -> neg : subtract amount from negative value in dict, no change to total percentage

        //understood as (initial adjust val) -> (new value post adjust) (stand in oxygenratio) (stand in nitrogenratio) (total ratio) -> (new total ratio value post new value adjust)
        //40 -> 60 75 20 135 -> 155
        //40 -> -20 75 20 135 -> 95
        //-20 -> 40 75 20 95 -> 135
        //-20 -> -5 75 20 95 -> 95

        //pos -> pos (multiplication) 
        //5 -> 60 (x12) 75 20 100 -> 
        //(where is multiplication being applied?[suppose for multiplication it it just added 5 * 11, for a 12x increase?, and
        //then subtracted 55 for its removal?, potentially avoiding multiplciation + subtraction problems(?!)])

        //total percentage should always be the sum of whatever the positive values are 
        //i have to make sure whenever an amt is unapplied (by way of equpped part or otherwise) the application affect is undone correctly  

        chemNameRatioDict[chemName] += amt;
        //if i ever needed to add more chemicals or it had to be more scalable then an enum of the chems should be made and then
        //iterated through for these
        int ReturnIfPositive(int amt){ if(amt >= 0){return amt;} else {return 0;}}
        totalChemPercent =  ReturnIfPositive(cur_oxygenRatio) + ReturnIfPositive(cur_nitrogenRatio) + ReturnIfPositive(cur_hydrogenRatio);
    }

    public float base_atm = 0;
    // public float cur_atm = base_atm;

    public float minNoiseLevel = 25;
    public float maxNoiseLevel = 100;
    


    private void Awake()
    {
        instance = this;
        InitializeTanks();
        InitializeChemicals();
        InitializeTools();
    }
}