using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using Random = UnityEngine.Random;

public class CollectionsFunction : MonoBehaviour
{
    private static CollectionsFunction instance;

    public static CollectionsFunction Get()
    {
        return instance;
    }

    private void Awake() {
        instance = this;
    }

    private AdjustableValuesBank bank;

    private bool collectionsActive = false;

    void Start()
    {
        bank = AdjustableValuesBank.Get();
    }

    private IEnumerator Collect()
    {
        yield return new WaitForSeconds(bank.CollectionsTime);
        int randVal = Random.Range(1, bank.TotalChemPercent + 1);
        Debug.Log(randVal);
        //odds need breakpoints from previous breakpoint thingy uhhhhhhhhhhhhhhhhhhhhhhh
        string chemName = string.Empty;

        //can almost surely be improved upon but i cant think of any other way of doing it right now
        if(randVal > 0 && randVal <= bank.ReturnOdds("Oxygen")) 
        { 
            chemName = "Oxygen"; 
        }
        else if(randVal > bank.ReturnOdds("Oxygen") &&
            randVal <= bank.ReturnOdds("Nitrogen") + bank.ReturnOdds("Oxygen"))
        { 
            chemName = "Nitrogen"; 
        }
        else if(randVal > bank.ReturnOdds("Oxygen") + bank.ReturnOdds("Nitrogen") && 
            randVal <= bank.TotalChemPercent)
        { 
            chemName = "Hydrogen";
        }
        else
        {
            chemName = "Oxygen"; 
        }
        bank.GetTank(chemName).CanAddResources(1);
        //TODO replace this 1 with something else
        //
        //normally would, depending on upgrades or otherwise, say a base colelctions rate, or specific multiplier in some cases
        //where i would have implemented an additional lsit or dict or something that keeps track of each individual collectoin
        //of masses or whatever
        StartCoroutine("Collect");
    }

    public bool ToggleCollections()
    {
        if (collectionsActive) 
        { 
            collectionsActive = false; 
            StopAllCoroutines();
            return false;
        }
        else 
        {  
            collectionsActive = true;
            StartCoroutine("Collect"); 
            return true;
        }
    }
}
