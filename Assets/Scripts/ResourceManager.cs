using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ResourceType
{
    Oxygen,
    Air,
    Nitrogen,
    Hydrogen,
    Carbon,
    Nutrients,
    Water,
    Energy,
}

public class ResourceManager : MonoBehaviour
{
    public List<Resources> resourceList;

    private void Start()
    {
        resourceList = new List<Resources>() { };

        foreach (ResourceType foo in Enum.GetValues(typeof(ResourceType)))
        {
            GameObject obj = new GameObject(foo.ToString());
            Resources res = obj.AddComponent<Resources>();
            res.Initialize(foo.ToString());
            resourceList.Add(res);
            obj.transform.SetParent(transform, false);
        }

        foreach (Resources re in resourceList)
        {
            print(re.name);
        }
    }
}

public class Resources : MonoBehaviour
{
    public float maxVol;
    public float currentVol;
    new public string name;
    public float toll;
    public float timeBetweenConsumption;
    //a marker that indicates the type of emergency that triggers when a certain amount of thing cannot be subtracted from

    public void Initialize(string name)
    {
        this.name = name;
        maxVol = 100f;
        currentVol = 4f;
        timeBetweenConsumption = 2f;
        toll = 2f;

        //StartCoroutine(Consume());
    }

    public IEnumerator Consume()
    {
        yield return new WaitForSeconds(timeBetweenConsumption);
        if (CanSubtractResources(toll))
        {
            StartCoroutine("Consume");
        }
        else print("yo there's an issue");
        print(currentVol);
    }

    public bool CanSubtractResources(float amt)
    {
        if(amt <= currentVol)
        {
            currentVol -= amt;
            return true;
        }
        else return false;
    }
}